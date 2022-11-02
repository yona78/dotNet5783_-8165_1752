using DO;
namespace Dal;

public class DalOrderItem
{
    public DalOrderItem() { }

    public int addOrderItem(OrderItem newOrderItem)
    {
        if (DataSource.Config.FirstIndexOrderItems == DataSource.maxOrderItems)
            throw new Exception("array is full");
        bool found = false;
        for(int i = 0; i < DataSource.maxOrders; i++)
        {
            if (DataSource.orders[i].ID==newOrderItem.OrderID)
            {
                found = true;
                break;
            }
        }
        if(!found)
        {
            throw new Exception("order not exist");
        }
        found = false;
        for(int i = 0; i < DataSource.maxProducts; i++)
        {
            if (DataSource.products[i].ID ==newOrderItem.ProductID)
            {
                found= true;
                break;
            }
        }
        if(!found)
        {
            throw new Exception("product not exist");
        }
        newOrderItem.OrderItemID = DataSource.Config.getLastIndexOrderItems;
        for (int i = 0; i < DataSource.maxOrderItems; i++)
        {
            if (DataSource.orderItems[i].OrderID==newOrderItem.OrderID &&DataSource.orderItems[i].ProductID==newOrderItem.ProductID)
                throw new Exception("orderItem already exist");
        }
        DataSource.orderItems[DataSource.Config.FirstIndexOrderItems] = newOrderItem;
        int newFirstIndexOrderItems = DataSource.maxOrderItems;
        for (int i = DataSource.Config.FirstIndexOrderItems; i < DataSource.maxOrderItems; i++)
        {
            if (DataSource.orderItems[i].OrderItemID==0)
            {
                newFirstIndexOrderItems = i;
                break;
            }
        }
        DataSource.Config.FirstIndexOrderItems = newFirstIndexOrderItems;
        return newOrderItem.OrderItemID;
    }
    public OrderItem getOrderItem(int idOrder, int idProduct)
    {
        for (int i = 0; i < DataSource.maxOrderItems; i++)
        {
            if (DataSource.orderItems[i].OrderID == idOrder &&DataSource.orderItems[i].ProductID==idProduct)
                return DataSource.orderItems[i];
        }
        throw new Exception("order couldn't be found");
    }
    public OrderItem[] getDataOfOrderItem()
    {

        return DataSource.orderItems;
    }
    public void deleteOrderItem(int idOrderItem)
    {
        bool found = false;
        for (int i = 0; i < DataSource.maxOrderItems; i++)
        {
            if (DataSource.orderItems[i].OrderItemID==idOrderItem)
            {
                found = true;
                DataSource.orderItems[i] = new OrderItem();
                DataSource.Config.FirstIndexOrderItems = i;
            }
        }
        if(!found)
        {
            throw new Exception("order couldn't be found");
        }
    }
    public void updateOrderItem(OrderItem newOrderItem)
    {
        bool found = false;
        for(int i = 0; i < DataSource.maxOrders; i++)
        {
            if (DataSource.orders[i].ID==newOrderItem.OrderID)
            {
                found = true;
                break;
            }
        }
        if(!found)
        {
            throw new Exception("order not exist");
        }
        found = false;
        for(int i = 0; i < DataSource.maxProducts; i++)
        {
            if (DataSource.products[i].ID ==newOrderItem.ProductID)
            {
                found= true;
                break;
            }
        }
        if(!found)
        {
            throw new Exception("product not exist");
        }
        found = false;
        for (int i = 0; i < DataSource.maxOrderItems; i++)
        {
            if (DataSource.orderItems[i].OrderItemID==newOrderItem.OrderItemID)
            {
                found=true;
                DataSource.orderItems[i].OrderID = newOrderItem.OrderID;
                DataSource.orderItems[i].ProductID = newOrderItem.ProductID;
                DataSource.orderItems[i].Amount = newOrderItem.Amount;
                DataSource.orderItems[i].Price = newOrderItem.Price;
                break;
            }
        }
        if(!found)
        {
        throw new Exception("order couldn't be found");
        }
    }

    // The special functions i was asked to add
    public OrderItem getOrderItem(int id)
    {
        for (int i = 0; i < DataSource.maxOrderItems; i++)
        {
            if (DataSource.orderItems[i].OrderItemID== id)
                return DataSource.orderItems[i];
        }
        throw new Exception("order couldn't be found");
    }
    public List<OrderItem> getDataOfOrderItem(int idOfOrder)
    {
        List<OrderItem> ret = new List<OrderItem>();
        for(int i = 0; i < DataSource.maxOrderItems; i++)
        {
            if(DataSource.orderItems[i].OrderID== idOfOrder)
            {
                ret.Add(DataSource.orderItems[i]);
            }
        }
        return ret;
    }
}
