using SK_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
namespace SK_DataEntity.Generator
{
    public class OracleEntityGenerator
    {
        private static string ConnectionString = string.Empty;
        private static string SchemaName = string.Empty;
        private static string OutputDirectory = "Utility//Entity";
        private static string NamespaceName = "SK_DataEntity.Entity";
        public static void Generate(string ConnectionString, List<string> tableNames)
        {
            try
            {
                OracleEntityGenerator.ConnectionString = ConnectionString;
                DirectoryInfo directoryInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                //DirectoryInfo directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
                OutputDirectory = directoryInfo.FullName + OutputDirectory;
                if (!Directory.Exists(OutputDirectory))
                {
                    Directory.CreateDirectory(OutputDirectory);
                }

                foreach (var table in tableNames)
                {
                    var entityCode = GenerateEntity(table);
                    var className = table.ToUpper() + "entity";
                    var filePath = Path.Combine(OutputDirectory, $"{className}.cs");
                    File.WriteAllText(filePath, entityCode);
                    Debug.WriteLine($"Generated: {className}.cs");
                }
            }
            catch
            {
                throw ;
            }
        }

        public static string GenerateEntity(string tableName)
        {
            var columns = GetColumns(tableName);
            var className = tableName.ToUpper();

            var sb = new StringBuilder();

            // Using statements
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.ComponentModel.DataAnnotations;");
            sb.AppendLine("using System.ComponentModel.DataAnnotations.Schema;");
            sb.AppendLine();

            // Namespace
            sb.AppendLine($"namespace {NamespaceName}");
            sb.AppendLine("{");

            // Class declaration
            sb.AppendLine($"    [Table(\"{tableName}\")]");
            sb.AppendLine($"    public partial class {className}");
            sb.AppendLine("    {");

            // Constructor for collections
            var hasCollections = columns.Any(c => c.IsForeignKey);
            if (hasCollections)
            {
                sb.AppendLine($"        public {className}()");
                sb.AppendLine("        {");
                // Initialize collections here if needed
                sb.AppendLine("        }");
                sb.AppendLine();
            }

            // Properties
            foreach (var column in columns)
            {
                GenerateProperty(sb, column);
            }

            // Navigation properties (commented out - to be implemented manually)
            sb.AppendLine("        // Navigation Properties");
            sb.AppendLine("        // Add navigation properties here based on foreign key relationships");

            var foreignKeys = columns.Where(c => c.IsForeignKey).ToList();
            foreach (var fk in foreignKeys)
            {
                if (!string.IsNullOrEmpty(fk.ReferencedTable))
                {
                    var referencedClassName = fk.ReferencedTable.ToUpper();
                    var propertyName = fk.ColumnName.EndsWith("_ID")
                        ? fk.ColumnName.Substring(0, fk.ColumnName.Length - 3)
                        : fk.ReferencedTable;
                    propertyName = propertyName.ToUpper();

                    sb.AppendLine($"        // public virtual {referencedClassName} {propertyName} {{ get; set; }}");
                }
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        public static List<ColumnInfo> GetColumns(string tableName)
        {
            var columns = new List<ColumnInfo>();

            using (var dbconnect = new OracleRepository(ConnectionString))
            {
                var sql = @"
                    SELECT 
                        c.COLUMN_NAME,
                        c.DATA_TYPE,
                        c.NULLABLE,
                        c.DATA_LENGTH,
                        c.DATA_PRECISION,
                        c.DATA_SCALE,
                        c.DATA_DEFAULT,
                        CASE WHEN pk.COLUMN_NAME IS NOT NULL THEN 'Y' ELSE 'N' END as IS_PRIMARY_KEY,
                        CASE WHEN fk.COLUMN_NAME IS NOT NULL THEN 'Y' ELSE 'N' END as IS_FOREIGN_KEY,
                        fk.R_TABLE_NAME as REFERENCED_TABLE
                    FROM USER_TAB_COLUMNS c
                    LEFT JOIN (
                        SELECT cc.COLUMN_NAME 
                        FROM USER_CONSTRAINTS con
                        JOIN USER_CONS_COLUMNS cc ON con.CONSTRAINT_NAME = cc.CONSTRAINT_NAME
                        WHERE con.CONSTRAINT_TYPE = 'P' 
                        AND cc.TABLE_NAME = :tableName
                    ) pk ON c.COLUMN_NAME = pk.COLUMN_NAME
                    LEFT JOIN (
                        SELECT 
                            cc.COLUMN_NAME,
                            r_cc.TABLE_NAME as R_TABLE_NAME
                        FROM USER_CONSTRAINTS con
                        JOIN USER_CONS_COLUMNS cc ON con.CONSTRAINT_NAME = cc.CONSTRAINT_NAME
                        JOIN USER_CONSTRAINTS r_con ON con.R_CONSTRAINT_NAME = r_con.CONSTRAINT_NAME
                        JOIN USER_CONS_COLUMNS r_cc ON r_con.CONSTRAINT_NAME = r_cc.CONSTRAINT_NAME
                        WHERE con.CONSTRAINT_TYPE = 'R' 
                        AND cc.TABLE_NAME = :tableName
                    ) fk ON c.COLUMN_NAME = fk.COLUMN_NAME
                    WHERE c.TABLE_NAME = :tableName
                    ORDER BY c.COLUMN_ID";

                DataTable tablecolumns = dbconnect.QueryEntitiesAsDataTable(sql, OracleParameterFactory.Create("tableName", tableName));
                foreach (DataRow dr in tablecolumns.Rows)
                {
                    columns.Add(new ColumnInfo
                    {
                        ColumnName = dr["COLUMN_NAME"].ToString(),
                        DataType = dr["DATA_TYPE"].ToString(),
                        IsNullable = dr["NULLABLE"].ToString() == "Y",
                        IsPrimaryKey = dr["IS_PRIMARY_KEY"].ToString() == "Y",
                        IsForeignKey = dr["IS_FOREIGN_KEY"].ToString() == "Y",
                        MaxLength = DataRowHelper.GetInt(dr, "DATA_LENGTH"),
                        Precision = DataRowHelper.GetInt(dr, "DATA_PRECISION"),
                        Scale = DataRowHelper.GetInt(dr, "DATA_SCALE"),
                        DefaultValue = DataRowHelper.GetString(dr, "DATA_DEFAULT")?.Trim(),
                        ReferencedTable = DataRowHelper.GetString(dr, "REFERENCED_TABLE"),
                    });
                }
            }

            return columns;
        }

        private static void GenerateProperty(StringBuilder sb, ColumnInfo column)
        {
            var propertyName = column.ColumnName.ToUpper();
            var dataType = GetCSharpType(column);
            var isNullable = column.IsNullable && IsValueType(GetBaseCSharpType(column));

            // Column attribute
            sb.AppendLine($"        [Column(\"{column.ColumnName}\")]");

            // Key attribute
            if (column.IsPrimaryKey)
            {
                sb.AppendLine("        [Key]");
                sb.AppendLine("        [DatabaseGenerated(DatabaseGeneratedOption.None)]");
            }

            // Required attribute
            if (!column.IsNullable && GetBaseCSharpType(column) == "string")
            {
                sb.AppendLine("        [Required]");
            }

            // MaxLength attribute
            if (column.MaxLength > 0 && GetBaseCSharpType(column) == "string" && column.MaxLength < 4000)
            {
                sb.AppendLine($"        [MaxLength({column.MaxLength})]");
            }

            // DatabaseGenerated attribute for identity columns
            if (column.IsPrimaryKey && !string.IsNullOrEmpty(column.DefaultValue))
            {
                sb.AppendLine("        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]");
            }

            // Property declaration
            var nullableSuffix = isNullable ? "?" : "";
            sb.AppendLine($"        public {dataType}{nullableSuffix} {propertyName} {{ get; set; }}");
            sb.AppendLine();
        }

        private static string GetCSharpType(ColumnInfo column)
        {
            switch (column.DataType.ToUpper())
            {
                case "NUMBER":
                    if (column.Scale > 0)
                        return "decimal";
                    else if (column.Precision <= 3)
                        return "byte";
                    else if (column.Precision <= 5)
                        return "short";
                    else if (column.Precision <= 10)
                        return "int";
                    else
                        return "long";

                case "VARCHAR2":
                case "NVARCHAR2":
                case "CHAR":
                case "NCHAR":
                case "CLOB":
                case "NCLOB":
                    return "string";

                case "DATE":
                case "TIMESTAMP":
                case "TIMESTAMP WITH TIME ZONE":
                case "TIMESTAMP WITH LOCAL TIME ZONE":
                    return "DateTime";

                case "BLOB":
                case "RAW":
                    return "byte[]";

                case "BINARY_FLOAT":
                    return "float";

                case "BINARY_DOUBLE":
                    return "double";

                default:
                    return "object";
            }
        }

        private static string GetBaseCSharpType(ColumnInfo column)
        {
            var type = GetCSharpType(column);
            return type.Replace("?", "");
        }

        private static bool IsValueType(string type)
        {
            return new[] { "byte", "short", "int", "long", "decimal", "DateTime", "bool", "double", "float" }.Contains(type);
        }

        public class ColumnInfo
        {
            public string ColumnName { get; set; }
            public string DataType { get; set; }
            public bool IsNullable { get; set; }
            public bool IsPrimaryKey { get; set; }
            public bool IsForeignKey { get; set; }
            public int MaxLength { get; set; }
            public int Precision { get; set; }
            public int Scale { get; set; }
            public string DefaultValue { get; set; }
            public string ReferencedTable { get; set; }
        }
    }
}


