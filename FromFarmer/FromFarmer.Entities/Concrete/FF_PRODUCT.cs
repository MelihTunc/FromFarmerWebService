using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FromFarmer.Entities.Concrete
{
    [Table("FF_PRODUCT", Schema = "dbo")]
    public class FF_PRODUCT : BaseEntity
    {
        public int IDENTITY_NUMBER { get; set; }
        public string TYPE { get; set; }
        public string CATEGORY { get; set; }
        public string TITLE { get; set; }
        public string DESCRIPTION { get; set; }
        public int FARMER_ID { get; set; }
        public int PRICE { get; set; }
        public int QUANTITY { get; set; }
        
    }
}
