using BO;
namespace BlApi;
public interface ICart
{
    public Cart AddProduct(Cart cart,int idProduct);
    public Cart UpdateAmount(Cart cart,int idProduct,int amount);
    public void MakeOrder(Cart cart,string name,string address,string email);
}
