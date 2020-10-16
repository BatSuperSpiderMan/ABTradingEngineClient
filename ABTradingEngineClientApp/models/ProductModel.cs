using System;
using System.Collections.Generic;
using System.Text;

namespace ABTradingEngineClientApp.models
{
    class ProductModel
    {
        public object _id { get; set; }

        public string name { get; set; }

        public double price { get; set; }

        public string group { get; set; }

        public string owner { get; set; }

        public bool isForSale { get; set; }
    }
}
