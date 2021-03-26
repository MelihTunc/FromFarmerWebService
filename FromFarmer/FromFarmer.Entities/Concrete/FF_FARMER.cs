using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FromFarmer.Entities.Concrete
{
    [Table("FF_FARMER", Schema = "dbo")]
    public class FF_FARMER : BaseEntity
    {
        public int IDENTITY_NUMBER { get; set; }
        public string CITY { get; set; }
        public string DISTRICT { get; set; }
        public string TOWN_OR_VILLAGE { get; set; }
        public string NEIGHBORHOOD { get; set; }
        public string STREET { get; set; }
        public string APARTMENT { get; set; }
        public string FARMER_CERTIFICATE { get; set; }
    }
}
