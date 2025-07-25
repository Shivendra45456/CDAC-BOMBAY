using ADODEMO_1.Service;
using Microsoft.Extensions.Configuration;
using ADODEMO_1.Models;
namespace ADODEMO_1
{
    internal class Program
    {

        private static IConfiguration _iconfiguration;
        static void Main(string[] args)
        {
            GetAppSettingsFile();
            displaySQLI();
            callservice();
        }
        static void GetAppSettingsFile()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _iconfiguration = builder.Build();
            var mypath = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory());
         //   Console.WriteLine(Directory.GetCurrentDirectory());
          //  Console.WriteLine(new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()));
        }
       

        static void displaySQLI()
        {
            Productlayer obj = new Productlayer(_iconfiguration);
            string s = "LED';delete from Product;Select * from Product where Name like 'd";
            obj.displayproduct(s);
        }

        static void callservice()
        {
            Myservice ms = new Myservice(_iconfiguration);
            List<Product> ls =   ms.GetProducts;
            foreach (Product p in ls)
            {
                Console.WriteLine($"Id = {p.Id} Name = {p.Name} Price = {p.Price} Qty = {p.QTY}");
            }
        }


    }
}
