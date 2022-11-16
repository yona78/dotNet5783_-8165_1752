using DO;

namespace BO;
public class OrderItem
{
    public int ID { set; get; }
    public int ProductID { set; get; }
    public string Name { set; get; }
    public double Price { set; get; }
    public int Amount { set; get; }
    public double TotalPrice { set; get; }
    public override string ToString() => $@"
       ID:{ID}
       ProductID: {ProductID}
       Name: {Name}
       Price: {Price}
       Amount: {Amount}
       TotelPrice: {TotalPrice}
    ";

}
