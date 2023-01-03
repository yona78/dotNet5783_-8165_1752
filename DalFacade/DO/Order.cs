/// not a special thing, regular abstract class.

namespace DO;
/// <summary>
/// public struct for abstract order class
/// </summary>
public struct Order
{
    public int ID { set; get; }
    public string? CustomerName { set; get; }
    public string? CustomerEmail { set; get; }
    public string? CustomerAddress { set; get; }
    public DateTime? OrderDate { set; get; }
    public DateTime? ShipDate { set; get; }
    public DateTime? DeliveryDate { set; get; }
    public override string ToString() => $@"
       Order ID: {ID}
       CustomerName: {CustomerName}
       CustomerEmail: {CustomerEmail}
       CustomerAdress: {CustomerAddress}
       OrderDate: {OrderDate}
       ShipDate: {ShipDate}
       DeliveryDate: {DeliveryDate}
    ";

}