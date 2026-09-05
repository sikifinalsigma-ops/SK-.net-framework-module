using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SK_DataEntity.Entity
{
    // 1. Define your EF Entity
    [Table("SK_TEST")]
    public class SK_TEST
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ID")]
        public string ID { get; set; }

        [Required]
        [StringLength(100)]
        [Column("NAME")]
        public string NAME { get; set; }

        [Column("DATETIME")]
        public DateTime? DATETIME { get; set; }

        [Column("NUM")]
        public int? NUM { get; set; }

        [Column("VERSION")]
        [ConcurrencyCheck]
        public int VERSION { get; set; }

    }
}
