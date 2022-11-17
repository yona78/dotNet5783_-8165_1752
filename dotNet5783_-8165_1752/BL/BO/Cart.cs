using DO;

namespace BO;
public class Cart // cart of customer
{
    public string CustomerName { set; get; } // customer name
    public string CustomerEmail { set; get; } // customer email
    public string CustomerAddress { set; get; } // customer address
    public List<OrderItem> Items { set; get; } // all the items in the cart
    public double TotelPrice { set; get; } // total price of cart

    public override string ToString() => $@" 
       CustomerName: {CustomerName}
       CustomerEmail: {CustomerEmail}
       CustomerAddress: {CustomerAddress}
       Items: {Items}
       TotelPrice: {TotelPrice}
    "; // to string.

}

