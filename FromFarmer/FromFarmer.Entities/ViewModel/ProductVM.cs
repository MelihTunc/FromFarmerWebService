using System;
using System.Collections.Generic;
using System.Text;

namespace FromFarmer.Entities.ViewModel
{
    public class ProductVM
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
