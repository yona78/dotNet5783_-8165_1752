using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace BO
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Enums.Category Category { get; set; }
        public int InStock { get; set; }
        public override string ToString() => $@"
       Product ID: {ID}
       Name: {Name}
       category: {Category}
       Price: {Price}
       Amount in stock: {InStock}
    ";
    }
}
