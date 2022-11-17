using DO;

namespace BO;
public class OrderItem // item in the order, indevidiual item in the order
{
    public int ID { set; get; } // id of order item
    public int ProductID { set; get; } // id of prodcut, which product is this item
    public string Name { set; get; } // name of the product, what is the name of the product that the item is it
    public double Price { set; get; } // price of the product
    public int Amount { set; get; } // amount of things in the item. eg, i can order 4 items from the winter dress product
    public double TotalPrice { set; get; } // total price of this item
    public override string ToString() => $@"
       ID:{ID}
       ProductID: {ProductID}
       Name: {Name}
       Price: {Price}
       Amount: {Amount}
       TotelPrice: {TotalPrice}
    "; // to string.

}
