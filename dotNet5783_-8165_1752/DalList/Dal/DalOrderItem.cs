using DO;
namespace Dal;

public class DalOrderItem
{
    public DalOrderItem() { }

    public int addOrderItem(OrderItem newOrderItem)
    {

        return newOrderItem.ProductID; // I'm not sure what id should i return
    }
    public OrderItem getOrderItem(int id)
    {
        OrderItem orderItem = null;

        return orderItem;
    }
    public string getDataOfOrderItem()
    {

        return "the data of the orderItem";
    }
    public void deleteOrderItem(int id)
    {

    }
    public void updateOrderItem(OrderItem newOrderItem)
    {

    }

    // The special functions i was asked to add
    public OrderItem getOrderItem(Order order, Product product)
    {
        return new OrderItem();
    }
    public string getDataOfOrderItem(int idOfOrder)
    {
        return "the data of the orderItem in specific order";
    }
}
