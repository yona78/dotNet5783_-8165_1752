using BO;
using System.Dynamic;

namespace BlApi;
/// <summary>
/// The interface for all product functions
/// </summary>
public interface IProduct
{
    public void Add(Product product); // func that gets a proudct, and add it into the dBase
    public void Update(Product product); // func that gets a proudct, and update him in the dBase
    public void Delete(int idProduct); // func that gets and id of product, and deletes him from the dBase. The only one who can use this func is the manager
    public IEnumerable<ProductForList> GetList(); // func that returns all the products in a special logic object, which either the manager can use it or it will be printed to the customer screen
    public Product GetForManager(int idProduct); // func that gets an id of product, and returns the product from this specific id. The manager will use this logic object, in oppsite from the last func, when the customer is going to use it, as it will be printed to the screen
    public ProductItem GetForCustomer(int idCustomer, Cart cart); // func that gets an id of product in the client's cart, and his cart, and return the data of the specific product and the cart, as an item in the cart

    public Product Get(Func<Product?, bool>? func); // func that returns proudct according to a term it gets.
    public IEnumerable<BO.Product> GetDataOf(Func<Product?, bool>? predict = null); // func that returns all of the Products  with the specail condition that is indicate in the predict

}
