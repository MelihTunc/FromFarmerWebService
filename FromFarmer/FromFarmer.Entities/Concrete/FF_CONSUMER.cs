using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FromFarmer.Entities.Concrete
{
    [Table("FF_CONSUMER", Schema = "dbo")]
    public class FF_CONSUMER : BaseEntity
    {
        public int IDENTITY_NUMBER { get; set; }
        public string CITY { get; set; }
        public string DISTRICT { get; set; }
        public string NEİGHBORHOOD { get; set; }
        public string STREET { get; set; }
        public string APARTMENT { get; set; }
        public string PHONE_NUMBER { get; set; }
    }
}
