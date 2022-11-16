using DO;

namespace BO;
public class OrderForList
{
    public int ID { set; get; }
    public string CustomerName { set; get; }
    public Enums.Status OrderStatus { set; get; }
    public int AmountOfItems { set; get; }
    public double TotelPrice { set; get; }

    public override string ToString() => $@"
       ID:{ID}
       CustomerName: {CustomerName}
       Status: {OrderStatus}
       Amount of Items: {AmountOfItems}
       TotelPrice: {TotelPrice}
    ";

}
