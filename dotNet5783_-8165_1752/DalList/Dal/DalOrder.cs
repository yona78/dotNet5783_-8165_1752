using DO;

namespace Dal;

public class DalOrder
{
    public DalOrder() { }
    public int addOrder(Order newOrder)
    {
        newOrder.ID = DataSource.Config.getLastIndexOrder;
        if(DataSource.Config.FirstIndexOrders == DataSource.maxOrders)
            throw new Exception("array is full");
        for (int i = 0; i < DataSource.maxOrders; i++)
        {
            if (DataSource.orders[i].ID == newOrder.ID)
                throw new Exception("order already exist");
        }
        DataSource.orders[DataSource.Config.FirstIndexOrders] = newOrder;
        int newFirstIndexOrders = DataSource.maxOrders;
        int cpyFirstIndexOrders = DataSource.Config.FirstIndexOrders;
        for (int i = DataSource.Config.FirstIndexOrders; i < DataSource.maxOrders; i++)
        {
            if (DataSource.orders[i].ID == 0)
            {
                newFirstIndexOrders = i;
                break;
            }
        }
        DataSource.Config.FirstIndexOrders = newFirstIndexOrders;
        return newFirstIndexOrders;
    }
    public Order getOrder(int id)
    {
        for (int i = 0; i < DataSource.maxOrders; i++)
        {
            if (DataSource.orders[i].ID == id)
                return DataSource.orders[i];
        }
        throw new Exception("order couldn't be found");
    }
    public Order[] getDataOfOrder()
    {
        return DataSource.orders;
    }
    public void deleteOrder(int id)
    {
        bool found = false;
        for (int i = 0; i < DataSource.maxOrders; i++)
        {
            if (DataSource.orders[i].ID == id)
            {
                DataSource.orders[i] = new Order();
                DataSource.Config.FirstIndexOrders = i;
                found = true;
            }
        }
        if(!found)
        {
            throw new Exception("order couldn't be found");
        }
          
    }
    public void updateOrder(Order newOrder)
    {
        bool found= false;
        for (int i = 0; i < DataSource.maxOrders; i++)
        {
            if (DataSource.orders[i].ID == newOrder.ID)
            {
                DataSource.orders[i].CustomerName = newOrder.CustomerName;
                DataSource.orders[i].CustomerEmail = newOrder.CustomerEmail;
                DataSource.orders[i].CustomerAdrress = newOrder.CustomerAdrress;
                DataSource.orders[i].OrderDate = newOrder.OrderDate;
                DataSource.orders[i].ShipDate = newOrder.ShipDate;
                DataSource.orders[i].DeliveryrDate = newOrder.DeliveryrDate;
                found= true;
                break;
            }
        }
        if(!found)
        {
            throw new Exception("order couldn't be found");
        }
    }

}
