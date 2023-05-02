using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
        private string connectionString = "server=127.0.0.1;uid=root;pwd=amP@ssw0rd;database=data";

        [WebMethod]
        public List<string[]> GetProductsData()
        {
            List<string[]> products = new List<string[]>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("SELECT * " +
                        "FROM data.products", connection);
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string id = "id: " + reader["id"].ToString();
                        string name = "name:" + reader["name"].ToString();
                        string details = "details: " + reader["details"].ToString();
                        string rate = "rate: " + reader["rate"].ToString();
                        string price = "price: " + reader["price"].ToString();
                        string type = "type: " + reader["type"].ToString();

                        string[] product = {id, name, details, rate, price, type};

                        products.Add(product);

                    }
                    connection.Close();
                    return products;
                }catch(Exception e)
                {
                    List<string[]> err = new List<string[]>
                    {
                        new string[]{ "error: " + e.ToString() }
                    };
                    return err;
                }
            }
        }

        [WebMethod]
        public string AddProduct(string name, string details, string rate, double price, string type)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("INSERT INTO data.products "
                                               + "VALUES(\"" + new Random().Next() + "\", \"" + name + "\", \"" + details + "\", \""
                                               + rate + "\", \"" + price + "\", \"" + type + "\");"
                                               , connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                    return "the itme is added successfully";

                }
                catch (Exception e)
                {
                    return "error: " + e.ToString();
                }
            }
        }

        [WebMethod]
        public string DeleteProductByID(string id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("DELETE FROM data.products WHERE id = \"" + id + "\";", connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                    return "the item is deleted successfully";

                }
                catch (Exception e)
                {
                    return "error: " + e.ToString();
                }

            }
        }
    }
}
