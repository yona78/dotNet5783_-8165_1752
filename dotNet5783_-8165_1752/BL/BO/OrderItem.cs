using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ProductID { get; set; }

        /// <summary>
        ///  sus, why OrderID doesn't exist ???
        /// </summary>
        public double Price { get; set; }
        public int Amount { get; set; }
        public double TotalPrice { get; set; }
        public override string ToString() => $@"
       OrderItem ID: {ID}
       Product ID: {ProductID}
       Name: {Name}
       Price: {Price}
       Amount: {Amount}
       Total price: {TotalPrice}
    ";
    }
}
