using System;
using System.Collections.Generic;
using System.Text;

namespace ABTradingEngineClientApp.models
{
    public class EventsModel
    {
        public object _id { get; set; }

        public double priceEffect { get; set; }

        public string name { get; set; }

        public string Scope { get; set; }

        public string affectedTradePost { get; set; }

        public string affectedProductGroups { get; set; }

        public string affectedProducts { get; set; }

      
    }
}
