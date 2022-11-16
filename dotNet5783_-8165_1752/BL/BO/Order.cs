using DO;

namespace BO;
public class Order
{
    public int ID { set; get; }
    public string CustomerName { set; get; }
    public string CustomerEmail { set; get; }
    public string CustomerAdress { set; get; }
    public Enums.Status OrderStatus { set; get; }
    public DateTime PaymentDate { set; get; }
    public DateTime ShipDate { set; get; }
    public DateTime DeliveryrDate { set; get; }
    public List<OrderItem> Items { set; get; }
    public double TotelPrice { set; get; }

    public override string ToString() => $@"
       ID:{ID}
       CustomerName: {CustomerName}
       CustomerEmail: {CustomerEmail}
       CustomerAdress: {CustomerAdress}
       Status: {OrderStatus}
       PaymentDate: {PaymentDate}
       ShipDate {ShipDate}
       DeliveryrDate: {DeliveryrDate}
       Items: {Items}
       TotelPrice: {TotelPrice}
    ";

}
