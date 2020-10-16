using ABTradingEngineClientApp.models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Windows.Forms;
using Nancy.Json;

namespace ABTradingEngineClientApp.services
{
    class ABEngineService
    {
        public List<ProductModel> GetProducts()
        {
            string url = String.Format("http://localhost:3000/api/products/");
            WebRequest requestObject = WebRequest.Create(url);

            requestObject.Method = "GET";
            HttpWebResponse responseObjGet = null;

            responseObjGet = (HttpWebResponse)requestObject.GetResponse();

            string strresulttest = null;
            using (Stream stream = responseObjGet.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                strresulttest = sr.ReadToEnd();

                sr.Close();
            }

            List<ProductModel> products = JsonConvert.DeserializeObject<List<ProductModel>>(strresulttest);

            return products;
        }

        public List<EventsModel> GetEvents()
        {
            string url = String.Format("http://localhost:3000/api/events/");
            WebRequest requestObject = WebRequest.Create(url);

            requestObject.Method = "GET";
            HttpWebResponse responseObjGet = null;

            responseObjGet = (HttpWebResponse)requestObject.GetResponse();

            string strresulttest = null;
            using (Stream stream = responseObjGet.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                strresulttest = sr.ReadToEnd();

                sr.Close();
            }

            List<EventsModel> events = JsonConvert.DeserializeObject<List<EventsModel>>(strresulttest);

            return events;
        }


        public UserModel GetUser(string id)
        {
            string url = String.Format("http://localhost:3000/api/users/{0}", id);
            
            WebRequest requestObject = WebRequest.Create(url);

            requestObject.Method = "GET";
            HttpWebResponse responseObjGet = null;

            responseObjGet = (HttpWebResponse)requestObject.GetResponse();

            string strresulttest = null;
            using (Stream stream = responseObjGet.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                strresulttest = sr.ReadToEnd();

                sr.Close();
            }

            UserModel user = JsonConvert.DeserializeObject<UserModel>(strresulttest);

            return user;
        }

        public List<UserModel> GetUsers()
        {
            string url = String.Format("http://localhost:3000/api/users/");

            WebRequest requestObject = WebRequest.Create(url);

            requestObject.Method = "GET";
            HttpWebResponse responseObjGet = null;

            responseObjGet = (HttpWebResponse)requestObject.GetResponse();

            string strresulttest = null;
            using (Stream stream = responseObjGet.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                strresulttest = sr.ReadToEnd();

                sr.Close();
            }

            List<UserModel> users = JsonConvert.DeserializeObject<List<UserModel>>(strresulttest);

            return users;
        }

        public void UpdateUser(object userID, string userName, string userEmail, double userWallet)
        {
            string url = String.Format("http://localhost:3000/api/users/{0}", userID);
            WebRequest requestObject = WebRequest.Create(url);

            requestObject.Method = "PUT";
            requestObject.ContentType = "application/json";

            var obj = new UserModel
            {
                _id = userID,
                name = userName,
                email = userEmail,
                wallet = userWallet,
            };

            var json = new JavaScriptSerializer().Serialize(obj);

            using (var writer = new StreamWriter(requestObject.GetRequestStream()))
            {
                writer.Write(json);
                writer.Flush();
                writer.Close();
            }

            using (var responseApi = (HttpWebResponse)requestObject.GetResponse())
            {
                using (var reader = new StreamReader(responseApi.GetResponseStream()))
                {
                    var objText = reader.ReadToEnd();
                    var objJson = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(objText);
                    MessageBox.Show(objJson.ToString());
                    //requestObject.Content = new StringContent(objText);

                }
            }
        }

        public void UpdateProduct(object prodId, string prodName , double prodPrice , string prodGroup , string prodOwner, bool isForSale)
        {
            string url = String.Format("http://localhost:3000/api/products/{0}", prodId);
            WebRequest requestObject = WebRequest.Create(url);

            requestObject.Method = "PUT";
            requestObject.ContentType = "application/json";

            var obj = new ProductModel
            {
                _id = prodId,
                name = prodName,
                price = prodPrice,
                group = prodGroup, 
                owner = prodOwner,
                isForSale = isForSale
            };

            var json = new JavaScriptSerializer().Serialize(obj);

            using (var writer = new StreamWriter(requestObject.GetRequestStream()))
            {
                writer.Write(json);
                writer.Flush();
                writer.Close();
            }

           using (var responseApi = (HttpWebResponse)requestObject.GetResponse())
            {
                using (var reader = new StreamReader(responseApi.GetResponseStream()))
                {
                    var objText = reader.ReadToEnd();
                    var objJson = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(objText);
                    MessageBox.Show(objJson.ToString());
                    //requestObject.Content = new StringContent(objText);

                }
            }
        }
    }
}
