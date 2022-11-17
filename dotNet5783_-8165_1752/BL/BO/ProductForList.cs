using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
public class ProductForList // // logic object for dealing with the products. we doens't care about the items, we only want to know about the product
{
    public int ID { get; set; } // id of product
    public string Name { get; set; } // name of product
    public double Price { get; set; } // price of product
    public Enums.Category Category { get; set; } // category of product
    public override string ToString() => $@"
       Product ID: {ID}
       Name: {Name}
       category: {Category}
       Price: {Price}
       Category: {Category}
    ";
}