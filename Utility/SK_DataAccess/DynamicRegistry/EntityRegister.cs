using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Configuration;

namespace SK_DataAccess
{
    internal static class EntityRegister
    {
        internal readonly static Type[] registerEntity;

        static EntityRegister()
        {

            string nameSpace = ConfigurationManager.AppSettings["EntityNameSpace"];
            if (string.IsNullOrWhiteSpace(nameSpace))
            {
                nameSpace = "SK_DataEntity.Entity"; // 预设值
            }

            registerEntity = Assembly.Load("SK_DataEntity").GetTypes().Where(t =>
            t.IsClass
            && !t.IsAbstract

            // 注意：需要先判断 t.Namespace != null，防范无命名空间的全域类别
            //&& t.Namespace != null
            && (t.Namespace == nameSpace)
            //&& targetNamespaces.Contains(t.Namespace)


            && (t.GetCustomAttribute<TableAttribute>() != null)

            ).ToArray();
        }
    }
}
