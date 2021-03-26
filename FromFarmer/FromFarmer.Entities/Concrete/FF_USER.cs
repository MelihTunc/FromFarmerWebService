using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FromFarmer.Entities.Concrete
{
    [Table("FF_USER", Schema = "dbo")]
    public class FF_USER : BaseEntity
    {
        public int IDENTITY_NUMBER { get; set; }
        public string FIRSTNAME { get; set; }
        public string LASTNAME { get; set; }
        public int GENDER { get; set; }
        public DateTime? DATE_OF_BIRTH { get; set; }
        public string MAIL_ADDRESS { get; set; }
        public string PHONE_NUMBER { get; set; }
        public string WORK_ADDRESS { get; set; }
        public string PASSWORD { get; set; }
        public int? IS_FARMER { get; set; }
    }
}
