using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WY_DataEntity.Entity
{
    [Table("SK_WHITE_CAR_LIST")]
    public partial class SK_WHITE_CAR_LIST
    {
        [Column("C_ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [MaxLength(80)]
        public string C_ID { get; set; }

        [Column("C_CARNO")]
        [MaxLength(80)]
        public string C_CARNO { get; set; }

        [Column("C_DELFLAG")]
        [MaxLength(2)]
        public string C_DELFLAG { get; set; }

        [Column("C_GATE_IP")]
        [MaxLength(80)]
        public string C_GATE_IP { get; set; }

        [Column("C_INPUTPERSON")]
        [MaxLength(80)]
        public string C_INPUTPERSON { get; set; }

        // Navigation Properties
        // Add navigation properties here based on foreign key relationships
    }
}
