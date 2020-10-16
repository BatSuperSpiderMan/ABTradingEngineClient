using ABTradingEngineClientApp.models;
using ABTradingEngineClientApp.services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ABTradingEngineClientApp.controller
{
    class TradeController
    {
        UserModel winner = new UserModel();
        ABEngineService engineService = new ABEngineService();
        ProductModel _boughtProduct;
        UserModel _buyer;
        UserModel _seller;
       

        public TradeController()
        {

        }

        public TradeController(ProductModel boughtProduct, UserModel buyer, UserModel seller)
        {
            _boughtProduct = boughtProduct;
            _buyer = buyer;
            _seller = seller;
        }

        public void initTrade()
        {
            //Check if buy has sufficient balance 
            if (_buyer.wallet < _boughtProduct.price)
            {
                MessageBox.Show("Insufficient Credit");
            }

            //Subtract productprice from buyer balance
            //Add the productprice to the sellerbalance 
            //change product owner from seller to buyer

            else
            {
                _buyer.wallet = _buyer.wallet -  _boughtProduct.price;
                _seller.wallet = _seller.wallet + _boughtProduct.price;
                _boughtProduct.owner = _buyer._id.ToString();

                engineService.UpdateProduct(_boughtProduct._id,_boughtProduct.name,_boughtProduct.price,_boughtProduct.group,_boughtProduct.owner,true);
                engineService.UpdateUser(_buyer._id, _buyer.name, _buyer.email, _buyer.wallet);
                engineService.UpdateUser(_seller._id, _seller.name, _seller.email, _seller.wallet);
            }      
        }

        public void ForSale(ProductModel prodOnSale)
        {
            var results = prodOnSale.isForSale = true;
            engineService.UpdateProduct(prodOnSale._id, prodOnSale.name, prodOnSale.price, prodOnSale.group, prodOnSale.owner, results);
        }

        public void NoSale(ProductModel prodOffSale)
        {
            var results = prodOffSale.isForSale = false;
            engineService.UpdateProduct(prodOffSale._id, prodOffSale.name, prodOffSale.price, prodOffSale.group, prodOffSale.owner, results);
        }

        public UserModel getTheWinner()
        {
            
            var users = engineService.GetUsers();
            double maxWallet = 0;

            foreach (var user in users)
            {
                if (user.wallet > maxWallet)
                {
                    maxWallet = user.wallet;
                    winner = user;
                }
            }
         
            return winner;
        }
    }
}
