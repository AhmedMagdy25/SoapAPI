using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Services;

namespace SoapAPI
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public List<string> getProductByName(string name)
        {

            List<string> product = new List<string>();
            string connectionString = "server=127.0.0.1;uid=root;pwd=amP@ssw0rd;database=data";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT id, details, rate, price " +
                    "FROM data.products WHERE name = \""+name+"\"" , connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string id = "id:" + reader["id"].ToString();
                    string details = "details: " + reader["details"].ToString();
                    string rate = "rate: " + reader["rate"].ToString();
                    string price = "price: " + reader["price"].ToString();

                    product.Add(id);
                    product.Add(details);
                    product.Add(rate);
                    product.Add(price);

                }
                connection.Close();
                return product;
            }
        }

        [WebMethod]
        public List<string> GetProductsData()
        {
            List<string> products = new List<string>();
            string connectionString = "server=127.0.0.1;uid=root;pwd=amP@ssw0rd;database=data";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT id, name, details, rate, price " +
                    "FROM data.products", connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string id =  "id: " + reader["id"].ToString();
                    string name = "name:" + reader["name"].ToString();
                    string details = "details: " + reader["details"].ToString();
                    string rate = "rate: " + reader["rate"].ToString();
                    string price = "price: " + reader["price"].ToString();

                    products.Add(id);
                    products.Add(name);
                    products.Add(details);
                    products.Add(rate);
                    products.Add(price);

                }
                connection.Close();
                return products;
            }
        }
    }
}
