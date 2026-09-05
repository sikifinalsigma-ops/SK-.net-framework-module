using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WY_DataEntity.Entity
{
    [Table("TM_PURCHASEORDER")]
    public partial class TM_PURCHASEORDER
    {
        [Column("C_PURCHASEORDERINFOID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [MaxLength(60)]
        public string C_PURCHASEORDERINFOID { get; set; }

        [Column("C_PURCHASEORDERID")]
        [Required]
        [MaxLength(50)]
        public string C_PURCHASEORDERID { get; set; }

        [Column("C_PURCHASEITEMID")]
        [Required]
        [MaxLength(50)]
        public string C_PURCHASEITEMID { get; set; }

        [Column("C_ORDERTYPE")]
        [MaxLength(20)]
        public string C_ORDERTYPE { get; set; }

        [Column("C_MOVETYPEID")]
        [Required]
        [MaxLength(3)]
        public string C_MOVETYPEID { get; set; }

        [Column("C_MATMAINDATAID")]
        [Required]
        [MaxLength(22)]
        public string C_MATMAINDATAID { get; set; }

        [Column("C_MATERIELID")]
        [MaxLength(44)]
        public string C_MATERIELID { get; set; }

        [Column("C_MATERIELDES")]
        [MaxLength(160)]
        public string C_MATERIELDES { get; set; }

        [Column("C_SUPPLIERID")]
        [MaxLength(10)]
        public string C_SUPPLIERID { get; set; }

        [Column("C_SUPPLIERDES")]
        [MaxLength(140)]
        public string C_SUPPLIERDES { get; set; }

        [Column("C_PURORGID")]
        [MaxLength(8)]
        public string C_PURORGID { get; set; }

        [Column("C_PURORGDES")]
        [MaxLength(80)]
        public string C_PURORGDES { get; set; }

        [Column("C_BUYERID")]
        [MaxLength(6)]
        public string C_BUYERID { get; set; }

        [Column("C_BUYERDES")]
        [MaxLength(80)]
        public string C_BUYERDES { get; set; }

        [Column("N_PURCHASEORDERQUANTITY")]
        public decimal? N_PURCHASEORDERQUANTITY { get; set; }

        [Column("C_REFMATERIELID")]
        [MaxLength(40)]
        public string C_REFMATERIELID { get; set; }

        [Column("C_ORDERFINISHQTY")]
        public decimal? C_ORDERFINISHQTY { get; set; }

        [Column("C_ORDERUNIT")]
        [MaxLength(10)]
        public string C_ORDERUNIT { get; set; }

        [Column("N_PRICEAMOUNT")]
        public decimal? N_PRICEAMOUNT { get; set; }

        [Column("C_PRICEUNIT")]
        [MaxLength(10)]
        public string C_PRICEUNIT { get; set; }

        [Column("C_FACTORYID")]
        [MaxLength(4)]
        public string C_FACTORYID { get; set; }

        [Column("C_STORAGEID")]
        [MaxLength(8)]
        public string C_STORAGEID { get; set; }

        [Column("C_TRANSPORTWAY")]
        [MaxLength(4)]
        public string C_TRANSPORTWAY { get; set; }

        [Column("C_MATERIELDIFFERENCE")]
        public decimal? C_MATERIELDIFFERENCE { get; set; }

        [Column("C_CONTRACTNO")]
        [MaxLength(100)]
        public string C_CONTRACTNO { get; set; }

        [Column("C_BALANCETIMETYPE")]
        [MaxLength(40)]
        public string C_BALANCETIMETYPE { get; set; }

        [Column("C_FINISHWAY")]
        [MaxLength(60)]
        public string C_FINISHWAY { get; set; }

        [Column("C_CLAUSENO")]
        [MaxLength(100)]
        public string C_CLAUSENO { get; set; }

        [Column("C_RATIFYMAN")]
        [MaxLength(20)]
        public string C_RATIFYMAN { get; set; }

        [Column("D_RATIFYDATE")]
        public DateTime? D_RATIFYDATE { get; set; }

        [Column("C_RATIFYFLAG")]
        [MaxLength(2)]
        public string C_RATIFYFLAG { get; set; }

        [Column("C_CLOSEFLAG")]
        [MaxLength(1)]
        public string C_CLOSEFLAG { get; set; }

        [Column("I_DELETEFLAG")]
        public long? I_DELETEFLAG { get; set; }

        [Column("C_PURCHASEID")]
        [MaxLength(40)]
        public string C_PURCHASEID { get; set; }

        [Column("C_MSRUPLOADFLAG")]
        [MaxLength(255)]
        public string C_MSRUPLOADFLAG { get; set; }

        [Column("C_MSRDOWNLOADFLAG")]
        [MaxLength(255)]
        public string C_MSRDOWNLOADFLAG { get; set; }

        [Column("C_TIMESTAMP")]
        public object C_TIMESTAMP { get; set; }

        [Column("C_EXTENDFIELDA")]
        [MaxLength(20)]
        public string C_EXTENDFIELDA { get; set; }

        [Column("C_EXTENDFIELDB")]
        [MaxLength(20)]
        public string C_EXTENDFIELDB { get; set; }

        [Column("C_EXTENDFIELDC")]
        [MaxLength(20)]
        public string C_EXTENDFIELDC { get; set; }

        [Column("C_BALANCEUNIT")]
        [MaxLength(40)]
        public string C_BALANCEUNIT { get; set; }

        [Column("C_BALANCECALCTYPE")]
        [MaxLength(40)]
        public string C_BALANCECALCTYPE { get; set; }

        [Column("C_SENDUNITID")]
        [MaxLength(24)]
        public string C_SENDUNITID { get; set; }

        [Column("C_SENDUNITDES")]
        [MaxLength(80)]
        public string C_SENDUNITDES { get; set; }

        [Column("C_SUPPLIERIDF")]
        [MaxLength(20)]
        public string C_SUPPLIERIDF { get; set; }

        [Column("C_SUPPLIERDESF")]
        [MaxLength(140)]
        public string C_SUPPLIERDESF { get; set; }

        [Column("C_LONGPROTOCOL")]
        [MaxLength(2)]
        public string C_LONGPROTOCOL { get; set; }

        [Column("C_FINANCEYEAR")]
        [MaxLength(8)]
        public string C_FINANCEYEAR { get; set; }

        [Column("C_NONCHECK")]
        [MaxLength(2)]
        public string C_NONCHECK { get; set; }

        [Column("C_EXTENDFIELDD")]
        [MaxLength(200)]
        public string C_EXTENDFIELDD { get; set; }

        [Column("C_EXTENDFIELDF")]
        [MaxLength(200)]
        public string C_EXTENDFIELDF { get; set; }

        [Column("C_EXTENDFIELDE")]
        [MaxLength(200)]
        public string C_EXTENDFIELDE { get; set; }

        [Column("C_EXTENDFIELDG")]
        [MaxLength(200)]
        public string C_EXTENDFIELDG { get; set; }

        [Column("C_EXTENDFIELDJ")]
        [MaxLength(200)]
        public string C_EXTENDFIELDJ { get; set; }

        [Column("C_CONTRACTPATH1")]
        [MaxLength(200)]
        public string C_CONTRACTPATH1 { get; set; }

        [Column("C_CONTRACTPATH2")]
        [MaxLength(200)]
        public string C_CONTRACTPATH2 { get; set; }

        [Column("C_YYID")]
        [MaxLength(40)]
        public string C_YYID { get; set; }

        // Navigation Properties
        // Add navigation properties here based on foreign key relationships
    }
}
