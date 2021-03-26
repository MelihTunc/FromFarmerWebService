using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FromFarmer.Entities.Concrete
{
    [Table("FF_ORDER", Schema = "dbo")]
    public class FF_ORDER : BaseEntity
    {
        public int IDENTITY_NUMBER { get; set; }
        public string STATUS { get; set; }
        public int CONSUMER_ID { get; set; }
        public DateTime? DATE_TİME { get; set; }
        public string DELIVERY_ADDRESS { get; set; }
        public string PAYMENT_INFORMATION { get; set; }
        public string SHIPPING_FREE { get; set; }
        public int TOTAL_PRICE { get; set; }
        public string SHIPPING_TRACKING_NUMBER { get; set; }
    }
}
