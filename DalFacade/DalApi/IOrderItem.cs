using DO;

namespace DalApi;
public interface IOrderItem : ICrud<OrderItem>
{
    public OrderItem GetOrderItem(int idOrder, int idProduct); // func that reutrns orderItem by its order and product ids
    public IEnumerable<OrderItem?> GetDataOfOrderItem(int idOfOrder); // func that returns all the orderItems from the specific order
}

