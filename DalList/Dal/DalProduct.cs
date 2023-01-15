using DalApi;
using DO;
namespace Dal;
using System.Runtime.CompilerServices;

/// <summary>
/// public class for implemention of product 
/// </summary>
internal class DalProduct : IProduct
{
    public DalProduct() { }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(Product newProduct) // func that adds an product to the array of products, and return its id
    {
        if (DataSource._products.Count() == DataSource.maxProducts)
            throw new ExceptionListIsFull();
        //for (int i = 0; i < DataSource._products.Count(); i++) // checks if the product is already exist
        //{
        //    if ((DataSource._products[i] ?? new Product()).ID == newProduct.ID)
        //        throw new ExceptionObjectAlreadyExist("product");
        //}
        try
        {
            Get(newProduct.ID);
        }
        catch (Exception e) 
        { 
            (DataSource._products ?? new List<Product?>()).Add(newProduct);
            return newProduct.ID;
        }
        throw new ExceptionObjectAlreadyExist("Product");
        //return newOrder.ID;
        //DataSource._products.Add(newProduct);

    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Product Get(int id) // func that reutrns product by its id
    {
        return Get(product => (product ?? new Product()).ID == id);
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Product?> GetDataOf(Func<Product?, bool>? predict = null) // func that returns all of the products
    {
        if (predict == null)
            return DataSource._products;
        IEnumerable<Product?> data = DataSource._products.Where(x => predict(x));
        return data;
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id) // func that deletes product from the array
    {
        if (DataSource._products.RemoveAll(o => o?.ID == id) == 0)
            throw new ExceptionObjectCouldNotBeFound("product");
        //bool found = false;
        //for (int i = 0; i < DataSource._products.Count(); i++) // looks for the product with the specific id
        //{
        //    if ((DataSource._products[i] ?? new Product()).ID == id)
        //    {
        //        DataSource._products.RemoveAt(i);
        //        found = true;
        //    }
        //}
        //if (!found) // if the product isn't exist throw an exception
        //    throw new ExceptionObjectCouldNotBeFound("product");
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Product newProduct) // func that updates product in his array
    {
        try
        {
            Get(newProduct.ID);
            DataSource._products.RemoveAll(o => o?.ID == newProduct.ID);
            DataSource._products.Add(newProduct);
        }
        catch (Exception e)
        {
            throw new ExceptionObjectCouldNotBeFound("product");
        }
        //bool found = false;
        //for (int i = 0; i < DataSource._products.Count(); i++) // if the specific product is found, it does a deep copy
        //{
        //    if ((DataSource._products[i] ?? new Product()).ID == newProduct.ID)
        //    {
        //        DataSource._products.RemoveAt(i);
        //        DataSource._products.Insert(i, newProduct);
        //        found = true;
        //        break;
        //    }
        //}
        //if (!found) // if the product isn't exist throw an exception
        //    throw new ExceptionObjectCouldNotBeFound("product");
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public Product Get(Func<Product?, bool>? func) // func that returns an proudct by a term it gets.
    {
        Product? p = DataSource._products?.FirstOrDefault(x => func(x));
        if (p == null)
        //foreach (var item in DataSource._products)
        //{
        //    if ((func ?? (x => false))(item))
        //        return (item ?? new Product()); // if item is null, i will return a default value
        //}
            throw new ExceptionObjectCouldNotBeFound("product"); // else, if i couldn't have found this product, i will throw an exception
        return (p ?? new Product());
    }
}