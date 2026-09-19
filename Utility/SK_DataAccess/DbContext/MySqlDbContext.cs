using System.Data.Entity;
using MySql.Data.EntityFramework;
using MySql.Data.MySqlClient;

namespace SK_DataAccess
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class MySqlDbContext : DbContext
    {        
        public MySqlDbContext(string connString) : base(new MySqlConnection(connString), contextOwnsConnection: true)
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            foreach (var entityType in EntityRegister.registerEntity)
            {
                modelBuilder.RegisterEntityType(entityType);
            }
            base.OnModelCreating(modelBuilder);
        }
    }

}
