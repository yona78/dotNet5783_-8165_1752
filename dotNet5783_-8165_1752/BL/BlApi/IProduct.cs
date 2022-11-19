using BO;
using System.Dynamic;

namespace BlApi;
public interface IProduct
{
   public void Add(Product product); // func that gets a proudct, and add it into the dBase
    public void Update(Product product); // func that gets and id of product, and deletes him from the dBase. The only that can use this func is the manager
    public void Delete(int idProduct); // func that gets an id of product in the client's cart, and his cart, and return the data of the specific product and the cart, as an item in the cart
    public List<ProductForList> GetList(); // func that gets an id of product, and returns the product from this specific id. The manager will use this logic object, in oppsite from the last func, when the customer is going to use it, as it will be printed to the screen
    public Product GetForManager(int idProduct); // func that returns all the products in a special logic object, which either the manager can use it or it will be printed to the customer screen
    public ProductItem GetForCustomer(int idCustomer, Cart cart); // func that gets a proudct, and update him in the dBase
}
