using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WY_DataEntity.Entity
{
    [Table("TB_BARRIER_WHITE_CAR")]
    public partial class TB_BARRIER_WHITE_CAR
    {
        [Column("C_MAINID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [MaxLength(80)]
        public string C_MAINID { get; set; }

        [Column("C_CARNO")]
        [MaxLength(80)]
        public string C_CARNO { get; set; }

        [Column("C_CREATETIME")]
        [MaxLength(80)]
        public string C_CREATETIME { get; set; }

        [Column("C_BARRIERNAME")]
        [MaxLength(80)]
        public string C_BARRIERNAME { get; set; }

        [Column("C_DELETEFLAG")]
        [MaxLength(2)]
        public string C_DELETEFLAG { get; set; }

        [Column("C_BARRIERIP")]
        [MaxLength(60)]
        public string C_BARRIERIP { get; set; }

        [Column("C_INPUTPERSON")]
        [MaxLength(80)]
        public string C_INPUTPERSON { get; set; }

        [Column("C_BAK_1")]
        [MaxLength(200)]
        public string C_BAK_1 { get; set; }

        [Column("C_BAK_2")]
        [MaxLength(2)]
        public string C_BAK_2 { get; set; }

        [Column("C_BAK_3")]
        [MaxLength(2)]
        public string C_BAK_3 { get; set; }

        [Column("C_BAK_4")]
        [MaxLength(2)]
        public string C_BAK_4 { get; set; }

        [Column("C_BARRIERID")]
        [MaxLength(80)]
        public string C_BARRIERID { get; set; }

        [Column("C_REASON")]
        [MaxLength(200)]
        public string C_REASON { get; set; }

        [Column("C_UPLOADFLAG")]
        [MaxLength(2)]
        public string C_UPLOADFLAG { get; set; }

        // Navigation Properties
        // Add navigation properties here based on foreign key relationships
    }
}
