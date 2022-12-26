using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal
{
    internal class OrderItem : IOrderItem
    {
        XElement OrderItemRoot;
        XElement idNum;
        string FPath_n = "@config.xml";
        string FPath = "@OrderItem.xml";
        public OrderItem()
        {
            if (!File.Exists(FPath))
                CreateFiles();
            else
                LoadData();
            idNum = XElement.Load(FPath_n);
        }
        private void CreateFiles()
        {
            OrderItemRoot = new XElement("orderItems");
            OrderItemRoot.Save(FPath);
        }

        private void LoadData()
        {
            try
            {
                OrderItemRoot = XElement.Load(FPath);
            }
            catch
            {
                Console.WriteLine("File upload problem");
            }
        }
        public int Add(DO.OrderItem toAdd)
        {
            int num_id = int.Parse(idNum.Element("lastIndexOrderItems").Value);
            XElement orderElement;
            try
            {
                orderElement = (from p in OrderItemRoot.Elements()
                                where Convert.ToInt32(p.Element("id").Value) == toAdd.OrderItemID
                                select p).FirstOrDefault();
                throw new ExceptionObjectAlreadyExist("orderItem");
            }
            catch
            {
                XElement id = new XElement("id", num_id);
                num_id += 1;
                idNum.Element("lastIndexOrderItems").Value = num_id.ToString();
                idNum.Save(FPath_n);
                XElement Price = new XElement("Price", toAdd.Price);
                XElement ProductID = new XElement("ProductID", toAdd.ProductID);
                XElement OrderID = new XElement("OrderID", toAdd.OrderID);
                XElement Amount = new XElement("Amount", toAdd.Amount);
                XElement orderItem = new XElement("orderItem", id, Price, ProductID, Amount, OrderID);
                OrderItemRoot.Add(orderItem);
                OrderItemRoot.Save(FPath);
                return num_id-1;
            }
        }

        public void Delete(int id)
        {
            XElement orderItemElement;
            try
            {
                orderItemElement = (from p in OrderItemRoot.Elements()
                                where Convert.ToInt32(p.Element("id").Value) == id
                                select p).FirstOrDefault();
                orderItemElement.Remove();
                OrderItemRoot.Save(FPath);
            }
            catch
            {
                throw new ExceptionObjectCouldNotBeFound("OrderItem");
            }
        }

        public DO.OrderItem Get(Func<DO.OrderItem?, bool>? func)
        {
            IEnumerable<DO.OrderItem?> orders = GetDataOf();
            foreach (var p in orders)
            {
                if (func(p))
                    return (DO.OrderItem)p;
            }
            throw new ExceptionObjectCouldNotBeFound("orderItem");
        }

        public DO.OrderItem Get(int id)
        {
            DO.OrderItem orderItem;
            try
            {
                orderItem = (from p in OrderItemRoot.Elements()
                         where Convert.ToInt32(p.Element("id").Value) == id
                         select new DO.OrderItem()
                         {
                             OrderItemID = Convert.ToInt32(p.Element("id").Value),
                             OrderID = Convert.ToInt32(p.Element("OrderID").Value),
                             ProductID = Convert.ToInt32(p.Element("ProductID").Value),
                             Price = Convert.ToInt32(p.Element("Price").Value),
                             Amount = Convert.ToInt32(p.Element("Amount").Value)
                         }).FirstOrDefault();
            }
            catch
            {
                throw new ExceptionObjectCouldNotBeFound("orderItem");
            }
            return orderItem;
        }

        public IEnumerable<DO.OrderItem?> GetDataOf(Func<DO.OrderItem?, bool>? predict = null)
        {
            IEnumerable<DO.OrderItem?> orderItems;
            try
            {
                orderItems = ((IEnumerable<DO.OrderItem?>)(from p in OrderItemRoot.Elements()
                                                   select new DO.OrderItem()
                                                   {
                                                       OrderItemID = Convert.ToInt32(p.Element("id").Value),
                                                       OrderID = Convert.ToInt32(p.Element("OrderID").Value),
                                                       ProductID = Convert.ToInt32(p.Element("ProductID").Value),
                                                       Price = Convert.ToInt32(p.Element("Price").Value),
                                                       Amount = Convert.ToInt32(p.Element("Amount").Value)

                                                   }));
            }
            catch
            {
                orderItems = null;
            }
            IEnumerable<DO.OrderItem?> data = orderItems.Where(x => predict(x));
            return data;
        }

        public IEnumerable<DO.OrderItem?> GetDataOfOrderItem(int idOfOrder)
        {
            IEnumerable<DO.OrderItem?> orderItems;
            try
            {
                orderItems = ((IEnumerable<DO.OrderItem?>)(from p in OrderItemRoot.Elements()
                                                           where Convert.ToInt32(p.Element("OrderID").Value) == idOfOrder
                                                           select new DO.OrderItem()
                                                           {
                                                               OrderItemID = Convert.ToInt32(p.Element("id").Value),
                                                               OrderID = Convert.ToInt32(p.Element("OrderID").Value),
                                                               ProductID = Convert.ToInt32(p.Element("ProductID").Value),
                                                               Price = Convert.ToInt32(p.Element("Price").Value),
                                                               Amount = Convert.ToInt32(p.Element("Amount").Value)

                                                           }));
            }
            catch
            {
                orderItems = null;
            }
            return orderItems;
        }

        public DO.OrderItem GetOrderItem(int idOrder, int idProduct)
        {
            DO.OrderItem orderItem;
            try
            {
                orderItem = (from p in OrderItemRoot.Elements()
                             where Convert.ToInt32(p.Element("OrderID").Value) == idOrder && Convert.ToInt32(p.Element("ProductID").Value) == idProduct
                             select new DO.OrderItem()
                             {
                                 OrderItemID = Convert.ToInt32(p.Element("id").Value),
                                 OrderID = Convert.ToInt32(p.Element("OrderID").Value),
                                 ProductID = Convert.ToInt32(p.Element("ProductID").Value),
                                 Price = Convert.ToInt32(p.Element("Price").Value),
                                 Amount = Convert.ToInt32(p.Element("Amount").Value)
                             }).FirstOrDefault();
            }
            catch
            {
                throw new ExceptionObjectCouldNotBeFound("orderItem");
            }
            return orderItem;
        }

        public void Update(DO.OrderItem toUpdate)
        {
            try
            {
                XElement orderElement = (from p in OrderItemRoot.Elements()
                                         where Convert.ToInt32(p.Element("id").Value) == toUpdate.OrderItemID
                                         select p).FirstOrDefault();
                orderElement.Element("OrderID").Value = toUpdate.OrderID.ToString();
                orderElement.Element("ProductID").Value = toUpdate.ProductID.ToString();
                orderElement.Element("Amount").Value = toUpdate.Amount.ToString();
                orderElement.Element("Price").Value = toUpdate.Price.ToString();
                OrderItemRoot.Save(FPath);
            }
            catch
            {
                throw new ExceptionObjectCouldNotBeFound("orderItem");
            }
        }
    }
}