using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WY_DataEntity.Entity
{
    [Table("TM_INOUTRECORDIDDETAIL")]
    public partial class TM_INOUTRECORDIDDETAIL
    {
        [Column("C_INOUTRECORDID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [MaxLength(50)]
        public string C_INOUTRECORDID { get; set; }

        [Column("C_CARNO")]
        [MaxLength(30)]
        public string C_CARNO { get; set; }

        [Column("C_CARTYPE")]
        [MaxLength(20)]
        public string C_CARTYPE { get; set; }

        [Column("C_CARNAME")]
        [MaxLength(30)]
        public string C_CARNAME { get; set; }

        [Column("D_ENTEROUTTIME")]
        public DateTime? D_ENTEROUTTIME { get; set; }

        [Column("C_GATEINFOID")]
        [MaxLength(30)]
        public string C_GATEINFOID { get; set; }

        [Column("C_GATEDES")]
        [MaxLength(30)]
        public string C_GATEDES { get; set; }

        [Column("C_GATEIP")]
        [MaxLength(30)]
        public string C_GATEIP { get; set; }

        [Column("C_ENTERCARPICTURE")]
        [MaxLength(200)]
        public string C_ENTERCARPICTURE { get; set; }

        [Column("C_CARNOPICTURE")]
        [MaxLength(200)]
        public string C_CARNOPICTURE { get; set; }

        [Column("C_FINISHFLAG")]
        [MaxLength(2)]
        public string C_FINISHFLAG { get; set; }

        [Column("C_TIMESTAMP")]
        public object C_TIMESTAMP { get; set; }

        [Column("C_EXTENDFIELDA")]
        [MaxLength(200)]
        public string C_EXTENDFIELDA { get; set; }

        [Column("C_EXTENDFIELDB")]
        [MaxLength(200)]
        public string C_EXTENDFIELDB { get; set; }

        [Column("C_EXTENDFIELDC")]
        [MaxLength(200)]
        public string C_EXTENDFIELDC { get; set; }

        [Column("C_EXTENDFIELDD")]
        [MaxLength(40)]
        public string C_EXTENDFIELDD { get; set; }

        [Column("C_DELFALG")]
        [MaxLength(10)]
        public string C_DELFALG { get; set; }

        [Column("C_REASON")]
        [MaxLength(255)]
        public string C_REASON { get; set; }

        [Column("C_CARRECORDNO")]
        [MaxLength(255)]
        public string C_CARRECORDNO { get; set; }

        [Column("C_CARRYTOOLTYPE")]
        [MaxLength(255)]
        public string C_CARRYTOOLTYPE { get; set; }

        [Column("C_UPLOAD")]
        [MaxLength(2)]
        public string C_UPLOAD { get; set; }

        [Column("C_RESTASUS")]
        [MaxLength(10)]
        public string C_RESTASUS { get; set; }

        [Column("C_REMSG")]
        [MaxLength(255)]
        public string C_REMSG { get; set; }

        [Column("C_PROCESSTIME")]
        public DateTime? C_PROCESSTIME { get; set; }

        [Column("C_FACTORYID")]
        [MaxLength(255)]
        public string C_FACTORYID { get; set; }

        [Column("C_UPLOAD2")]
        [MaxLength(255)]
        public string C_UPLOAD2 { get; set; }

        [Column("C_RESTASUS2")]
        [MaxLength(255)]
        public string C_RESTASUS2 { get; set; }

        [Column("C_REMSG2")]
        [MaxLength(255)]
        public string C_REMSG2 { get; set; }

        [Column("C_UPLOAD3")]
        [MaxLength(255)]
        public string C_UPLOAD3 { get; set; }

        [Column("C_RESTASUS3")]
        [MaxLength(255)]
        public string C_RESTASUS3 { get; set; }

        [Column("C_REMSG3")]
        [MaxLength(255)]
        public string C_REMSG3 { get; set; }

        [Column("C_TRANSFLAG1")]
        [MaxLength(255)]
        public string C_TRANSFLAG1 { get; set; }

        [Column("C_TRANDATE1")]
        [MaxLength(255)]
        public string C_TRANDATE1 { get; set; }

        [Column("C_TRANSFLAG2")]
        [MaxLength(255)]
        public string C_TRANSFLAG2 { get; set; }

        [Column("C_TRANDATE2")]
        [MaxLength(255)]
        public string C_TRANDATE2 { get; set; }

        [Column("C_UPLOADFLAG")]
        [MaxLength(2)]
        public string C_UPLOADFLAG { get; set; }

        // Navigation Properties
        // Add navigation properties here based on foreign key relationships
    }
}
