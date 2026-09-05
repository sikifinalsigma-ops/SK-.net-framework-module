using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SK_DataEntity.Entity
{
    [Table("SK_SYS_USER")]
    public partial class SK_SYS_USER
    {
        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [MaxLength(80)]
        public string ID { get; set; }

        [Column("USER_ACCOUNT")]
        [MaxLength(40)]
        public string USER_ACCOUNT { get; set; }

        [Column("USER_NAME")]
        [MaxLength(80)]
        public string USER_NAME { get; set; }

        [Column("USER_PASSWORD")]
        [MaxLength(200)]
        public string USER_PASSWORD { get; set; }

        [Column("USER_STATUS")]
        [MaxLength(2)]
        public string USER_STATUS { get; set; }

        [Column("IS_DELETED")]
        [MaxLength(2)]
        public string IS_DELETED { get; set; }

        [Column("CREATE_BY")]
        [MaxLength(40)]
        public string CREATE_BY { get; set; }

        [Column("CREATE_TIME")]
        public DateTime? CREATE_TIME { get; set; }

        [Column("UPDATE_BY")]
        [MaxLength(40)]
        public string UPDATE_BY { get; set; }

        [Column("UPDATE_TIME")]
        public DateTime? UPDATE_TIME { get; set; }

        // Navigation Properties
        // Add navigation properties here based on foreign key relationships
    }
}
