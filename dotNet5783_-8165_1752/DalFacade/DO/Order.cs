

using System.Data;

namespace DO2;

public struct Order
{
    public int ID { set; get; }
    public string CustomerName { set; get; }
    public string CustomerEmail { set; get; }
    public string CustomerAdress { set; get; }
    public DateTime OrderDate { set; get; }
    public DateTime ShipData { set; get; }
    public DateTime DeliveryrData { set; get; }
    public override string ToString() => $@"
       OrderID: {ID}
       CustomerName: {CustomerName}
       CustomerEmail: {CustomerEmail}
       CustomerAdress: {CustomerAdress}
       OrderDate: {OrderDate}
       ShipData: {ShipData}
       DeliveryrData: {DeliveryrData}
    ";

}
