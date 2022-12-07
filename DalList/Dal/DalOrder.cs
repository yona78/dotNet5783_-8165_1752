﻿using DO;
using DalApi;

namespace Dal;
/// <summary>
/// public class for implemention of order 
/// </summary>
internal class DalOrder : IOrder
{
    public DalOrder() { }
    public int Add(Order newOrder) // func that adds an order to the array of orders, and return its id 
    {
        int curEmptyOrder = (DataSource._orders).Count(); // check whether it is possible to add this new order
        if (curEmptyOrder == DataSource.maxOrders)
            throw new ExceptionListIsFull();
        newOrder.ID = DataSource.Config.GetLastIndexOrder;
        for (int i = 0; i < curEmptyOrder; i++) // very simple loop that checks whether the order is already exist.
        {
            if (DataSource._orders!=null&&(DataSource._orders[i]??new Order()).ID == newOrder.ID)
                throw new ExceptionObjectAlreadyExist("order");

        }
        (DataSource._orders??new List<Order?>()).Add(newOrder);
        return newOrder.ID;
    }
    public Order Get(int id) // func that reutrns order by its id
    {
        return Get(order => (order ?? new Order()).ID == id);
    }
    public IEnumerable<Order?> GetDataOf(Func<Order?, bool>? predict = null) // func that returns all of the orders
    {
        if (predict == null)
            return (IEnumerable<Order?>)DataSource._orders;
        IEnumerable<Order?> data = DataSource._orders.Where(x => predict(x));
        return data;
    }
    public void Delete(int id) // func that deletes order from the array
    {
        bool found = false;
        for (int i = 0; i < DataSource._orders.Count(); i++)
        {
            if ((DataSource._orders[i]??new Order()).ID == id) // deleting only if it has the same id.
            {
                DataSource._orders.RemoveAt(i);
                found = true;
            }
        }
        if (!found) // checks if the speicifed order was found
            throw new ExceptionObjectCouldNotBeFound("order");


    }
    public void Update(Order newOrder) // func that updates an order in his array
    {
        bool found = false;
        for (int i = 0; i < DataSource._orders.Count(); i++)
        {
            if ((DataSource._orders[i]??new Order()).ID == newOrder.ID) // if the specified order is found, we do a deep copy.
            {
                DataSource._orders.RemoveAt(i);
                DataSource._orders.Insert(i, newOrder);
                found = true;
                break;
            }
        }
        if (!found)
            throw new ExceptionObjectCouldNotBeFound("order"); // checks if the speicifed order was found 
    }

    public Order Get(Func<Order?, bool>? func) // func that returns an order by a term it gets.
    {
        foreach (var item in DataSource._orders)
        {
            if ((func??(x=>false))(item))
                return (item??new Order()); // if item is null, i will return a default value
        }
        throw new ExceptionObjectCouldNotBeFound("order"); // else, if i couldn't have found this order, i will throw an exception
    }
}