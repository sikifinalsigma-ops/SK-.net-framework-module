using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WY_DataEntity.Entity
{
    [Table("TM_MEASUREMAIN")]
    public partial class TM_MEASUREMAIN
    {
        [Column("C_MEASUREDOCID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [MaxLength(60)]
        public string C_MEASUREDOCID { get; set; }

        [Column("C_MEASUREBEGINDATE")]
        [MaxLength(38)]
        public string C_MEASUREBEGINDATE { get; set; }

        [Column("C_CARRYTOOLMAINID")]
        [MaxLength(20)]
        public string C_CARRYTOOLMAINID { get; set; }

        [Column("C_MEASUREPROPERTIES")]
        [MaxLength(20)]
        public string C_MEASUREPROPERTIES { get; set; }

        [Column("C_CONTAINERID")]
        [MaxLength(20)]
        public string C_CONTAINERID { get; set; }

        [Column("C_FMEASUREDOCID")]
        [MaxLength(40)]
        public string C_FMEASUREDOCID { get; set; }

        [Column("I_ANALYZETRANSACTFLAG")]
        public long? I_ANALYZETRANSACTFLAG { get; set; }

        [Column("C_ANALYZETRANSACTDATE")]
        [MaxLength(38)]
        public string C_ANALYZETRANSACTDATE { get; set; }

        [Column("I_MEASUREFASHION")]
        [MaxLength(40)]
        public string I_MEASUREFASHION { get; set; }

        [Column("C_CARRYTOOLSTYPEID")]
        [MaxLength(4)]
        public string C_CARRYTOOLSTYPEID { get; set; }

        [Column("C_GOODSMOVETYPE")]
        [MaxLength(6)]
        public string C_GOODSMOVETYPE { get; set; }

        [Column("I_MSRFINISHFLAG")]
        public long? I_MSRFINISHFLAG { get; set; }

        [Column("C_ORDERID")]
        [MaxLength(100)]
        public string C_ORDERID { get; set; }

        [Column("N_GROSSWEIGHT")]
        public decimal? N_GROSSWEIGHT { get; set; }

        [Column("C_GROSSDATETIME")]
        [MaxLength(38)]
        public string C_GROSSDATETIME { get; set; }

        [Column("N_TAREWEIGHT")]
        public decimal? N_TAREWEIGHT { get; set; }

        [Column("C_TAREDATETIME")]
        [MaxLength(38)]
        public string C_TAREDATETIME { get; set; }

        [Column("N_CONTAINERWEIGHT")]
        public decimal? N_CONTAINERWEIGHT { get; set; }

        [Column("D_CONTAINERDATE")]
        [MaxLength(38)]
        public string D_CONTAINERDATE { get; set; }

        [Column("N_NETWEIGHT")]
        public decimal? N_NETWEIGHT { get; set; }

        [Column("D_NETDATETIME")]
        [MaxLength(38)]
        public string D_NETDATETIME { get; set; }

        [Column("C_SENDSTORAGEID")]
        [MaxLength(40)]
        public string C_SENDSTORAGEID { get; set; }

        [Column("C_STORAGEINFOID")]
        [MaxLength(16)]
        public string C_STORAGEINFOID { get; set; }

        [Column("C_STRGPLCID")]
        [MaxLength(20)]
        public string C_STRGPLCID { get; set; }

        [Column("C_AFFIRMFLAG")]
        [MaxLength(4)]
        public string C_AFFIRMFLAG { get; set; }

        [Column("C_UNLOADCLASS")]
        [MaxLength(40)]
        public string C_UNLOADCLASS { get; set; }

        [Column("C_UNLOADMAN")]
        [MaxLength(1998)]
        public string C_UNLOADMAN { get; set; }

        [Column("D_UNLOADDATE")]
        public DateTime? D_UNLOADDATE { get; set; }

        [Column("C_UPLOADPHOTO")]
        [MaxLength(510)]
        public string C_UPLOADPHOTO { get; set; }

        [Column("C_STOW")]
        [MaxLength(24)]
        public string C_STOW { get; set; }

        [Column("C_TIER")]
        [MaxLength(60)]
        public string C_TIER { get; set; }

        [Column("C_COL")]
        [MaxLength(20)]
        public string C_COL { get; set; }

        [Column("N_INTRINSICWEIGHT")]
        public decimal? N_INTRINSICWEIGHT { get; set; }

        [Column("N_THEORYWEIGHT")]
        public decimal? N_THEORYWEIGHT { get; set; }

        [Column("D_ARRIVALDATE")]
        public DateTime? D_ARRIVALDATE { get; set; }

        [Column("C_SENDUNIT")]
        [MaxLength(36)]
        public string C_SENDUNIT { get; set; }

        [Column("C_RECEIVEUNITID")]
        [MaxLength(40)]
        public string C_RECEIVEUNITID { get; set; }

        [Column("C_MATMAINDATAID")]
        [MaxLength(44)]
        public string C_MATMAINDATAID { get; set; }

        [Column("C_MATERIELID")]
        [MaxLength(36)]
        public string C_MATERIELID { get; set; }

        [Column("C_MATERIELDES")]
        [MaxLength(160)]
        public string C_MATERIELDES { get; set; }

        [Column("C_SENDBATCHID")]
        [MaxLength(40)]
        public string C_SENDBATCHID { get; set; }

        [Column("C_INLOT")]
        [MaxLength(40)]
        public string C_INLOT { get; set; }

        [Column("C_STOVESITE")]
        [MaxLength(40)]
        public string C_STOVESITE { get; set; }

        [Column("C_STOVELOT")]
        [MaxLength(40)]
        public string C_STOVELOT { get; set; }

        [Column("D_OUTIRONDATE")]
        public DateTime? D_OUTIRONDATE { get; set; }

        [Column("C_RECEIVEBATCHID")]
        [MaxLength(40)]
        public string C_RECEIVEBATCHID { get; set; }

        [Column("C_CARMODEL")]
        [MaxLength(20)]
        public string C_CARMODEL { get; set; }

        [Column("C_STARTSTATION")]
        [MaxLength(40)]
        public string C_STARTSTATION { get; set; }

        [Column("C_CHECKLOT")]
        [MaxLength(60)]
        public string C_CHECKLOT { get; set; }

        [Column("C_BALANCENO")]
        [MaxLength(2)]
        public string C_BALANCENO { get; set; }

        [Column("C_COSTCENTERID")]
        [MaxLength(40)]
        public string C_COSTCENTERID { get; set; }

        [Column("I_BRANCHQTY")]
        public long? I_BRANCHQTY { get; set; }

        [Column("C_ENTRUSTFACTORY")]
        [MaxLength(40)]
        public string C_ENTRUSTFACTORY { get; set; }

        [Column("C_MAINTAINER")]
        [MaxLength(40)]
        public string C_MAINTAINER { get; set; }

        [Column("D_SENDDATE")]
        public DateTime? D_SENDDATE { get; set; }

        [Column("C_NETWEIGHTSTATION")]
        [MaxLength(40)]
        public string C_NETWEIGHTSTATION { get; set; }

        [Column("C_REMARK")]
        [MaxLength(200)]
        public string C_REMARK { get; set; }

        [Column("C_RETURNFLAG")]
        [MaxLength(2)]
        public string C_RETURNFLAG { get; set; }

        [Column("C_TRAINTOCARFLAG")]
        [MaxLength(2)]
        public string C_TRAINTOCARFLAG { get; set; }

        [Column("C_ALTERPURCHASEID")]
        [MaxLength(40)]
        public string C_ALTERPURCHASEID { get; set; }

        [Column("C_INSTORAGEFLAG")]
        [MaxLength(2)]
        public string C_INSTORAGEFLAG { get; set; }

        [Column("C_SUPPLIERID")]
        [MaxLength(20)]
        public string C_SUPPLIERID { get; set; }

        [Column("C_SUPPLYIERDES")]
        [MaxLength(120)]
        public string C_SUPPLYIERDES { get; set; }

        [Column("C_CLIENTID")]
        [MaxLength(20)]
        public string C_CLIENTID { get; set; }

        [Column("C_CLIENTDES")]
        [MaxLength(120)]
        public string C_CLIENTDES { get; set; }

        [Column("C_REFMATERIELID")]
        [MaxLength(40)]
        public string C_REFMATERIELID { get; set; }

        [Column("C_DELREASON")]
        [MaxLength(400)]
        public string C_DELREASON { get; set; }

        [Column("C_STATE")]
        [MaxLength(2)]
        public string C_STATE { get; set; }

        [Column("C_TIMESTAMP")]
        public object C_TIMESTAMP { get; set; }

        [Column("C_MSRUPLOADFLAG")]
        [MaxLength(510)]
        public string C_MSRUPLOADFLAG { get; set; }

        [Column("C_MSRDOWNLOADFLAG")]
        [MaxLength(510)]
        public string C_MSRDOWNLOADFLAG { get; set; }

        [Column("C_EXTENDFIELDA")]
        [MaxLength(40)]
        public string C_EXTENDFIELDA { get; set; }

        [Column("C_EXTENDFIELDB")]
        [MaxLength(40)]
        public string C_EXTENDFIELDB { get; set; }

        [Column("C_EXTENDFIELDC")]
        [MaxLength(40)]
        public string C_EXTENDFIELDC { get; set; }

        [Column("C_EXTENDFIELDD")]
        [MaxLength(510)]
        public string C_EXTENDFIELDD { get; set; }

        [Column("C_EXTENDFIELDE")]
        [MaxLength(510)]
        public string C_EXTENDFIELDE { get; set; }

        [Column("C_EXTENDFIELDF")]
        [MaxLength(510)]
        public string C_EXTENDFIELDF { get; set; }

        [Column("C_EXTENDFIELDG")]
        [MaxLength(510)]
        public string C_EXTENDFIELDG { get; set; }

        [Column("C_EXTENDFIELDH")]
        [MaxLength(510)]
        public string C_EXTENDFIELDH { get; set; }

        [Column("C_ROW")]
        [MaxLength(20)]
        public string C_ROW { get; set; }

        [Column("C_CALCDRYBASISFLAG")]
        [MaxLength(2)]
        public string C_CALCDRYBASISFLAG { get; set; }

        [Column("C_THROUGHFLAG")]
        [MaxLength(2)]
        public string C_THROUGHFLAG { get; set; }

        [Column("C_YJBALANCEID")]
        [MaxLength(40)]
        public string C_YJBALANCEID { get; set; }

        [Column("D_CREATETIME")]
        public DateTime? D_CREATETIME { get; set; }

        [Column("C_LOCATIONID")]
        [MaxLength(40)]
        public string C_LOCATIONID { get; set; }

        [Column("N_ITAREWEIGHT")]
        public decimal? N_ITAREWEIGHT { get; set; }

        [Column("N_IGROSSWEIGHT")]
        public decimal? N_IGROSSWEIGHT { get; set; }

        [Column("N_SUBWATER")]
        public decimal? N_SUBWATER { get; set; }

        [Column("N_SUPPLYWEIGHT")]
        public decimal? N_SUPPLYWEIGHT { get; set; }

        [Column("N_PIECERATE")]
        public decimal? N_PIECERATE { get; set; }

        [Column("N_COKERATE")]
        public decimal? N_COKERATE { get; set; }

        [Column("N_FREIGHT_CAR")]
        public decimal? N_FREIGHT_CAR { get; set; }

        [Column("N_FREIGHT")]
        public decimal? N_FREIGHT { get; set; }

        [Column("N_FACTWEIGHT")]
        public decimal? N_FACTWEIGHT { get; set; }

        [Column("N_LOSTWEIGHT")]
        public decimal? N_LOSTWEIGHT { get; set; }

        [Column("N_IMPURERATE")]
        public decimal? N_IMPURERATE { get; set; }

        [Column("N_SUBWEIGHT")]
        public decimal? N_SUBWEIGHT { get; set; }

        [Column("N_SUBWEIGHTRATE")]
        public decimal? N_SUBWEIGHTRATE { get; set; }

        [Column("C_STORAGETYPE")]
        [MaxLength(40)]
        public string C_STORAGETYPE { get; set; }

        [Column("C_CARTYPE")]
        [MaxLength(40)]
        public string C_CARTYPE { get; set; }

        [Column("C_OFFSTATION")]
        [MaxLength(40)]
        public string C_OFFSTATION { get; set; }

        [Column("C_EXTENDFIELDI")]
        [MaxLength(40)]
        public string C_EXTENDFIELDI { get; set; }

        [Column("C_EXTENDFIELDJ")]
        [MaxLength(200)]
        public string C_EXTENDFIELDJ { get; set; }

        [Column("C_EXTENDFIELDK")]
        [MaxLength(510)]
        public string C_EXTENDFIELDK { get; set; }

        [Column("N_PRINTNUM")]
        public short? N_PRINTNUM { get; set; }

        [Column("CONFIRMATIONSINGLESTATE")]
        [MaxLength(510)]
        public string CONFIRMATIONSINGLESTATE { get; set; }

        [Column("C_ACCOUNTTYPE")]
        [MaxLength(510)]
        public string C_ACCOUNTTYPE { get; set; }

        [Column("C_EXTENDFIELDL")]
        [MaxLength(510)]
        public string C_EXTENDFIELDL { get; set; }

        [Column("C_EXTENDFIELDM")]
        [MaxLength(510)]
        public string C_EXTENDFIELDM { get; set; }

        [Column("C_EXTENDFIELDN")]
        [MaxLength(510)]
        public string C_EXTENDFIELDN { get; set; }

        [Column("C_EXTENDFIELDO")]
        [MaxLength(510)]
        public string C_EXTENDFIELDO { get; set; }

        [Column("C_EXTENDFIELDP")]
        [MaxLength(510)]
        public string C_EXTENDFIELDP { get; set; }

        [Column("C_EXTENDFIELDQ")]
        [MaxLength(510)]
        public string C_EXTENDFIELDQ { get; set; }

        [Column("C_CHECKID")]
        [MaxLength(20)]
        public string C_CHECKID { get; set; }

        [Column("C_PRINTNO")]
        [MaxLength(20)]
        public string C_PRINTNO { get; set; }

        [Column("C_YFBALANCEID")]
        [MaxLength(20)]
        public string C_YFBALANCEID { get; set; }

        [Column("C_WASHFLAG")]
        [MaxLength(2)]
        public string C_WASHFLAG { get; set; }

        [Column("C_WASHDATE")]
        public DateTime? C_WASHDATE { get; set; }

        [Column("C_LOADTIME")]
        [MaxLength(40)]
        public string C_LOADTIME { get; set; }

        [Column("C_LOADFLAG")]
        [MaxLength(4)]
        public string C_LOADFLAG { get; set; }

        [Column("C_LOADIMGPATH")]
        [MaxLength(160)]
        public string C_LOADIMGPATH { get; set; }

        [Column("C_LOADIMGPATH2")]
        [MaxLength(160)]
        public string C_LOADIMGPATH2 { get; set; }

        [Column("C_LOADIMGPATH3")]
        [MaxLength(160)]
        public string C_LOADIMGPATH3 { get; set; }

        [Column("C_LOADIMGFLAG")]
        [MaxLength(4)]
        public string C_LOADIMGFLAG { get; set; }

        [Column("C_LOADIMGTIME")]
        [MaxLength(40)]
        public string C_LOADIMGTIME { get; set; }

        [Column("C_TRANSFLAG")]
        [MaxLength(4)]
        public string C_TRANSFLAG { get; set; }

        [Column("C_TRANSTIME")]
        [MaxLength(40)]
        public string C_TRANSTIME { get; set; }

        // Navigation Properties
        // Add navigation properties here based on foreign key relationships
    }
}
