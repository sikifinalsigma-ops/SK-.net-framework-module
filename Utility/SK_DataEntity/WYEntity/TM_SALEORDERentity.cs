using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WY_DataEntity.Entity
{
    [Table("TM_SALEORDER")]
    public partial class TM_SALEORDER
    {
        [Column("C_SALEORDERMAINID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [MaxLength(80)]
        public string C_SALEORDERMAINID { get; set; }

        [Column("C_SALESORDERID")]
        [MaxLength(40)]
        public string C_SALESORDERID { get; set; }

        [Column("C_SALESITEMID")]
        [MaxLength(40)]
        public string C_SALESITEMID { get; set; }

        [Column("C_MATMAINDATAID")]
        [MaxLength(44)]
        public string C_MATMAINDATAID { get; set; }

        [Column("C_MATERIALDES")]
        [Required]
        [MaxLength(80)]
        public string C_MATERIALDES { get; set; }

        [Column("N_ORDERQTY")]
        public decimal? N_ORDERQTY { get; set; }

        [Column("N_ORDERFINISHQTY")]
        public decimal? N_ORDERFINISHQTY { get; set; }

        [Column("C_WEIGHTUNIT")]
        [MaxLength(6)]
        public string C_WEIGHTUNIT { get; set; }

        [Column("C_FACTORYDESC")]
        [MaxLength(40)]
        public string C_FACTORYDESC { get; set; }

        [Column("C_CLIENTID")]
        [Required]
        [MaxLength(20)]
        public string C_CLIENTID { get; set; }

        [Column("C_SHIPPINGTYPEID")]
        [MaxLength(4)]
        public string C_SHIPPINGTYPEID { get; set; }

        [Column("C_DELIVERYTYPEID")]
        [MaxLength(8)]
        public string C_DELIVERYTYPEID { get; set; }

        [Column("C_DELIVERYDATE")]
        public DateTime? C_DELIVERYDATE { get; set; }

        [Column("C_ARRIVESTATION")]
        [MaxLength(60)]
        public string C_ARRIVESTATION { get; set; }

        [Column("C_CLOSEFLAG")]
        [MaxLength(2)]
        public string C_CLOSEFLAG { get; set; }

        [Column("C_LOGICDELETEFLAG")]
        [MaxLength(2)]
        public string C_LOGICDELETEFLAG { get; set; }

        [Column("C_MSRUPLOADFLAG")]
        [MaxLength(510)]
        public string C_MSRUPLOADFLAG { get; set; }

        [Column("C_MSRDOWNLOADFLAG")]
        [MaxLength(510)]
        public string C_MSRDOWNLOADFLAG { get; set; }

        [Column("C_TIMESTAMP")]
        public object C_TIMESTAMP { get; set; }

        [Column("C_EXTENDFIELDA")]
        [MaxLength(40)]
        public string C_EXTENDFIELDA { get; set; }

        [Column("C_EXTENDFIELDB")]
        [MaxLength(40)]
        public string C_EXTENDFIELDB { get; set; }

        [Column("C_EXTENDFIELDC")]
        [MaxLength(40)]
        public string C_EXTENDFIELDC { get; set; }

        [Column("C_MATERIELID")]
        [MaxLength(36)]
        public string C_MATERIELID { get; set; }

        [Column("C_CLIENTDESC")]
        [MaxLength(100)]
        public string C_CLIENTDESC { get; set; }

        [Column("C_CHECKFLAG")]
        [MaxLength(2)]
        public string C_CHECKFLAG { get; set; }

        [Column("C_CHECKER")]
        [MaxLength(20)]
        public string C_CHECKER { get; set; }

        [Column("D_CHECKDATE")]
        public DateTime? D_CHECKDATE { get; set; }

        [Column("C_CONTRACTNO")]
        [MaxLength(40)]
        public string C_CONTRACTNO { get; set; }

        [Column("N_PRICE")]
        public decimal? N_PRICE { get; set; }

        [Column("I_DRYBASISBALANCE")]
        [MaxLength(2)]
        public string I_DRYBASISBALANCE { get; set; }

        [Column("C_LEASEDLINE")]
        [MaxLength(100)]
        public string C_LEASEDLINE { get; set; }

        [Column("C_FACTORYID")]
        [Required]
        [MaxLength(40)]
        public string C_FACTORYID { get; set; }

        [Column("C_STORAGEID")]
        [MaxLength(40)]
        public string C_STORAGEID { get; set; }

        [Column("C_STORAGEDESC")]
        [MaxLength(80)]
        public string C_STORAGEDESC { get; set; }

        [Column("C_BALANCEUNIT")]
        [MaxLength(40)]
        public string C_BALANCEUNIT { get; set; }

        [Column("D_CREATETIME")]
        public DateTime? D_CREATETIME { get; set; }

        [Column("N_TOTALPRICE")]
        public decimal? N_TOTALPRICE { get; set; }

        [Column("N_JJPRICE")]
        public decimal? N_JJPRICE { get; set; }

        [Column("D_UPDATAORDER")]
        public DateTime? D_UPDATAORDER { get; set; }

        [Column("C_MATERIALID")]
        [MaxLength(80)]
        public string C_MATERIALID { get; set; }

        [Column("C_YYID")]
        [MaxLength(80)]
        public string C_YYID { get; set; }

        // Navigation Properties
        // Add navigation properties here based on foreign key relationships
    }
}
