using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WY_DataEntity.Entity
{
    [Table("TM_INOUTRECORD")]
    public partial class TM_INOUTRECORD
    {
        [Column("C_INOUTRECORDID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [MaxLength(255)]
        public string C_INOUTRECORDID { get; set; }

        [Column("C_CARNO")]
        [MaxLength(30)]
        public string C_CARNO { get; set; }

        [Column("C_CARTYPE")]
        [MaxLength(20)]
        public string C_CARTYPE { get; set; }

        [Column("C_CARNAME")]
        [MaxLength(30)]
        public string C_CARNAME { get; set; }

        [Column("D_ENTERTIME")]
        public DateTime? D_ENTERTIME { get; set; }

        [Column("D_OUTTIME")]
        public DateTime? D_OUTTIME { get; set; }

        [Column("C_GATEINFOID")]
        [MaxLength(30)]
        public string C_GATEINFOID { get; set; }

        [Column("C_GATEDES")]
        [MaxLength(30)]
        public string C_GATEDES { get; set; }

        [Column("C_INGATEIP")]
        [MaxLength(30)]
        public string C_INGATEIP { get; set; }

        [Column("C_OUTGATEIP")]
        [MaxLength(30)]
        public string C_OUTGATEIP { get; set; }

        [Column("C_ENTERCARPICTURE")]
        [MaxLength(200)]
        public string C_ENTERCARPICTURE { get; set; }

        [Column("C_OUTCARPICTURE")]
        [MaxLength(200)]
        public string C_OUTCARPICTURE { get; set; }

        [Column("C_FINISHFLAG")]
        [MaxLength(2)]
        public string C_FINISHFLAG { get; set; }

        [Column("C_TIMESTAMP")]
        public object C_TIMESTAMP { get; set; }

        [Column("C_EXTENDFIELDA")]
        [MaxLength(200)]
        public string C_EXTENDFIELDA { get; set; }

        [Column("C_EXTENDFIELDB")]
        [MaxLength(200)]
        public string C_EXTENDFIELDB { get; set; }

        [Column("C_EXTENDFIELDC")]
        [MaxLength(200)]
        public string C_EXTENDFIELDC { get; set; }

        [Column("C_EXTENDFIELDD")]
        [MaxLength(200)]
        public string C_EXTENDFIELDD { get; set; }

        [Column("C_INOPENMAN")]
        [MaxLength(255)]
        public string C_INOPENMAN { get; set; }

        [Column("C_OUTOPENMAN")]
        [MaxLength(255)]
        public string C_OUTOPENMAN { get; set; }

        [Column("C_UPDATEMAN")]
        [MaxLength(255)]
        public string C_UPDATEMAN { get; set; }

        [Column("C_CARRECORDNO")]
        [MaxLength(255)]
        public string C_CARRECORDNO { get; set; }

        [Column("C_CARRYTOOLTYPE")]
        [MaxLength(255)]
        public string C_CARRYTOOLTYPE { get; set; }

        [Column("C_DELETEFLAG")]
        [MaxLength(255)]
        public string C_DELETEFLAG { get; set; }

        [Column("C_EMISSION")]
        [MaxLength(255)]
        public string C_EMISSION { get; set; }

        [Column("C_ENVIROPIC")]
        [MaxLength(255)]
        public string C_ENVIROPIC { get; set; }

        [Column("C_MOREMEA")]
        [MaxLength(10)]
        public string C_MOREMEA { get; set; }

        [Column("C_MOREMEAER")]
        [MaxLength(20)]
        public string C_MOREMEAER { get; set; }

        [Column("C_MORETIME")]
        [MaxLength(50)]
        public string C_MORETIME { get; set; }

        [Column("C_INNUM")]
        [MaxLength(255)]
        public string C_INNUM { get; set; }

        [Column("C_OUTNUM")]
        [MaxLength(255)]
        public string C_OUTNUM { get; set; }

        [Column("C_IGNORE")]
        [MaxLength(2)]
        public string C_IGNORE { get; set; }

        [Column("C_FACTORYID")]
        [MaxLength(255)]
        public string C_FACTORYID { get; set; }

        [Column("C_EXTENDFIELDE")]
        [MaxLength(255)]
        public string C_EXTENDFIELDE { get; set; }

        [Column("C_EXTENDFIELDF")]
        [MaxLength(255)]
        public string C_EXTENDFIELDF { get; set; }

        [Column("C_FACTORY")]
        [MaxLength(255)]
        public string C_FACTORY { get; set; }

        [Column("C_TRANSFLAG1")]
        [MaxLength(255)]
        public string C_TRANSFLAG1 { get; set; }

        [Column("C_TRANDATE1")]
        [MaxLength(255)]
        public string C_TRANDATE1 { get; set; }

        [Column("C_TRANSFLAG2")]
        [MaxLength(255)]
        public string C_TRANSFLAG2 { get; set; }

        [Column("C_TRANDATE2")]
        [MaxLength(255)]
        public string C_TRANDATE2 { get; set; }

        [Column("C_FUELTYPE")]
        [MaxLength(40)]
        public string C_FUELTYPE { get; set; }

        [Column("C_REGISTERDATE")]
        [MaxLength(100)]
        public string C_REGISTERDATE { get; set; }

        [Column("C_VIN")]
        [MaxLength(100)]
        public string C_VIN { get; set; }

        [Column("C_FDJNO")]
        [MaxLength(100)]
        public string C_FDJNO { get; set; }

        [Column("C_VEHICLEUSE")]
        [MaxLength(150)]
        public string C_VEHICLEUSE { get; set; }

        [Column("C_CARNOTYPE")]
        [MaxLength(20)]
        public string C_CARNOTYPE { get; set; }

        [Column("C_CARID")]
        [MaxLength(100)]
        public string C_CARID { get; set; }

        [Column("C_IDCARD")]
        [MaxLength(50)]
        public string C_IDCARD { get; set; }

        [Column("C_OUTNULLER")]
        [MaxLength(50)]
        public string C_OUTNULLER { get; set; }

        [Column("C_OURNULLTIME")]
        [MaxLength(50)]
        public string C_OURNULLTIME { get; set; }

        [Column("C_OUTBULLTAG")]
        [MaxLength(20)]
        public string C_OUTBULLTAG { get; set; }

        [Column("C_PRINTNUM")]
        [MaxLength(50)]
        public string C_PRINTNUM { get; set; }

        [Column("C_PRINTDATE")]
        [MaxLength(30)]
        public string C_PRINTDATE { get; set; }

        [Column("C_TRANSFLAG3")]
        [MaxLength(20)]
        public string C_TRANSFLAG3 { get; set; }

        [Column("C_TRANDATE3")]
        [MaxLength(50)]
        public string C_TRANDATE3 { get; set; }

        [Column("C_DELTIME")]
        [MaxLength(255)]
        public string C_DELTIME { get; set; }

        [Column("C_DELMAN")]
        [MaxLength(255)]
        public string C_DELMAN { get; set; }

        [Column("C_UPDATETIME")]
        [MaxLength(255)]
        public string C_UPDATETIME { get; set; }

        [Column("C_CREATETIME")]
        [MaxLength(150)]
        public string C_CREATETIME { get; set; }

        [Column("C_CREATEMAN")]
        [MaxLength(150)]
        public string C_CREATEMAN { get; set; }

        [Column("C_CHECKMAN")]
        [MaxLength(150)]
        public string C_CHECKMAN { get; set; }

        [Column("C_CHECKTIME")]
        [MaxLength(200)]
        public string C_CHECKTIME { get; set; }

        [Column("C_REASON")]
        [MaxLength(255)]
        public string C_REASON { get; set; }

        [Column("C_UPLOADFLAG")]
        [MaxLength(2)]
        public string C_UPLOADFLAG { get; set; }

        // Navigation Properties
        // Add navigation properties here based on foreign key relationships
    }
}
