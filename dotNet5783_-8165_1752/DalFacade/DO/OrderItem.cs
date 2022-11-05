
/// not a special thing, regular abstract class.

namespace DO;

public struct OrderItem
{
    public int OrderItemID { set; get; }
    public int ProductID { set; get; }
    public int OrderID { set; get; }
    public double Price { set; get; }
    public int Amount { set; get; }
    public override string ToString() => $@"
       OrderItem ID={OrderItemID}
       Product ID={ProductID}
       Order ID: {OrderID}
       Price: {Price}
       Amount : {Amount}
    ";
}
