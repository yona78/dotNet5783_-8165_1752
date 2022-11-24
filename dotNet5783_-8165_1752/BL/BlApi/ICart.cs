using BO;
namespace BlApi;
/// <summary>
/// The interface for all cart functions
/// </summary>
public interface ICart
{
    public Cart AddProduct(Cart cart,int idProduct); // add a product to the cart of the customer. The customer will use this func
    public Cart UpdateAmount(Cart cart,int idProduct,int amount); // func that updates the amount of a product in the cart
    public void MakeOrder(Cart cart,string name,string address,string email); // func that take the customer cart and make it into a real order in the dBase
}
