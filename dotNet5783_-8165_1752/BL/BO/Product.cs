using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
public class Product
{
    public override string ToString() => $@"
       Name: {Name}
       category: {Category}
       Price: {Price}
    ";
}