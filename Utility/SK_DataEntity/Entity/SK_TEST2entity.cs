using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SK_DataEntity.Entity
{
    [Table("SK_TEST2")]
    public partial class SK_TEST2
    {
        [Column("ID")]
        [Key]
        public long ID { get; set; }

        [Column("NAME")]
        [MaxLength(80)]
        public string NAME { get; set; }

        // Navigation Properties
        // Add navigation properties here based on foreign key relationships
    }
}
