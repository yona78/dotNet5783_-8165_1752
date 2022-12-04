using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using Dal;
using DalApi;
using DO;

namespace BlImplementation;
/// <summary>
/// The functions of order
/// </summary>
internal class Order : BlApi.IOrder  // object of the manager, on a order a client had asked for
{
    private IDal Dal = new DalList(); // a way to communicate with dBase level

    /// <summary>
    /// The functions returns data about all the orders
    /// </summary>
    /// <returns>list with data of all the orders</returns>
    public IEnumerable<BO.OrderForList?> GetOrderList() // returns a list of the orders in the dBase to present on the screen to the customer
    {
        List<BO.OrderForList?> listToReturn = new List<BO.OrderForList>();
        IEnumerable<DO.Order> orderList = Dal.Order.GetDataOf();
        BO.OrderForList tmp = new BO.OrderForList();
        double price = 0;
        int amountOfItems = 0;
        foreach (DO.Order order in orderList)  // for each order in the orderlist from the dB, i would like to add a similiar orderList in the orderList list.
        {
            tmp.CustomerName = order.CustomerName;
            tmp.ID = order.ID;
            // now i will calculate the totalPrice of the order, and the amount of items.
            IEnumerable<DO.OrderItem> listOfItems = Dal.OrderItem.GetDataOfOrderItem(order.ID);
            foreach (DO.OrderItem item in listOfItems)
            {
                price += (item.Amount * item.Price); // the price is the total price, the amount*price
                amountOfItems += item.Amount;
            }
            tmp.TotelPrice = price; // the total price of the order
            tmp.AmountOfItems = amountOfItems; // the total amount of items in the order
            // now i will check the status of the order, by comparing the current time, and the time in the data.
            price = 0;
            amountOfItems = 0;
            DateTime now = DateTime.Now;
            if (now > order.DeliveryDate && order.DeliveryDate != null) // it means the order has already arrived. 
                tmp.OrderStatus = BO.Enums.Status.Arrived;
            else if (now > order.ShipDate && order.ShipDate != null) // it means it has been sent, but hasn't arrived yet
                tmp.OrderStatus = BO.Enums.Status.Sent;
            else
                tmp.OrderStatus = BO.Enums.Status.Confirmed; // it must be confirmed, otherwise it wasn't an order in the dBase
            listToReturn.Add(tmp);
            tmp = new BO.OrderForList();
        }
        return listToReturn; // return the list
    }
    /// <summary>
    /// The function returns data about order for menager
    /// </summary>
    /// <param name="idOrder">the id of order to return</param>
    /// <returns>the data of order</returns>
    /// <exception cref="ExceptionDataIsInvalid"></exception>
    /// <exception cref="ExceptionLogicObjectCouldNotBeFound"></exception>
    public BO.Order GetOrderManager(int idOrder) // func that returns an order from the dBase to the using of the manager
    {
        if (idOrder < 0) // throw exception if the id isn't positive
            throw new ExceptionDataIsInvalid("order");
        DO.Order order = new DO.Order();
        try
        {
            order = Dal.Order.Get(idOrder); // now i get the order from the dBase
        }
        catch (ExceptionObjectCouldNotBeFound inner)
        {
            throw new ExceptionLogicObjectCouldNotBeFound("order", inner);
        }
        return Get(order => order.ID == idOrder);
    }
    /// <summary>
    /// The functions returns all the statues of order has been 
    /// </summary>
    /// <param name="idOrder">id of the order the menager wants</param>
    /// <returns>list with all the statues the order was with the date</returns>
    /// <exception cref="ExceptionLogicObjectCouldNotBeFound"></exception>
    public BO.OrderTracking TrackOrder(int idOrder) // func that help me to track an order, by creating and sending a entity that is adjusted for working with orders
    {
        BO.OrderTracking orderTracking = new BO.OrderTracking();
        DO.Order order = new DO.Order();
        try
        {
            order = Dal.Order.Get(idOrder);
        }
        catch (ExceptionObjectCouldNotBeFound inner)
        {
            throw new ExceptionLogicObjectCouldNotBeFound("order", inner);
        }
        orderTracking.ID = order.ID;
        // now i will check the status of the order, by comparing the current time, and the time in the data.
        DateTime now = DateTime.Now;
        List<(DateTime, BO.Enums.Status)?> lst = new List<(DateTime, BO.Enums.Status)?>();
        lst.Add(((DateTime, BO.Enums.Status)?)(order.OrderDate, BO.Enums.Status.Confirmed));
        if (now > order.DeliveryDate && order.DeliveryDate != null) // it means the order has already arrived. 
        {
            orderTracking.OrderStatus = BO.Enums.Status.Arrived;
            lst.Add(((DateTime, BO.Enums.Status)?)(order.ShipDate, BO.Enums.Status.Sent));
            lst.Add(((DateTime, BO.Enums.Status)?)(order.DeliveryDate, BO.Enums.Status.Arrived));
        }
        else if (now > order.ShipDate && order.ShipDate != null)
        {// it means it has been sent, but hasn't arrived yet
            lst.Add(((DateTime, BO.Enums.Status)?)(order.ShipDate, BO.Enums.Status.Sent));
            orderTracking.OrderStatus = BO.Enums.Status.Sent;
        }
        else
            orderTracking.OrderStatus = BO.Enums.Status.Confirmed;
        // it must be confirmed, otherwise it wasn't an order in the dBase
        //list.Add((DateTime.Now, orderTracking.OrderStatus)); // adding the first touple to the list, i guess that's what you meant.
        orderTracking.status = lst;
        return orderTracking;
    }
    /// <summary>
    /// The function update amount of order, bonus func for the manager
    /// </summary>
    /// <param name="idOrder">the id of order to update</param>
    /// <param name="amount">the new amount </param>
    ///  <param name="idOfProduct">the product to update </param>
    /// <exception cref="NotImplementedException"></exception>
    public void Update(int idOrder, int idOfProduct, int amount) // update for the manager, to update an order
    {
        DO.Order order = new DO.Order();
        try
        {
            order = Dal.Order.Get(idOrder);
        }
        catch (ExceptionObjectCouldNotBeFound inner)
        {
            throw new ExceptionLogicObjectCouldNotBeFound("order", inner);
        }
        if (order.ShipDate != null)  // checks if it hasn't been shipped yet
        {
            throw new ExceptionDataIsInvalid("order");
        }
        DO.Product product = new DO.Product();
        try
        {
            product = Dal.Product.Get(idOfProduct);
        }
        catch (ExceptionObjectCouldNotBeFound inner)
        {
            throw new ExceptionLogicObjectCouldNotBeFound("product", inner);
        }
        IEnumerable<DO.OrderItem> list;
        try
        {
            list = Dal.OrderItem.GetDataOfOrderItem(idOrder); // trying to get all of the order items in the order
        }
        catch (ExceptionObjectCouldNotBeFound inner)
        {
            throw new ExceptionLogicObjectCouldNotBeFound("orderItem", inner);
        }
        DO.OrderItem it = new DO.OrderItem();
        foreach (var item in list) // we wants to update an item in the order, which its id is the id of the product we got.
        {
            if ((item).ProductID == idOfProduct)
            {
                it = (item);
                break;
            }
        }
        if (product.InStock + (it).Amount < amount) // checks if we can take more items from this kind from the dBase
        {
            throw new ExceptionNotEnoughInDataBase("order");
        }
        if (it.Amount < amount)
            product.InStock -= (amount - it.Amount);
        else
            product.InStock += it.Amount - amount;
        it.Amount = amount;
        Dal.Product.Update(product);
        Dal.OrderItem.Update(it);

    }
    /// <summary>
    /// The function update the status of order to arrived
    /// </summary>
    /// <param name="idOrder">the id of order to updtae</param>
    /// <returns>the new order after the change</returns>
    /// <exception cref="ExceptionLogicObjectCouldNotBeFound"></exception>
    /// <exception cref="ExceptionObjectIsNotAviliable"></exception>
    public BO.Order UpdateArrived(int idOrder) // update that this order is know been delivering (Arrived)
    {
        DO.Order order = new DO.Order();
        BO.Order orderToReturn = new BO.Order();
        try
        {
            order = Dal.Order.Get(idOrder);
        }
        catch (ExceptionObjectCouldNotBeFound inner)
        {
            throw new ExceptionLogicObjectCouldNotBeFound("order", inner);
        }

        orderToReturn = GetOrderManager(idOrder);
        if (orderToReturn.OrderStatus != BO.Enums.Status.Sent)
            throw new ExceptionObjectIsNotAviliable("order"); // becuase it has been already sent, or even hasn't supplied yet
        orderToReturn.DeliveryDate = DateTime.Now; // updating the logic object
        order.DeliveryDate = DateTime.Now; // updating the object
        try
        {
            Dal.Order.Update(order);
        }
        catch (ExceptionObjectCouldNotBeFound inner)
        {
            throw new ExceptionLogicObjectCouldNotBeFound("order", inner);
        }
        return orderToReturn;
    }
    /// <summary>
    /// The function upsate the status of order to sent
    /// </summary>
    /// <param name="idOrder">the id of order to update</param>
    /// <returns>the new order</returns>
    /// <exception cref="ExceptionLogicObjectCouldNotBeFound"></exception>
    /// <exception cref="ExceptionObjectIsNotAviliable"></exception>
    public BO.Order UpdateSent(int idOrder) // update that this order in know been sending. (Sent)
    {
        DO.Order order = new DO.Order();
        BO.Order orderToReturn = new BO.Order();
        try
        {
            order = Dal.Order.Get(idOrder);
        }
        catch (ExceptionObjectCouldNotBeFound inner)
        {
            throw new ExceptionLogicObjectCouldNotBeFound("order", inner);
        }

        orderToReturn = GetOrderManager(idOrder);
        if (orderToReturn.OrderStatus != Enums.Status.Confirmed)
            throw new ExceptionObjectIsNotAviliable("order"); // becuase it has been already sent, or even arrived
        orderToReturn.ShipDate = DateTime.Now; // updating the logic object
        orderToReturn.OrderStatus = Enums.Status.Sent;
        order.ShipDate = DateTime.Now; // updating the object
        try
        {
            Dal.Order.Update(order);
        }
        catch (ExceptionObjectCouldNotBeFound inner)
        {
            throw new ExceptionLogicObjectCouldNotBeFound("order", inner);
        }
        return orderToReturn;
    }
    /// <summary>
    /// func that returns a product that is being choosing by a specified condition
    /// </summary>
    /// <param name="func"></param>the predict, the specific condition
    /// <returns>the order ti return</returns>
    /// <exception cref="ExceptionObjectCouldNotBeFound"></exception>
    public BO.Order Get(Func<BO.Order?, bool>? func) // func that returns an order by a term it gets.
    {
        IEnumerable<DO.Product> products = Dal.Product.GetDataOf();
        List<BO.Product> listOfLogicEntities = new List<BO.Product>();
        BO.Product product = new BO.Product();
        foreach (var item in products)
        {

            product.InStock = item.InStock;
            product.ID = item.ID;
            product.Price = item.Price;
            product.Name = item.Name;
            product.Category = (BO.Enums.Category)item.Category;

            listOfLogicEntities.Add(product);
        } // now, i've created like "DataSouce._products


        foreach (var item in listOfLogicEntities)
        {
            if (func(item))
                return (item); // if item is null, i will return a default value
        }
        throw new ExceptionObjectCouldNotBeFound("product"); // else, if i couldn't have found this product, i will throw an exception
    }
    /// <summary>
    /// func that returns a list of order that are being chosed by a specified condition
    /// </summary>
    /// <param name="predict"></param>the condition we get
    /// <returns>the order to return</returns>
    public IEnumerable<BO.Order> GetDataOf(Func<BO.Order?, bool>? predict = null) // func that returns all of the orders    
    {
        IEnumerable<DO.Product> products = Dal.Product.GetDataOf();
        List<BO.Product> productsToReturn = new List<BO.Product>();
        BO.Product product = new BO.Product();
        foreach (var item in products)
        {

            product.InStock = item.InStock;
            product.ID = item.ID;
            product.Price = item.Price;
            product.Name = item.Name;
            product.Category = (BO.Enums.Category)item.Category;

            productsToReturn.Add(product);

        }

        if (predict == null)
            return productsToReturn;
        IEnumerable<BO.Product> data = productsToReturn.Where(x => predict(x));
        return data;
    }
}}

