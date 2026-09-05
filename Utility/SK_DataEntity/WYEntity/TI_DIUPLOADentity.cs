using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WY_DataEntity.Entity
{
    [Table("TI_DIUPLOAD")]
    public partial class TI_DIUPLOAD
    {
        [Column("C_TBDIPDIID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [MaxLength(50)]
        public string C_TBDIPDIID { get; set; }

        [Column("C_QUEUEID")]
        [MaxLength(30)]
        public string C_QUEUEID { get; set; }

        [Column("C_DATA")]
        public string C_DATA { get; set; }

        [Column("C_TIMESTAMP")]
        public object C_TIMESTAMP { get; set; }

        [Column("C_STATUS")]
        [MaxLength(2)]
        public string C_STATUS { get; set; }

        [Column("C_PROCESSTIME")]
        public DateTime? C_PROCESSTIME { get; set; }

        [Column("C_RESTASUS")]
        [MaxLength(80)]
        public string C_RESTASUS { get; set; }

        [Column("C_REMSG")]
        public string C_REMSG { get; set; }

        [Column("C_EXTENDFIELDA")]
        [MaxLength(200)]
        public string C_EXTENDFIELDA { get; set; }

        [Column("C_EXTENDFIELDB")]
        [MaxLength(200)]
        public string C_EXTENDFIELDB { get; set; }

        [Column("C_EXTENDFIELDC")]
        [MaxLength(200)]
        public string C_EXTENDFIELDC { get; set; }

        // Navigation Properties
        // Add navigation properties here based on foreign key relationships
    }
}
