using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SK_DataEntity.ZLEnity
{
    [Table("MES_DISPATCH_MEASURE")]
    public partial class MES_DISPATCH_MEASURE
    {
        [Column("ID")]
        [MaxLength(100)]
        public string ID { get; set; }

        [Column("SYS_ORG_CODE")]
        [MaxLength(64)]
        public string SYS_ORG_CODE { get; set; }

        [Column("CREATE_BY")]
        [MaxLength(50)]
        public string CREATE_BY { get; set; }

        [Column("CREATE_TIME")]
        public DateTime? CREATE_TIME { get; set; }

        [Column("UPDATE_BY")]
        [MaxLength(50)]
        public string UPDATE_BY { get; set; }

        [Column("UPDATE_TIME")]
        public DateTime? UPDATE_TIME { get; set; }

        [Column("DISPATCH_NUM")]
        [MaxLength(100)]
        public string DISPATCH_NUM { get; set; }

        [Column("MEASURE_NUM")]
        [MaxLength(100)]
        public string MEASURE_NUM { get; set; }

        [Column("DISPATCH_CREAT")]
        [MaxLength(100)]
        public string DISPATCH_CREAT { get; set; }

        [Column("DISPATCH_UPDATE")]
        [MaxLength(500)]
        public string DISPATCH_UPDATE { get; set; }

        [Column("DISPATCH_DEL")]
        [MaxLength(100)]
        public string DISPATCH_DEL { get; set; }

        [Column("DISPATCH_ENTRUST")]
        [MaxLength(100)]
        public string DISPATCH_ENTRUST { get; set; }

        [Column("ENTRUST_PRINT")]
        [MaxLength(500)]
        public string ENTRUST_PRINT { get; set; }

        [Column("PRINT_COUNT")]
        public int? PRINT_COUNT { get; set; }

        [Column("UPDATE_COUNT")]
        public int? UPDATE_COUNT { get; set; }

        [Column("DISPATCH_CREATE_BY")]
        [MaxLength(100)]
        public string DISPATCH_CREATE_BY { get; set; }

        [Column("DISPATCH_UPDATE_BY")]
        [MaxLength(100)]
        public string DISPATCH_UPDATE_BY { get; set; }

        [Column("DISPATCH_DEL_BY")]
        [MaxLength(100)]
        public string DISPATCH_DEL_BY { get; set; }

        [Column("DISPATCH_ENTRUST_BY")]
        [MaxLength(100)]
        public string DISPATCH_ENTRUST_BY { get; set; }

        [Column("ENTRUST_PRINT_BY")]
        [MaxLength(500)]
        public string ENTRUST_PRINT_BY { get; set; }

        [Column("LOAD_TIME")]
        [MaxLength(500)]
        public string LOAD_TIME { get; set; }

        [Column("LOAD_BY")]
        [MaxLength(500)]
        public string LOAD_BY { get; set; }

        [Column("LOAD_COUNT")]
        public int? LOAD_COUNT { get; set; }

        [Column("SALE_TYPE")]
        [MaxLength(100)]
        public string SALE_TYPE { get; set; }

        [Column("IF_PLAN")]
        [MaxLength(32)]
        public string IF_PLAN { get; set; }

        [Column("DISPATCH_PLAN_NUM")]
        [MaxLength(100)]
        public string DISPATCH_PLAN_NUM { get; set; }

        [Column("RECEIVE")]
        [MaxLength(100)]
        public string RECEIVE { get; set; }

        [Column("MATERIAL")]
        [MaxLength(100)]
        public string MATERIAL { get; set; }

        [Column("CAR_NUM")]
        [MaxLength(100)]
        public string CAR_NUM { get; set; }

        [Column("THUNDERING_DATE")]
        [MaxLength(100)]
        public string THUNDERING_DATE { get; set; }

        [Column("DEDUCTION")]
        public short? DEDUCTION { get; set; }

        [Column("ENTRUST_ID")]
        [MaxLength(100)]
        public string ENTRUST_ID { get; set; }

        [Column("ENTRUST_DEL_TIME")]
        [MaxLength(100)]
        public string ENTRUST_DEL_TIME { get; set; }

        [Column("ENTRUST_DEL")]
        [MaxLength(100)]
        public string ENTRUST_DEL { get; set; }

        [Column("ENTRUST_DEL_BY")]
        [MaxLength(100)]
        public string ENTRUST_DEL_BY { get; set; }

        [Column("ALLOW_PRINTING")]
        [MaxLength(100)]
        public string ALLOW_PRINTING { get; set; }

        // Navigation Properties
        // Add navigation properties here based on foreign key relationships
    }
}
