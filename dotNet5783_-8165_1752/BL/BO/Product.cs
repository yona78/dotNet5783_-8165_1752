using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace BO;
public class Product // logic obejct of product, in order to deal with data base object 
{
    public int ID { get; set; } // id of product
    public string Name { get; set; } // name of product
    public Enums.Category Category { get; set; } // category of product
    public double Price { get; set; } // price of product
    public int InStock { set; get; } // num of items from this product in stock
    public override string ToString() => $@"
       ID:{ID}
       Name: {Name}
       Price: {Price}
       Category: {Category}
       In Stock: {InStock}
    "; // to string.
}

    
