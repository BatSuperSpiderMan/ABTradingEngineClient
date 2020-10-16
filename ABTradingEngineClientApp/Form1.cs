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
    public partial class Form1 : Form
    {
        
        ABEngineService engineService = new ABEngineService();
        TradeController trade = new TradeController();
        UserModel buyer, seller;
        List<ProductModel> productsAvailable = new List<ProductModel>();
        List<ProductModel> productsOwned = new List<ProductModel>();
        private int _ticks;

        public Form1()
        {
            InitializeComponent();
            label3.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {         
            if (this.listView1.SelectedItems.Count == 0)
                return;

            int namn = this.listView1.SelectedItems[0].Index;
            ProductModel boughtProd = productsAvailable[namn];

            DialogResult dialogResult = MessageBox.Show("Would you like to buy: " + boughtProd.name, "Purchase Confirmation", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                
                

                seller = engineService.GetUser(boughtProd.owner.ToString());

                TradeController trade = new TradeController(boughtProd,buyer,seller);

                trade.initTrade();
                RefreshView();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
            
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           RefreshView();
            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView2.SelectedItems.Count == 0)
                return;

            int namn = this.listView2.SelectedItems[0].Index;
            ProductModel prodForSale = productsOwned[namn];

            if (prodForSale.isForSale == true)
            {
                DialogResult dialogResult = MessageBox.Show("Would you like to place " + prodForSale.name + " off the market? ", "Remove product from market?", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    trade.NoSale(prodForSale);
                }
            }
            else 
            {
                DialogResult dialogResult = MessageBox.Show("Would you like to place " + prodForSale.name + " on the market? ", "Place product on the market?", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    trade.ForSale(prodForSale);
                }

            }

                RefreshView();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _ticks++;
            label5.Text = "Game Timer: "+_ticks.ToString();

            if (_ticks == 20)
            {
                label6.Text = "Stage 2";
            }
            else if (_ticks == 40)
            {
                label6.Text = "Stage 3";
            }
           
            else if (_ticks == 60)
            {
                timer1.Stop();
                label6.Text = "Game Over";

                var winner = trade.getTheWinner();

                
                MessageBox.Show("Winner of the game is: "+winner.name +" with R"+ winner.wallet, "Winner Announcement");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void RefreshView()
        {
            if (textBox1.Text == null || textBox1.Text == "")
                throw new ArgumentNullException("Null value submitted");

            buyer = engineService.GetUser(textBox1.Text);

            if (buyer == null)
                throw new ArgumentNullException("No matching user found in the DB");

            button1.Hide();
            textBox1.Hide();
            label3.Text = "Logged in as: " + buyer.name;
            label3.Show();

            label4.Text = "Products owned by: " + buyer.name;

            label2.Text = buyer.wallet.ToString();
            listView1.Items.Clear();
            listView2.Items.Clear();
            listView1.View = View.List;
            listView2.View = View.List;
            foreach (var product in engineService.GetProducts())
            {
                if (buyer._id.ToString() != product.owner)
                {
                    if (product.isForSale == true)
                    {
                        productsAvailable.Add(product);
                        listView1.Items.Add(product.name + "    :   R" + product.price);
                    }
                }
                else
                {
                    productsOwned.Add(product);
                    listView2.Items.Add(product.name + "    :   R" + product.price);

                }

            }

        }
    }
}
