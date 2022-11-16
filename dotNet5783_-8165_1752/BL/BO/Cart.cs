using DO;

namespace BO;
public class Cart
{
    public string CustomerName { set; get; }
    public string CustomerEmail { set; get; }
    public string CustomerAdress { set; get; }
    public List<OrderItem> Items { set; get; }
    public double TotelPrice { set; get; }

    public override string ToString() => $@"
       CustomerName: {CustomerName}
       CustomerEmail: {CustomerEmail}
       CustomerAdress: {CustomerAdress}
       Items: {Items}
       TotelPrice: {TotelPrice}
    ";

}

