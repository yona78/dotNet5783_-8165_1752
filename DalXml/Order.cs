/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DalApi;
using DO;

namespace Dal
{
    internal class Order : IOrder
    {
        XElement OrderRoot;
        XElement idNum;
        string FPath_n = "@config.xml";
        string FPath = "@Order.xml";
        public Order()
        {
            if (!File.Exists(FPath))
                CreateFiles();
            else
                LoadData();
            idNum = XElement.Load(FPath_n);
        }
        private void CreateFiles()
        {
            OrderRoot = new XElement("orders");
            OrderRoot.Save(FPath);
        }

        private void LoadData()
        {
            try
            {
                OrderRoot = XElement.Load(FPath);
            }
            catch
            {
                Console.WriteLine("File upload problem");
            }
        }
        public int Add(DO.Order toAdd)
        {
            int num_id = int.Parse(idNum.Element("lastIndexOrder").Value);
            XElement orderElement;
            try
            {
                orderElement = (from p in OrderRoot.Elements()
                                  where Convert.ToInt32(p.Element("id").Value) == toAdd.ID
                                  select p).FirstOrDefault();
                throw new ExceptionObjectAlreadyExist("order");
            }
            catch
            {
                XElement id = new XElement("id", num_id);
                num_id += 1;
                idNum.Element("lastIndexOrder").Value = num_id.ToString();
                idNum.Save(FPath_n);
                XElement CustomerName = new XElement("CustomerName", toAdd.CustomerName);
                XElement CustomerAdrress = new XElement("CustomerAdrress", toAdd.CustomerAdrress);
                XElement CustomerEmail = new XElement("CustomerEmail", toAdd.CustomerEmail);
                XElement ShipDate = new XElement("catgeory", toAdd.ShipDate);
                XElement OrderDate = new XElement("OrderDate", toAdd.OrderDate);
                XElement DeliveryDate = new XElement("DeliveryDate", toAdd.DeliveryDate);
                XElement order = new XElement("order", id, DeliveryDate, OrderDate, ShipDate, CustomerEmail, CustomerAdrress, CustomerName);
                OrderRoot.Add(order);
                OrderRoot.Save(FPath);
                return num_id-1;
            }
        }

        public void Delete(int id)
        {
            XElement orderElement;
            try
            {
                orderElement = (from p in OrderRoot.Elements()
                                  where Convert.ToInt32(p.Element("id").Value) == id
                                  select p).FirstOrDefault();
                orderElement.Remove();
                OrderRoot.Save(FPath);
            }
            catch
            {
                throw new ExceptionObjectCouldNotBeFound("orderItem");
            }
        }

        public DO.Order Get(Func<DO.Order?, bool>? func)
        {
            IEnumerable<DO.Order?> orders = GetDataOf();
            foreach (var p in orders)
            {
                if (func(p))
                    return (DO.Order)p;
            }
            throw new ExceptionObjectCouldNotBeFound("order");
        }

        public DO.Order Get(int id)
        {
            DO.Order order;
            try
            {
                order = (from p in OrderRoot.Elements()
                         where Convert.ToInt32(p.Element("id").Value) == id
                         select new DO.Order()
                         {
                             ID = Convert.ToInt32(p.Element("id").Value),
                             CustomerName = p.Element("CustomerName").Value,
                             CustomerAdrress = p.Element("CustomerAdrress").Value,
                             CustomerEmail = p.Element("CustomerEmail").Value,
                             OrderDate = DateTime.Parse(p.Element("OrderDate").Value),
                             ShipDate = DateTime.Parse(p.Element("ShipDate").Value),
                             DeliveryDate = DateTime.Parse(p.Element("DeliveryDate").Value)
                         }).FirstOrDefault();
            }
            catch
            {
                throw new ExceptionObjectCouldNotBeFound("order");
            }
            return order;
        }

        public IEnumerable<DO.Order?> GetDataOf(Func<DO.Order?, bool>? predict = null)
        {
            IEnumerable<DO.Order?> orders;
            try
            {
                orders = ((IEnumerable<DO.Order?>)(from p in OrderRoot.Elements()
                                                       select new DO.Order()
                                                       {
                                                           ID = Convert.ToInt32(p.Element("id").Value),
                                                           CustomerName = p.Element("CustomerName").Value,
                                                           CustomerAdrress = p.Element("CustomerAdrress").Value,
                                                           CustomerEmail = p.Element("CustomerEmail").Value,
                                                           OrderDate = DateTime.Parse(p.Element("OrderDate").Value),
                                                           ShipDate = DateTime.Parse(p.Element("ShipDate").Value),
                                                           DeliveryDate = DateTime.Parse(p.Element("DeliveryDate").Value)

                                                       }));
            }
            catch
            {
                orders = null;
            }
            IEnumerable<DO.Order?> data = orders.Where(x => predict(x));
            return data;
        }

        public void Update(DO.Order toUpdate)
        {
            try
            {
                XElement orderElement = (from p in OrderRoot.Elements()
                                           where Convert.ToInt32(p.Element("id").Value) == toUpdate.ID
                                           select p).FirstOrDefault();
                orderElement.Element("CustomerName").Value = toUpdate.CustomerName;
                orderElement.Element("CustomerEmail").Value = toUpdate.CustomerEmail;
                orderElement.Element("CustomerAdress").Value = toUpdate.CustomerEmail;
                orderElement.Element("OrderDate").Value = toUpdate.OrderDate.ToString();
                orderElement.Element("ShipDate").Value = toUpdate.ShipDate.ToString();
                orderElement.Element("DeliveryDate").Value = toUpdate.DeliveryDate.ToString();
                OrderRoot.Save(FPath);
            }
            catch
            {
                throw new ExceptionObjectCouldNotBeFound("order");
            }
        }
    }
}
*/