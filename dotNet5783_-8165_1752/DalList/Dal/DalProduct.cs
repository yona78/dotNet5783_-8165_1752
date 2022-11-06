using DO;
namespace Dal;
/// <summary>
/// public class for implemention of product 
/// </summary>
public class DalProduct
{
    public DalProduct() { }
    public int AddProduct(Product newProduct) // func that adds an product to the array of products, and return its id
    {
        if (DataSource.Config.firstIndexProducts == DataSource.maxProducts)
            throw new Exception("array is full");
        for (int i = 0; i < DataSource.maxProducts; i++) // checks if the product is already exist
        {
            if (DataSource._products[i].ID == newProduct.ID)
                throw new Exception("product already exist");
        }
        DataSource._products[DataSource.Config.firstIndexProducts] = newProduct;
        int newFirstIndexProducts = DataSource.maxProducts;
        for (int i = DataSource.Config.firstIndexProducts; i < DataSource.maxProducts; i++) // updates where is the new empty place in the array
        {
            if (DataSource._products[i].ID == 0) // it means this place is empty
            {
                newFirstIndexProducts = i;
                break;
            }
        }
        DataSource.Config.firstIndexProducts = newFirstIndexProducts;
        return newProduct.ID;
    }
    public Product GetProduct(int id) // func that reutrns product by its id
    {
        for (int i = 0; i < DataSource.maxProducts; i++) // the loop checks whether this prodcut is exist or not
        {
            if (DataSource._products[i].ID == id)
                return DataSource._products[i];
        }
        throw new Exception("product couldn't be found");
    }
    public Product[] GetDataOfProduct() // func that returns all of the products
    {
        return DataSource._products;
    }
    public void DeleteProduct(int id) // func that deletes product from the array
    {
        bool found = false;
        for (int i = 0; i < DataSource.maxProducts; i++) // looks for the product with the specific id
        {
            if (DataSource._products[i].ID == id)
            {
                DataSource._products[i] = new Product();
                DataSource.Config.firstIndexProducts = i;
                found = true;
            }
        }
        if (!found) // if the product isn't exist throw an exception
        {
            throw new Exception("product couldn't be found");
        }
    }
    public void UpdateProduct(Product newProduct) // func that updates product in his array
    {
        bool found = false;
        for (int i = 0; i < DataSource.maxProducts; i++) // if the specific product is found, it does a deep copy
        {
            if (DataSource._products[i].ID == newProduct.ID)
            {
                DataSource._products[i].Name = newProduct.Name;
                DataSource._products[i].Price = newProduct.Price;
                DataSource._products[i].Category = newProduct.Category;
                DataSource._products[i].InStock = newProduct.InStock;
                found = true;
                break;
            }
        }
        if (!found) // if the product isn't exist throw an exception
        {
            throw new Exception("product couldn't be found");
        }
    }

}
