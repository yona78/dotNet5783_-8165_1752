using DO;
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
        int curEmptyOrder = DataSource._orders.Count(); // check whether it is possible to add this new order
        if (curEmptyOrder == DataSource.maxOrders)
            throw new ExceptionListIsFull();
        newOrder.ID = DataSource.Config.GetLastIndexOrder;
        for (int i = 0; i < curEmptyOrder; i++) // very simple loop that checks whether the order is already exist.
        {
            if (DataSource._orders[i].ID == newOrder.ID)
                throw new ExceptionObjectAlreadyExist("order");

        }
        DataSource._orders.Add(newOrder);
        return newOrder.ID;
    }
    public Order Get(int id) // func that reutrns order by its id
    {
        for (int i = 0; i < DataSource._orders.Count(); i++) // looking for the order with the specified id
        {
            if (DataSource._orders[i].ID == id)
                return DataSource._orders[i];
        }
        throw new ExceptionObjectCouldNotBeFound("order");
    }
    public IEnumerable<Order> GetDataOf() // func that returns all of the orders
    {
        return DataSource._orders;
    }
    public void Delete(int id) // func that deletes order from the array
    {
        bool found = false;
        for (int i = 0; i < DataSource._orders.Count(); i++)
        {
            if (DataSource._orders[i].ID == id) // deleting only if it has the same id.
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
            if (DataSource._orders[i].ID == newOrder.ID) // if the specified order is found, we do a deep copy.
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

}