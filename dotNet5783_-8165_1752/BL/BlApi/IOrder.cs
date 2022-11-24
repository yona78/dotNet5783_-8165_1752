using BO;
namespace BlApi;
/// <summary>
/// The interface for all order functions
/// </summary>
public interface IOrder
{
    public List<OrderForList> GetOrderList(); // returns a list of the orders in the dBase to present on the screen to the customer
    public Order GetOrderManager(int idOrder); // func that returns an order from the dBase to the using of the manager
    public Order UpdateSent(int idOrder); // update that this order in know been sending. (Sent)
    public Order UpdateArrived(int idOrder); // update that this order is know been delivering (Arrived)
    public OrderTracking TrackOrder(int idOrder); // func that help me to track an order, by creating and sending a entity that is adjusted for working with orders
    public Order Update(int idOrder, int amount); // update for the manager, to update an order. I guess that you want me to return the order 
}
