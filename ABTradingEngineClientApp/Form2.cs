using ABTradingEngineClientApp.controller;
using ABTradingEngineClientApp.models;
using ABTradingEngineClientApp.services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABTradingEngineClientApp
{
    public partial class Form2 : Form
    {
        private int _ticks; 
        
        List<EventsModel> marketEvents;
        EventsController eventsController = new EventsController();
        ABEngineService engineService = new ABEngineService();


        public Form2()
        {
            InitializeComponent();
            PopulateView();
            timer1.Start();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (this.listView1.SelectedItems.Count == 0)
                return;

            int chosenIndex = this.listView1.SelectedItems[0].Index;
            var eventToDeploy = marketEvents[chosenIndex];

            deployEvent(eventToDeploy);
        }

        public void deployEvent(EventsModel occasion)
        {
            double eventPrice =0.0;

            if (occasion.affectedProductGroups == "All" || occasion.affectedProducts == "All")
            {
                double toMutiply = occasion.priceEffect / 100;


                foreach (var product in eventsController.ListProducts())
                {
                    var productPrice = product.price;
                    productPrice = productPrice+(product.price * toMutiply);
                    engineService.UpdateProduct(product._id, product.name, productPrice, product.group, product.owner, product.isForSale);
                }

                this.Close();
            }
            else
            {
                double toMutiply = occasion.priceEffect / 100;

                foreach (var product in eventsController.ListProducts())
                {
                    if (occasion.affectedProductGroups == product.group) 
                    {
                        var productPrice = product.price;
                        productPrice = productPrice + (product.price * toMutiply);
                        engineService.UpdateProduct(product._id, product.name, productPrice, product.group, product.owner, product.isForSale);
                    }

                }

            }            
        }



        public void PopulateView()
        {
            marketEvents = eventsController.ListMarketEvents();
            listView1.View = View.List;
            foreach (var marketEvent in marketEvents)
            {
                double priceEffect = marketEvent.priceEffect;
                listView1.Items.Add(marketEvent.name + ":   "+ marketEvent.affectedProductGroups + ":  " + priceEffect + "%");
            }

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _ticks++;
        }
    }
}
