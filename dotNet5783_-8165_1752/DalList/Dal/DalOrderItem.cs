using DO;
namespace Dal;

public class DalOrderItem
{
    public DalOrderItem() { }

    public int AddOrderItem(OrderItem newOrderItem)
    {
        if (DataSource.Config.firstIndexOrderItems == DataSource.maxOrderItems)
            throw new Exception("array is full");
        bool found = false;
        for (int i = 0; i < DataSource.maxOrders; i++)
        {
            if (DataSource._orders[i].ID == newOrderItem.OrderID)
            {
                found = true;
                break;
            }
        }
        if (!found)
        {
            throw new Exception("order not exist");
        }
        found = false;
        for (int i = 0; i < DataSource.maxProducts; i++)
        {
            if (DataSource._products[i].ID == newOrderItem.ProductID)
            {
                found = true;
                break;
            }
        }
        if (!found)
        {
            throw new Exception("product not exist");
        }
        newOrderItem.OrderItemID = DataSource.Config.GetLastIndexOrderItems;
        for (int i = 0; i < DataSource.maxOrderItems; i++)
        {
            if (DataSource._orderItems[i].OrderID == newOrderItem.OrderID && DataSource._orderItems[i].ProductID == newOrderItem.ProductID)
                throw new Exception("orderItem already exist");
        }
        DataSource._orderItems[DataSource.Config.firstIndexOrderItems] = newOrderItem;
        int newFirstIndexOrderItems = DataSource.maxOrderItems;
        for (int i = DataSource.Config.firstIndexOrderItems; i < DataSource.maxOrderItems; i++)
        {
            if (DataSource._orderItems[i].OrderItemID == 0)
            {
                newFirstIndexOrderItems = i;
                break;
            }
        }
        DataSource.Config.firstIndexOrderItems = newFirstIndexOrderItems;
        return newOrderItem.OrderItemID;
    }
    public OrderItem GetOrderItem(int idOrder, int idProduct)
    {
        for (int i = 0; i < DataSource.maxOrderItems; i++)
        {
            if (DataSource._orderItems[i].OrderID == idOrder && DataSource._orderItems[i].ProductID == idProduct)
                return DataSource._orderItems[i];
        }
        throw new Exception("order couldn't be found");
    }
    public OrderItem[] GetDataOfOrderItem()
    {

        return DataSource._orderItems;
    }
    public void DeleteOrderItem(int idOrderItem)
    {
        bool found = false;
        for (int i = 0; i < DataSource.maxOrderItems; i++)
        {
            if (DataSource._orderItems[i].OrderItemID == idOrderItem)
            {
                found = true;
                DataSource._orderItems[i] = new OrderItem();
                DataSource.Config.firstIndexOrderItems = i;
            }
        }
        if (!found)
        {
            throw new Exception("order couldn't be found");
        }
    }
    public void UpdateOrderItem(OrderItem newOrderItem)
    {
        bool found = false;
        for (int i = 0; i < DataSource.maxOrders; i++)
        {
            if (DataSource._orders[i].ID == newOrderItem.OrderID)
            {
                found = true;
                break;
            }
        }
        if (!found)
        {
            throw new Exception("order not exist");
        }
        found = false;
        for (int i = 0; i < DataSource.maxProducts; i++)
        {
            if (DataSource._products[i].ID == newOrderItem.ProductID)
            {
                found = true;
                break;
            }
        }
        if (!found)
        {
            throw new Exception("product not exist");
        }
        found = false;
        for (int i = 0; i < DataSource.maxOrderItems; i++)
        {
            if (DataSource._orderItems[i].OrderItemID == newOrderItem.OrderItemID)
            {
                found = true;
                DataSource._orderItems[i].OrderID = newOrderItem.OrderID;
                DataSource._orderItems[i].ProductID = newOrderItem.ProductID;
                DataSource._orderItems[i].Amount = newOrderItem.Amount;
                DataSource._orderItems[i].Price = newOrderItem.Price;
                break;
            }
        }
        if (!found)
        {
            throw new Exception("order couldn't be found");
        }
    }

    // The special functions i was asked to add
    public OrderItem GetOrderItem(int id)
    {
        for (int i = 0; i < DataSource.maxOrderItems; i++)
        {
            if (DataSource._orderItems[i].OrderItemID == id)
                return DataSource._orderItems[i];
        }
        throw new Exception("order couldn't be found");
    }
    public List<OrderItem> GetDataOfOrderItem(int idOfOrder)
    {
        List<OrderItem> ret = new List<OrderItem>();
        for (int i = 0; i < DataSource.maxOrderItems; i++)
        {
            if (DataSource._orderItems[i].OrderID == idOfOrder)
            {
                ret.Add(DataSource._orderItems[i]);
            }
        }
        return ret;
    }
}
