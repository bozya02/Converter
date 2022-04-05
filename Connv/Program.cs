using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connv
{
    class Program
    {
        static void Main(string[] args)
        {
            var products = new ObservableCollection<Product>(DBConnection.connection.Products);

            var images = Directory.GetFiles(@"\\nas36d451\user-domain$\stud\201906\Desktop\Product");

            foreach (var image in images)
            {
                string name = image.Split('\\')[8].Split('.')[0];
                Product prod = products.Where(p => p.Name == name).FirstOrDefault();

                try
                {
                    prod.Photo = File.ReadAllBytes(images.FirstOrDefault(p => p.Contains(prod.Name)));
                }
                catch
                {
                    Console.WriteLine($"Error {name}");
                }

                DBConnection.connection.Products.SingleOrDefault(p => p.Name == name);
                Console.WriteLine(DBConnection.connection.SaveChanges());
            }
        }
    }
}
