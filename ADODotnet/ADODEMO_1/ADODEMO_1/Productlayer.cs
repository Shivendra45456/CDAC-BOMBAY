using System;
using System.Collections.Generic;
//using System.Data.SqlClient; //Depricated
using System.Linq;
using System.Data;

using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace ADODEMO_1
{
    internal class Productlayer
    {
        private string _connectionstring;
        public Productlayer(IConfiguration configuration)
        {
            _connectionstring = configuration.GetConnectionString("Default");
        }

        public void Products()
        {
            using (SqlConnection con = new SqlConnection(_connectionstring))
            {
                // Pass the connection to the command object, so the command object knows on which
                // connection to execute the command
                SqlCommand cmd = new SqlCommand("Select * from Product", con);
                // Open the connection. Otherwise you get a runtime error. An open connection is
                // required to execute the command            
                con.Open();
                Console.WriteLine("connected");
                SqlDataReader rdr = cmd.ExecuteReader(); //returns object of sqldatareder
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Console.WriteLine("{0} {1} {2} {3}", rdr["Id"], rdr["Name"], rdr["Price"], rdr["Qty"]);
                    }
                }
            }


        }

        public void displayproduct(string pname)//"T"
        {
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                try
                {  //"v'; Delete from Product;Select * from Product where Name like 'v"

                    //Build the query dynamically, by concatenating the text, that the user has 
                    //typed into the ProductNameTextBox. This is a bad way of constructing
                    //queries. This line of code will open doors for sql injection attack
                    // Select* from Product where Name like 'T%';
                    SqlCommand cmd = new SqlCommand("spGetProductsByName", connection);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductName", pname + "%");
                    connection.Open();
                    SqlDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                        Console.WriteLine("{0} {1} {2} {3}", rd["Id"], rd["Name"], rd["Price"], rd["QTY"]);
                }

                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }



        }
    }
}