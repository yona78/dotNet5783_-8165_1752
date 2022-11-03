using DO;

namespace Dal;

public class DalOrder
{
    public DalOrder() { }
    public int AddOrder(Order newOrder) 
    {
        int amountOfOrders = DataSource.Config.firstIndexOrders;
        if (amountOfOrders == DataSource.maxOrders)
            throw new Exception("array is full");
        newOrder.ID = DataSource.Config.GetLastIndexOrder;
        for (int i = 0; i < DataSource.maxOrders; i++)
        {
            if (DataSource._orders[i].ID == newOrder.ID)
            {
                throw new Exception("order already exist");
            }
        }
        DataSource._orders[DataSource.Config.firstIndexOrders] = newOrder;
        int newFirstIndexOrders = DataSource.maxOrders;
        for (int i = DataSource.Config.firstIndexOrders; i < DataSource.maxOrders; i++)
        {
            if (DataSource._orders[i].ID == 0)
            {
                newFirstIndexOrders = i;
                break;
            }
        }
        DataSource.Config.firstIndexOrders = newFirstIndexOrders;
        return newOrder.ID;
    }
    public Order GetOrder(int id)
    {
        for (int i = 0; i < DataSource.maxOrders; i++)
        {
            if (DataSource._orders[i].ID == id)
                return DataSource._orders[i];
        }
        throw new Exception("order couldn't be found");
    }
    public Order[] GetDataOfOrder()
    {
        return DataSource._orders;
    }
    public void DeleteOrder(int id)
    {
        bool found = false;
        for (int i = 0; i < DataSource.maxOrders; i++)
        {
            if (DataSource._orders[i].ID == id)
            {
                DataSource._orders[i] = new Order();
                DataSource.Config.firstIndexOrders = i;
                found = true;
            }
        }
        if (!found)
        {
            throw new Exception("order couldn't be found");
        }

    }
    public void UpdateOrder(Order newOrder)
    {
        bool found = false;
        for (int i = 0; i < DataSource.maxOrders; i++)
        {
            if (DataSource._orders[i].ID == newOrder.ID)
            {
                DataSource._orders[i].CustomerName = newOrder.CustomerName;
                DataSource._orders[i].CustomerEmail = newOrder.CustomerEmail;
                DataSource._orders[i].CustomerAdrress = newOrder.CustomerAdrress;
                DataSource._orders[i].OrderDate = newOrder.OrderDate;
                DataSource._orders[i].ShipDate = newOrder.ShipDate;
                DataSource._orders[i].DeliveryrDate = newOrder.DeliveryrDate;
                found = true;
                break;
            }
        }
        if (!found)
        {
            throw new Exception("order couldn't be found");
        }
    }

}
