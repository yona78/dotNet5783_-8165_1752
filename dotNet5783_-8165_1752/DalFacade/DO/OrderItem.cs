

namespace DO2;

public struct OrderItem
{
    public int ProductID { set; get; }
    public int OrderID { set; get; }
    public double Price { set; get; }
    public int Amount { set; get; }
    public override string ToString() => $@"
       Product ID={ProductID}
       OrderID: {OrderID}
    	Price: {Price}
    	Amount : {Amount}
    ";
}
