using DO;
namespace Dal;

public class DalOrderItem
{
    public DalOrderItem() { }

    public int addOrderItem(OrderItem newOrderItem)
    {
        newOrderItem.OrderItemID = DataSource.Config.getLastIndexOrderItems;
        if (DataSource.Config.FirstIndexOrderItems == DataSource.maxOrderItems)
            throw new Exception("array is full");
        for (int i = 0; i < DataSource.maxOrderItems; i++)
        {
            if (DataSource.orderItems[i].OrderID == newOrderItem.OrderID && DataSource.orderItems[i].OrderItemID == newOrderItem.OrderItemID)
                throw new Exception("order already exist");
        }
        DataSource.orderItems[DataSource.Config.FirstIndexOrderItems] = newOrderItem;
        int newFirstIndexOrderItems = DataSource.maxOrderItems;
        int cpyFirstIndexOrderItems = DataSource.Config.FirstIndexOrderItems;
        for (int i = DataSource.Config.FirstIndexOrderItems; i < DataSource.maxOrderItems; i++)
        {
            if (DataSource.orderItems[i].OrderID == 0 && DataSource.orderItems[i].OrderItemID == 0)
            {
                newFirstIndexOrderItems = i;
                break;
            }
        }
        DataSource.Config.FirstIndexOrders = newFirstIndexOrderItems;
        return cpyFirstIndexOrderItems;
    }
    public OrderItem getOrderItem(int idOrder, int idProduct)
    {
        for (int i = 0; i < DataSource.maxOrderItems; i++)
        {
            if (DataSource.orderItems[i].OrderID == idOrder &&DataSource.orderItems[i].OrderItemID==idProduct)
                return DataSource.orderItems[i];
        }
        throw new Exception("order couldn't be found");
    }
    public OrderItem[] getDataOfOrderItem()
    {

        return DataSource.orderItems;
    }
    public void deleteOrderItem(int idOrder, int idProduct)
    {
        for (int i = 0; i < DataSource.maxOrderItems; i++)
        {
            if (DataSource.orderItems[i].OrderID == idOrder && DataSource.orderItems[i].ProductID==idProduct)
            {
                DataSource.orderItems[i] = new OrderItem();
                DataSource.Config.FirstIndexOrderItems = i;
            }
        }
        throw new Exception("order couldn't be found");
    }
    public void updateOrderItem(OrderItem newOrderItem)
    {
        for (int i = 0; i < DataSource.maxOrders; i++)
        {
            if (DataSource.orderItems[i].OrderID == newOrderItem.OrderID && DataSource.orderItems[i].OrderItemID == newOrderItem.OrderItemID)
            {
                DataSource.orderItems[i].OrderID = newOrderItem.OrderID;
                DataSource.orderItems[i].ProductID = newOrderItem.ProductID;
                DataSource.orderItems[i].Amount = newOrderItem.Amount;
                DataSource.orderItems[i].Price = newOrderItem.Price;
                break;
            }
        }
        throw new Exception("order couldn't be found");
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
