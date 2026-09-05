using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WY_DataEntity.Entity
{
    [Table("TB_BARRIER_INFO")]
    public partial class TB_BARRIER_INFO
    {
        [Column("C_MAIND")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [MaxLength(80)]
        public string C_MAIND { get; set; }

        [Column("C_CODE")]
        [MaxLength(30)]
        public string C_CODE { get; set; }

        [Column("C_NAME")]
        [MaxLength(40)]
        public string C_NAME { get; set; }

        [Column("C_STATE")]
        [MaxLength(2)]
        public string C_STATE { get; set; }

        [Column("C_IP")]
        [MaxLength(30)]
        public string C_IP { get; set; }

        [Column("C_OPENURL")]
        [MaxLength(160)]
        public string C_OPENURL { get; set; }

        // Navigation Properties
        // Add navigation properties here based on foreign key relationships
    }
}
