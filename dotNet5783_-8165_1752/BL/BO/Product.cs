using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace BO;
public class Product
{
    public string Name { get; set; }
    public Enums.Category Category { get; set; }
    public double Price { get; set; }
    public override string ToString() => $@"
       Name: {Name}
       category: {Category}
       Price: {Price}
    ";
}