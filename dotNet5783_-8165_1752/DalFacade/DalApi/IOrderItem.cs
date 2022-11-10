using DO;
using System;

namespace DalApi;
public interface IOrderItem : ICrud<OrderItem>
{
    public OrderItem GetOrderItem(int id); // func that return orderItem by its id 
    public List<OrderItem> GetDataOfOrderItem(int idOfOrder); // func that returns all the orderItems from the specific order
}

