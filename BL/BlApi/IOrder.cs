using BO;
namespace BlApi;
/// <summary>
/// The interface for all order functions
/// </summary>
public interface IOrder
{
    public IEnumerable<OrderForList?> GetOrderList(); // returns a list of the orders in the dBase to present on the screen to the customer
    public Order GetOrderManager(int idOrder); // func that returns an order from the dBase to the using of the manager
    public Order UpdateSent(int idOrder); // update that this order in know been sending. (Sent)
    public Order UpdateArrived(int idOrder); // update that this order is know been delivering (Arrived)
    public OrderTracking TrackOrder(int idOrder); // func that help me to track an order, by creating and sending a entity that is adjusted for working with orders
    public void Update(int idOrder, int idOfProduct, int amount); // update for the manager, to update an order.

    public Order Get(Func<Order?, bool>? func); // func that returns order according to a term it gets.
    public IEnumerable<Order?> GetDataOf(Func<Order?, bool>? predict = null); // func that returns all of the Orders  with the specail condition that is indicate in the predict
    public void UpdateNameEmailAddress(string customerAddress1, string customerEmail, string customerAddress2, int ID); // Bonus func that we addes, for updateOrderCutomer (and also manager)

    public int? idOrderUpdateNow();
    public Order UpdateStatus(int idOrder); 
}
