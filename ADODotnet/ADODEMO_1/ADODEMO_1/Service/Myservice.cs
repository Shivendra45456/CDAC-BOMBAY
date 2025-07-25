using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using ADODEMO_1.Models;
using Microsoft.Extensions.Configuration;

namespace ADODEMO_1.Service
{
    internal class Myservice
    {
        string _connectionstring;
        public Myservice(IConfiguration configuration)
        {
            _connectionstring = configuration.GetConnectionString("Default");
        }

        public List<Product> GetProducts
        {
            get
            {
                List<Product> products = new List<Product>();

                using(SqlConnection con = new SqlConnection(_connectionstring))
                {
                    SqlCommand cmd = new SqlCommand("Select * from Product", con);
                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader(); 

                    while (rdr.Read())
                    {
                        Product product = new Product();
                        product.Id = Convert.ToInt32(rdr["Id"]);
                        product.Name = rdr["Name"].ToString();
                        product.Price = Convert.ToSingle(rdr["Price"]);
                        product.QTY = Convert.ToInt32(rdr["Qty"]);
                        products.Add(product);

                    }

                    return products;
                }
               
            }
            
        }

    }
}
