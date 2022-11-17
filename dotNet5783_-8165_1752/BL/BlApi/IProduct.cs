using BO;
using System.Dynamic;

namespace BlApi;
public interface IProduct
{
   public void Add(Product product);
   public void Update(Product product);
    public void Delete(int idProduct);
    public List<ProductForList> GetList();
    public Product GetForManager(int idProduct);
    public ProductItem GetForCustomer(int idCustomer, Cart cart);
}
