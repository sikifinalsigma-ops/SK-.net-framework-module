using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WY_DataEntity.Entity
{
    [Table("TM_ICCREATCARD")]
    public partial class TM_ICCREATCARD
    {
        [Column("C_NOTEID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [MaxLength(50)]
        public string C_NOTEID { get; set; }

        [Column("C_CARDID")]
        [Required]
        [MaxLength(50)]
        public string C_CARDID { get; set; }

        [Column("C_SETNO")]
        [MaxLength(100)]
        public string C_SETNO { get; set; }

        [Column("C_ICTYPE")]
        [MaxLength(1)]
        public string C_ICTYPE { get; set; }

        [Column("C_CARID")]
        [MaxLength(20)]
        public string C_CARID { get; set; }

        [Column("C_CARTYPE")]
        [MaxLength(2)]
        public string C_CARTYPE { get; set; }

        [Column("D_STARTDATE")]
        public DateTime? D_STARTDATE { get; set; }

        [Column("D_STOPDATE")]
        public DateTime? D_STOPDATE { get; set; }

        [Column("D_CREATECARDDATE")]
        public DateTime? D_CREATECARDDATE { get; set; }

        [Column("C_CREATECARDMAN")]
        [MaxLength(100)]
        public string C_CREATECARDMAN { get; set; }

        [Column("C_CREATECARDSTATION")]
        [MaxLength(20)]
        public string C_CREATECARDSTATION { get; set; }

        [Column("C_CREATECARDFACTORY")]
        [MaxLength(20)]
        public string C_CREATECARDFACTORY { get; set; }

        [Column("C_CANCELCARDMAN")]
        [MaxLength(20)]
        public string C_CANCELCARDMAN { get; set; }

        [Column("C_CANCELCARDUNIT")]
        [MaxLength(20)]
        public string C_CANCELCARDUNIT { get; set; }

        [Column("D_CANCELCARDDATE")]
        public DateTime? D_CANCELCARDDATE { get; set; }

        [Column("C_CARDSTATION")]
        [MaxLength(2)]
        public string C_CARDSTATION { get; set; }

        [Column("C_CASHPLEDGE")]
        [MaxLength(10)]
        public string C_CASHPLEDGE { get; set; }

        [Column("C_MEMO")]
        [MaxLength(100)]
        public string C_MEMO { get; set; }

        [Column("C_USERUNITINFACID")]
        [MaxLength(20)]
        public string C_USERUNITINFACID { get; set; }

        [Column("C_UNITINFACDES")]
        [MaxLength(200)]
        public string C_UNITINFACDES { get; set; }

        [Column("C_UNITDES")]
        [MaxLength(200)]
        public string C_UNITDES { get; set; }

        [Column("C_USER")]
        [MaxLength(16)]
        public string C_USER { get; set; }

        [Column("C_TEL")]
        [MaxLength(20)]
        public string C_TEL { get; set; }

        [Column("C_PAPERID")]
        [MaxLength(20)]
        public string C_PAPERID { get; set; }

        [Column("C_TIMESTAMP")]
        public object C_TIMESTAMP { get; set; }

        [Column("C_DRIVER")]
        [MaxLength(30)]
        public string C_DRIVER { get; set; }

        [Column("C_DRIVERNUM")]
        [MaxLength(20)]
        public string C_DRIVERNUM { get; set; }

        [Column("C_DEL")]
        [MaxLength(1)]
        public string C_DEL { get; set; }

        [Column("C_EXTENDFIELDA")]
        [MaxLength(20)]
        public string C_EXTENDFIELDA { get; set; }

        [Column("C_EXTENDFIELDB")]
        [MaxLength(20)]
        public string C_EXTENDFIELDB { get; set; }

        [Column("C_EXTENDFIELDC")]
        [MaxLength(20)]
        public string C_EXTENDFIELDC { get; set; }

        [Column("C_MSRUPLOADFLAG")]
        [MaxLength(510)]
        public string C_MSRUPLOADFLAG { get; set; }

        [Column("C_MSRDOWNLOADFLAG")]
        [MaxLength(510)]
        public string C_MSRDOWNLOADFLAG { get; set; }

        // Navigation Properties
        // Add navigation properties here based on foreign key relationships
    }
}
