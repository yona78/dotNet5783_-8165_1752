using DO;

namespace Dal;
/// <summary>
/// public class for implemention of order 
/// </summary>
public class DalOrder
{
    public DalOrder() { }
    public int AddOrder(Order newOrder) // func that adds an order to the array of orders, and return its id 
    {
        int curEmptyOrder = DataSource._orders.Count(); // check whether it is possible to add this new order
        if (curEmptyOrder == DataSource.maxOrders)
            throw new Exception("array is full");
        newOrder.ID = DataSource.Config.GetLastIndexOrder;
        for (int i = 0; i < curEmptyOrder; i++) // very simple loop that checks whether the order is already exist.
        {
            if (DataSource._orders[i].ID == newOrder.ID)
            {
                throw new Exception("order already exist");
            }
        }
        DataSource._orders.Add(newOrder);
        return newOrder.ID;
    }
    public Order GetOrder(int id) // func that reutrns order by its id
    {
        for (int i = 0; i < DataSource.maxOrders; i++) // looking for the order with the specified id
        {
            if (DataSource._orders[i].ID == id)
                return DataSource._orders[i];
        }
        throw new Exception("order couldn't be found");
    }
    public List<Order> GetDataOfOrder() // func that returns all of the orders
    {
        return DataSource._orders;
    }
    public void DeleteOrder(int id) // func that deletes order from the array
    {
        bool found = false;
        for (int i = 0; i < DataSource.maxOrders; i++)
        {
            if (DataSource._orders[i].ID == id) // deleting only if it has the same id.
            {
                DataSource._orders.RemoveAt(i);
                found = true;
            }
        }
        if (!found) // checks if the speicifed order was found
        {
            throw new Exception("order couldn't be found");
        }

    }
    public void UpdateOrder(Order newOrder) // func that updates an order in his array
    {
        bool found = false;
        for (int i = 0; i < DataSource.maxOrders; i++)
        {
            if (DataSource._orders[i].ID == newOrder.ID) // if the specified order is found, we do a deep copy.
            {
                DataSource._orders.RemoveAt(i);
                DataSource._orders.Add(newOrder);
                found = true;
                break;
            }
        }
        if (!found)
        {
            throw new Exception("order couldn't be found"); // checks if the speicifed order was found
        }
    }

}
