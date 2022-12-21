using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;

namespace Dal
{
    sealed internal class DalXML : IDal
    {
        public IOrder Order { get; } = new Dal.Order();

        public IProduct Product { get; } = new Dal.Product();

        public IOrderItem OrderItem { get; } = new Dal.OrderItem();
    }
}
