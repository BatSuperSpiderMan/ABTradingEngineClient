using ABTradingEngineClientApp.models;
using ABTradingEngineClientApp.services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ABTradingEngineClientApp.controller
{
    class EventsController
    {
        ABEngineService engineService = new ABEngineService();
        List<EventsModel> _marketEvents;
        List<ProductModel> _products;

        public EventsController()
        {
            _marketEvents = engineService.GetEvents();
            _products = engineService.GetProducts();
        }

        public List<EventsModel> ListMarketEvents()
        {
            return _marketEvents;
        }

        public List<ProductModel> ListProducts()
        {
            return _products;
        }
    }
}
