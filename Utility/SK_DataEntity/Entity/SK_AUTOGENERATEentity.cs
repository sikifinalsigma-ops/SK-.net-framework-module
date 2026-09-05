using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SK_DataEntity.Entity
{
    [Table("SK_AUTOGENERATE")]
    public partial class SK_AUTOGENERATE
    {
        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [MaxLength(20)]
        public string ID { get; set; }

        [Column("NAME")]
        [MaxLength(40)]
        public string NAME { get; set; }

        [Column("CREATEDATE")]
        public DateTime? CREATEDATE { get; set; }

        [Column("NUM")]
        public byte? NUM { get; set; }

        [Column("NUMFRA")]
        public decimal? NUMFRA { get; set; }

        // Navigation Properties
        // Add navigation properties here based on foreign key relationships
    }
}
