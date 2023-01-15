using DalApi;
using DL;
using System.Xml.Linq;
using System.Runtime.CompilerServices;


namespace Dal
{
    internal class OrderItem : IOrderItem
    {
        private const string orderItemFileName = "orderItem.xml";
        private const string ordersFileName = "orders.xml";
        private const string productsFileName = "Product.xml";
        string FPath_n = @"..\xml\config.xml";
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int Add(DO.OrderItem toAdd)
        {
            List<DO.OrderItem?> orders = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(orderItemFileName);
            List<DO.Order?> lst = XMLTools.LoadListFromXMLSerializer<DO.Order?>(ordersFileName);
            List<DO.Product?> lst1 = XMLTools.LoadListFromXMLSerializer<DO.Product?>(productsFileName);
            bool found = false;
            for (int i = 0; i <lst.Count(); i++) // looks if  there is such an order
            {
                if ((lst[i] ?? new DO.Order()).ID == toAdd.OrderID)
                {
                    found = true;
                    break;
                }
            }
            if (!found)
                throw new ExceptionObjectCouldNotBeFound("order");
            double price = 0;
            found = false;
            for (int i = 0; i < lst1.Count(); i++)// looks if  there is such an product
            {
                if ((lst1[i] ?? new DO.Product()).ID == toAdd.ProductID)
                {
                    price = (lst1[i] ?? new DO.Product()).Price;
                    found = true;
                    break;
                }
            }
            if (!found)
                throw new ExceptionObjectCouldNotBeFound("product");
            toAdd.Price = price;
            XElement root = XElement.Load(FPath_n);
            toAdd.OrderItemID = int.Parse(root.Element("lastIndexOrderItems").Value);
            for (int i = 0; i < orders.Count(); i++)
            {
                if ((orders[i] ?? new DO.OrderItem()).OrderID == toAdd.OrderID && (orders[i] ?? new DO.OrderItem()).ProductID == toAdd.ProductID) // because we can't add a new orderItem to the same product and product id, if there is already one there. 
                    throw new ExceptionObjectAlreadyExist("orderItem");
            }
            try
            {
                GetOrderItem(toAdd.OrderID, toAdd.ProductID);

            }
            catch (Exception e)
            {
                orders.Add(toAdd);
            XMLTools.SaveListToXMLSerializer<DO.OrderItem?>(orders, orderItemFileName);
            root.Element("lastIndexOrderItems").Value = (toAdd.OrderItemID + 1).ToString();
            root.Save(FPath_n);
            return toAdd.OrderItemID; // return the id of the orderItem we added
            }
            throw new ExceptionObjectAlreadyExist("orderItem");
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Delete(int id)
        {
            List<DO.OrderItem> orders = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(orderItemFileName);
            int index = orders.FindIndex(o => o.OrderItemID == id);
            if (index != -1)
            {
                orders.RemoveAt(index);
            }
            else
            {
                throw new ExceptionObjectCouldNotBeFound("order");
            }
            XMLTools.SaveListToXMLSerializer<DO.OrderItem>(orders, orderItemFileName);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public DO.OrderItem Get(Func<DO.OrderItem?, bool>? func)
        {
            List<DO.OrderItem?> orders = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(orderItemFileName);
            DO.OrderItem? oi = orders?.FirstOrDefault(ot => func(ot));
            if (oi == null)
                throw new ExceptionObjectCouldNotBeFound("orderItem"); // else, if i couldn't have found this orderItem, i will throw an exception
            return oi ?? new DO.OrderItem();
            //foreach (var item in orders)
            //{
            //    if ((func ?? (x => false))(item))
            //        return (item ?? new DO.OrderItem()); // if item is null, i will return a default value
            //}
            //throw new ExceptionObjectCouldNotBeFound("orderItem"); // else, if i couldn't have found this order, i will throw an exception
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public DO.OrderItem Get(int id)
        {
            return Get(orderItem => (orderItem ?? new DO.OrderItem()).OrderItemID == id);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.OrderItem?> GetDataOf(Func<DO.OrderItem?, bool>? predict = null)
        {
            List<DO.OrderItem?> orders = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(orderItemFileName);
            if (predict == null)
                return (orders);
            IEnumerable<DO.OrderItem?> data = orders.Where(x => predict(x));
            return data;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.OrderItem?> GetDataOfOrderItem(int idOfOrder)
        {
            //List<DO.OrderItem?> orders = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(orderItemFileName);
            //List<DO.OrderItem?> ret = new List<DO.OrderItem?>(); // we use list because we don't know the what is the size of the structre we will need to use.
            //for (int i = 0; i < orders.Count(); i++) // returns a list of all of the orderItems from the specific order, whose id was given to us.
            //{
            //    if ((orders[i] ?? new DO.OrderItem()).OrderID == idOfOrder)
            //    {
            //        ret.Add((orders[i]));
            //    }
            //}
            //return ret;
            return GetDataOf(product => (product ?? new DO.OrderItem()).OrderID == idOfOrder);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public DO.OrderItem GetOrderItem(int idOrder, int idProduct)
        {
            return Get(orderItem => ((orderItem ?? new DO.OrderItem()).OrderID == idOrder) && ((orderItem ?? new DO.OrderItem()).ProductID == idProduct));
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Update(DO.OrderItem toUpdate)
        {
            List<DO.OrderItem?> orders = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(orderItemFileName);
            List<DO.Order?> lst = XMLTools.LoadListFromXMLSerializer<DO.Order?>(ordersFileName);
            List<DO.Product?> lst1 = XMLTools.LoadListFromXMLSerializer<DO.Product?>(productsFileName);
            bool found = false;
            for (int i = 0; i < lst.Count(); i++) // checks if the order exists
            {
                if ((lst[i] ?? new DO.Order()).ID == toUpdate.OrderID)
                {
                    found = true;
                    break;
                }
            }
            if (!found)
                throw new ExceptionObjectCouldNotBeFound("order");

            found = false;
            for (int i = 0; i < lst1.Count(); i++) // checks if the product exists
            {
                if ((lst1[i] ?? new DO.Product()).ID == toUpdate.ProductID)
                {
                    found = true;
                    toUpdate.Price = (lst1[i] ?? new DO.Product()).Price;
                    break;
                }
            }
            if (!found)
                throw new ExceptionObjectCouldNotBeFound("product");
            try
            {
                Get(toUpdate.OrderItemID);
                orders.RemoveAll(o => o?.OrderItemID == toUpdate.OrderItemID);
                orders.Add(toUpdate);
                XMLTools.SaveListToXMLSerializer<DO.OrderItem?>(orders, orderItemFileName);
            }
            catch (Exception e)
            {
                throw new ExceptionObjectCouldNotBeFound("orderItem");
                //found = false;
                //for (int i = 0; i < orders.Count(); i++)
                //{
                //    if ((orders[i] ?? new DO.OrderItem()).OrderItemID == toUpdate.OrderItemID) // if it has the same id, we do a deep copy
                //    {
                //        found = true;
                //        orders.RemoveAt(i);
                //        orders.Insert(i, toUpdate);
                //        break;
                //    }
                //}
                //if (!found) // otherwise, the order itemcouldn't be found.
                //    throw new ExceptionObjectCouldNotBeFound("orderItem");
            }
        }
    }
}
