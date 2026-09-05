using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YT_DataEntity.Entity
{
    [Table("TM_MEASUREMAIN")]
    public partial class TM_MEASUREMAIN
    {
        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [MaxLength(72)]
        public string ID { get; set; }

        [Column("CREATE_BY")]
        [MaxLength(100)]
        public string CREATE_BY { get; set; }

        [Column("CREATE_TIME")]
        public DateTime? CREATE_TIME { get; set; }

        [Column("UPDATE_BY")]
        [MaxLength(100)]
        public string UPDATE_BY { get; set; }

        [Column("UPDATE_TIME")]
        public DateTime? UPDATE_TIME { get; set; }

        [Column("SYS_ORG_CODE")]
        [MaxLength(128)]
        public string SYS_ORG_CODE { get; set; }

        [Column("MEASUREDOCID")]
        [MaxLength(64)]
        public string MEASUREDOCID { get; set; }

        [Column("MEASUREBEGINTIME")]
        [MaxLength(64)]
        public string MEASUREBEGINTIME { get; set; }

        [Column("CARNO")]
        [MaxLength(64)]
        public string CARNO { get; set; }

        [Column("CONSIGNMENTPLANID")]
        [MaxLength(1000)]
        public string CONSIGNMENTPLANID { get; set; }

        [Column("MEASUREPROPERTIES")]
        [MaxLength(64)]
        public string MEASUREPROPERTIES { get; set; }

        [Column("FIRSTMEASURESTATION")]
        [MaxLength(64)]
        public string FIRSTMEASURESTATION { get; set; }

        [Column("SECONDMEASURESTATION")]
        [MaxLength(64)]
        public string SECONDMEASURESTATION { get; set; }

        [Column("REPEATMEASURESTATION")]
        [MaxLength(64)]
        public string REPEATMEASURESTATION { get; set; }

        [Column("TOPMEASUREDOCID")]
        [MaxLength(64)]
        public string TOPMEASUREDOCID { get; set; }

        [Column("ANALYZETRANSACTFLAG")]
        [MaxLength(64)]
        public string ANALYZETRANSACTFLAG { get; set; }

        [Column("ANALYZETRANSACTDATE")]
        [MaxLength(64)]
        public string ANALYZETRANSACTDATE { get; set; }

        [Column("MEASUREFASHION")]
        [MaxLength(64)]
        public string MEASUREFASHION { get; set; }

        [Column("TRANSPORTTYPE")]
        [MaxLength(64)]
        public string TRANSPORTTYPE { get; set; }

        [Column("MOVETYPECODE")]
        [MaxLength(64)]
        public string MOVETYPECODE { get; set; }

        [Column("MSRFINISHFLAG")]
        [MaxLength(64)]
        public string MSRFINISHFLAG { get; set; }

        [Column("ORDERID")]
        [MaxLength(1000)]
        public string ORDERID { get; set; }

        [Column("DELIVERORDERID")]
        [MaxLength(64)]
        public string DELIVERORDERID { get; set; }

        [Column("MATERIELID")]
        [MaxLength(64)]
        public string MATERIELID { get; set; }

        [Column("MATERIELDESC")]
        [MaxLength(400)]
        public string MATERIELDESC { get; set; }

        [Column("SUPPLIERID")]
        [MaxLength(64)]
        public string SUPPLIERID { get; set; }

        [Column("SUPPLIERDESC")]
        [MaxLength(400)]
        public string SUPPLIERDESC { get; set; }

        [Column("CUSTOMERID")]
        [MaxLength(64)]
        public string CUSTOMERID { get; set; }

        [Column("CUSTOMERDESC")]
        [MaxLength(400)]
        public string CUSTOMERDESC { get; set; }

        [Column("GROSSWEIGHT")]
        public decimal? GROSSWEIGHT { get; set; }

        [Column("GROSSDATETIME")]
        [MaxLength(64)]
        public string GROSSDATETIME { get; set; }

        [Column("TAREWEIGHT")]
        public decimal? TAREWEIGHT { get; set; }

        [Column("TAREDATETIME")]
        [MaxLength(64)]
        public string TAREDATETIME { get; set; }

        [Column("NETWEIGHT")]
        public decimal? NETWEIGHT { get; set; }

        [Column("NETDATETIME")]
        [MaxLength(64)]
        public string NETDATETIME { get; set; }

        [Column("CONTAINERWEIGHT")]
        public decimal? CONTAINERWEIGHT { get; set; }

        [Column("CONTAINERDATETIME")]
        [MaxLength(64)]
        public string CONTAINERDATETIME { get; set; }

        [Column("SENDUNITID")]
        [MaxLength(64)]
        public string SENDUNITID { get; set; }

        [Column("RECEUNITID")]
        [MaxLength(64)]
        public string RECEUNITID { get; set; }

        [Column("SENDSTORAGEID")]
        [MaxLength(64)]
        public string SENDSTORAGEID { get; set; }

        [Column("RECESTORAGEID")]
        [MaxLength(64)]
        public string RECESTORAGEID { get; set; }

        [Column("STRGPLCID")]
        [MaxLength(64)]
        public string STRGPLCID { get; set; }

        [Column("AFFIRMFLAG")]
        [MaxLength(64)]
        public string AFFIRMFLAG { get; set; }

        [Column("UNLOADNAME")]
        [MaxLength(64)]
        public string UNLOADNAME { get; set; }

        [Column("UNLOADTIME")]
        [MaxLength(64)]
        public string UNLOADTIME { get; set; }

        [Column("STOW")]
        [MaxLength(64)]
        public string STOW { get; set; }

        [Column("TIER")]
        [MaxLength(64)]
        public string TIER { get; set; }

        [Column("TCOL")]
        [MaxLength(64)]
        public string TCOL { get; set; }

        [Column("TROW")]
        [MaxLength(64)]
        public string TROW { get; set; }

        [Column("INTRINSICWEIGHT")]
        public decimal? INTRINSICWEIGHT { get; set; }

        [Column("COKERATEWEIGHT")]
        public decimal? COKERATEWEIGHT { get; set; }

        [Column("PIECERATEWEIGHT")]
        public decimal? PIECERATEWEIGHT { get; set; }

        [Column("REWEIGHT")]
        public decimal? REWEIGHT { get; set; }

        [Column("REDATETIME")]
        [MaxLength(64)]
        public string REDATETIME { get; set; }

        [Column("THEORYWEIGHT")]
        public decimal? THEORYWEIGHT { get; set; }

        [Column("CARMODEL")]
        [MaxLength(64)]
        public string CARMODEL { get; set; }

        [Column("STARTSTATION")]
        [MaxLength(64)]
        public string STARTSTATION { get; set; }

        [Column("TRAINTOCARFLAG")]
        [MaxLength(64)]
        public string TRAINTOCARFLAG { get; set; }

        [Column("TRAVELTOOLNO")]
        [MaxLength(64)]
        public string TRAVELTOOLNO { get; set; }

        [Column("CHECKLOT")]
        [MaxLength(64)]
        public string CHECKLOT { get; set; }

        [Column("BRANCHQTY")]
        public int? BRANCHQTY { get; set; }

        [Column("INSTORAGEFLAG")]
        [MaxLength(64)]
        public string INSTORAGEFLAG { get; set; }

        [Column("DELREASON")]
        [MaxLength(128)]
        public string DELREASON { get; set; }

        [Column("STATE")]
        [MaxLength(64)]
        public string STATE { get; set; }

        [Column("RATIFYFLAG")]
        [MaxLength(64)]
        public string RATIFYFLAG { get; set; }

        [Column("RATIFYNAME")]
        [MaxLength(64)]
        public string RATIFYNAME { get; set; }

        [Column("RATIFYTIME")]
        public DateTime? RATIFYTIME { get; set; }

        [Column("MULITYDOCID")]
        [MaxLength(64)]
        public string MULITYDOCID { get; set; }

        [Column("BLACKALLOW")]
        [MaxLength(64)]
        public string BLACKALLOW { get; set; }

        [Column("RETURNFLAG")]
        [MaxLength(64)]
        public string RETURNFLAG { get; set; }

        [Column("MEASURESTATE")]
        [MaxLength(64)]
        public string MEASURESTATE { get; set; }

        [Column("OVERWGTALLOW")]
        [MaxLength(64)]
        public string OVERWGTALLOW { get; set; }

        [Column("OVERTIMEALLOW")]
        [MaxLength(64)]
        public string OVERTIMEALLOW { get; set; }

        [Column("OVERLOADALLOW")]
        [MaxLength(64)]
        public string OVERLOADALLOW { get; set; }

        [Column("FORBIDENPASS")]
        [MaxLength(64)]
        public string FORBIDENPASS { get; set; }

        [Column("IDENTIFICATION_ID")]
        [MaxLength(64)]
        public string IDENTIFICATION_ID { get; set; }

        [Column("LEAVEFLAG")]
        [MaxLength(64)]
        public string LEAVEFLAG { get; set; }

        [Column("SUBWEIGHT")]
        public decimal? SUBWEIGHT { get; set; }

        [Column("LOSTWEIGHTROAD")]
        public decimal? LOSTWEIGHTROAD { get; set; }

        [Column("SUBWATERWEIGHT")]
        public decimal? SUBWATERWEIGHT { get; set; }

        [Column("MSRUPLOADFLAG")]
        [MaxLength(64)]
        public string MSRUPLOADFLAG { get; set; }

        [Column("MSRDOWNLOADFLAG")]
        [MaxLength(64)]
        public string MSRDOWNLOADFLAG { get; set; }

        [Column("CARRIERID")]
        [MaxLength(64)]
        public string CARRIERID { get; set; }

        [Column("CARRIERDESC")]
        [MaxLength(64)]
        public string CARRIERDESC { get; set; }

        [Column("ARRIVALDATE")]
        public DateTime? ARRIVALDATE { get; set; }

        [Column("ELEMENTDONE")]
        [MaxLength(64)]
        public string ELEMENTDONE { get; set; }

        [Column("FACTORYID")]
        [MaxLength(64)]
        public string FACTORYID { get; set; }

        [Column("ALTERPURORDERID")]
        [MaxLength(64)]
        public string ALTERPURORDERID { get; set; }

        [Column("OFFSTATION")]
        [MaxLength(64)]
        public string OFFSTATION { get; set; }

        [Column("THROUGHFLAG")]
        [MaxLength(64)]
        public string THROUGHFLAG { get; set; }

        [Column("DISPATCHID")]
        [MaxLength(200)]
        public string DISPATCHID { get; set; }

        [Column("IDENTIFICATIONID")]
        [MaxLength(64)]
        public string IDENTIFICATIONID { get; set; }

        [Column("PROCESSLINE")]
        [MaxLength(64)]
        public string PROCESSLINE { get; set; }

        [Column("CURRENTPROCESSCODE")]
        [MaxLength(64)]
        public string CURRENTPROCESSCODE { get; set; }

        [Column("BUSINESSCODE")]
        [MaxLength(64)]
        public string BUSINESSCODE { get; set; }

        [Column("PAUSEPROCESS")]
        [MaxLength(64)]
        public string PAUSEPROCESS { get; set; }

        [Column("FACTORYDESC")]
        [MaxLength(400)]
        public string FACTORYDESC { get; set; }

        [Column("TIMESTAMP")]
        [MaxLength(64)]
        public string TIMESTAMP { get; set; }

        [Column("ISSUED")]
        [MaxLength(64)]
        public string ISSUED { get; set; }

        [Column("BALANCEID")]
        [MaxLength(64)]
        public string BALANCEID { get; set; }

        [Column("UNLOADTYPE")]
        [MaxLength(64)]
        public string UNLOADTYPE { get; set; }

        [Column("TAREALLOW")]
        [MaxLength(64)]
        public string TAREALLOW { get; set; }

        [Column("PAY")]
        [MaxLength(64)]
        public string PAY { get; set; }

        [Column("SAMPLING")]
        [MaxLength(64)]
        public string SAMPLING { get; set; }

        [Column("FORCEMEASURE")]
        [MaxLength(4)]
        public string FORCEMEASURE { get; set; }

        [Column("PROFITLOSSWGT")]
        public decimal? PROFITLOSSWGT { get; set; }

        [Column("IMPURITY")]
        public decimal? IMPURITY { get; set; }

        [Column("NETRECEIPTS")]
        public decimal? NETRECEIPTS { get; set; }

        [Column("SECONDARYCODE")]
        [MaxLength(64)]
        public string SECONDARYCODE { get; set; }

        [Column("SECONDARYNAME")]
        [MaxLength(64)]
        public string SECONDARYNAME { get; set; }

        [Column("CUSTOMERTYPE")]
        [MaxLength(64)]
        public string CUSTOMERTYPE { get; set; }

        [Column("ENDSTATION")]
        [MaxLength(64)]
        public string ENDSTATION { get; set; }

        [Column("SERIAL")]
        [MaxLength(64)]
        public string SERIAL { get; set; }

        [Column("BALANCESEQID")]
        [MaxLength(64)]
        public string BALANCESEQID { get; set; }

        [Column("TRACKLINE")]
        [MaxLength(200)]
        public string TRACKLINE { get; set; }

        [Column("TRACKSUM")]
        public decimal? TRACKSUM { get; set; }

        [Column("TRACKID")]
        [MaxLength(400)]
        public string TRACKID { get; set; }

        [Column("TRACKCOUNT")]
        [MaxLength(200)]
        public string TRACKCOUNT { get; set; }

        [Column("TRACKSPD")]
        [MaxLength(64)]
        public string TRACKSPD { get; set; }

        [Column("CHANGELOG")]
        [MaxLength(64)]
        public string CHANGELOG { get; set; }

        [Column("FREIGHT")]
        [MaxLength(64)]
        public string FREIGHT { get; set; }

        [Column("CHECKFLAG")]
        [MaxLength(4)]
        public string CHECKFLAG { get; set; }

        [Column("VGROSSWEIGHT")]
        [MaxLength(64)]
        public string VGROSSWEIGHT { get; set; }

        [Column("VTAREWEIGHT")]
        public decimal? VTAREWEIGHT { get; set; }

        [Column("VTAREDATETIME")]
        [MaxLength(64)]
        public string VTAREDATETIME { get; set; }

        [Column("VGROSSDATETIME")]
        [MaxLength(64)]
        public string VGROSSDATETIME { get; set; }

        [Column("STRGPLCID1")]
        [MaxLength(64)]
        public string STRGPLCID1 { get; set; }

        [Column("UNLOADTIME1")]
        [MaxLength(64)]
        public string UNLOADTIME1 { get; set; }

        [Column("UNLOADTYPE1")]
        [MaxLength(64)]
        public string UNLOADTYPE1 { get; set; }

        [Column("UNLOADNAME1")]
        [MaxLength(64)]
        public string UNLOADNAME1 { get; set; }

        [Column("NOTES")]
        public string NOTES { get; set; }

        [Column("UNLOADSUBWATERWEIGHT")]
        public decimal? UNLOADSUBWATERWEIGHT { get; set; }

        [Column("INTERNALCARNO")]
        [MaxLength(32)]
        public string INTERNALCARNO { get; set; }

        [Column("SAMPLINGTIME")]
        public DateTime? SAMPLINGTIME { get; set; }

        [Column("PERSON_UPDATE_TIME")]
        public DateTime? PERSON_UPDATE_TIME { get; set; }

        [Column("PERSON_UPDATE_BY")]
        [MaxLength(32)]
        public string PERSON_UPDATE_BY { get; set; }

        [Column("IS_UPDATE")]
        [MaxLength(32)]
        public string IS_UPDATE { get; set; }

        [Column("FULL_RETURN")]
        [MaxLength(32)]
        public string FULL_RETURN { get; set; }

        [Column("PUSHSTATUS")]
        [MaxLength(50)]
        public string PUSHSTATUS { get; set; }

        [Column("C_TRANSFLAG")]
        [MaxLength(4)]
        public string C_TRANSFLAG { get; set; }

        [Column("C_TRANSTIME")]
        [MaxLength(20)]
        public string C_TRANSTIME { get; set; }

        [Column("C_TRANSERPID")]
        [MaxLength(30)]
        public string C_TRANSERPID { get; set; }

        [Column("C_TRANSERPINID")]
        [MaxLength(30)]
        public string C_TRANSERPINID { get; set; }

        // Navigation Properties
        // Add navigation properties here based on foreign key relationships
    }
}
