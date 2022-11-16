using BO;
namespace BlApi;
public interface IOrder
{
    public List<OrderForList> GetOrderList();
    public Order GetOrderManager(int idOrder);
    public Order UpdateDelivery(int idOrder);
    public Order UpdateAmount(int idOrder);
    public OrderTracking TrackOrder(int idOrder);
    public void Update(int idOrder);
}
