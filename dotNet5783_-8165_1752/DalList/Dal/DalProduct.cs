using DO;
namespace Dal;

public class DalProduct
{
    public DalProduct() { }
    public int AddProduct(Product newProduct)
    {
        if (DataSource.Config.firstIndexProducts == DataSource.maxProducts)
            throw new Exception("array is full");
        for (int i = 0; i < DataSource.maxProducts; i++)
        {
            if (DataSource._products[i].ID == newProduct.ID)
                throw new Exception("order already exist");
        }
        DataSource._products[DataSource.Config.firstIndexProducts] = newProduct;
        int newFirstIndexProducts = DataSource.maxProducts;
        int cpyFirstIndexProducts = DataSource.Config.firstIndexProducts;
        for (int i = DataSource.Config.firstIndexProducts; i < DataSource.maxProducts; i++)
        {
            if (DataSource._products[i].ID == 0)
            {
                newFirstIndexProducts = i;
                break;
            }
        }
        DataSource.Config.firstIndexProducts = newFirstIndexProducts;
        return cpyFirstIndexProducts;
    }
    public Product GetProduct(int id)
    {
        for (int i = 0; i < DataSource.maxProducts; i++)
        {
            if (DataSource._products[i].ID == id)
                return DataSource._products[i];
        }
        throw new Exception("product couldn't be found");
    }
    public Product[] GetDataOfProduct()
    {
        return DataSource._products;
    }
    public void DeleteProduct(int id)
    {
        bool found = false;
        for (int i = 0; i < DataSource.maxProducts; i++)
        {
            if (DataSource._products[i].ID == id)
            {
                DataSource._products[i] = new Product();
                DataSource.Config.firstIndexProducts = i;
                found = true;
            }
        }
        if (!found)
        {
            throw new Exception("product couldn't be found");
        }
    }
    public void UpdateProduct(Product newProduct)
    {
        bool found = false;
        for (int i = 0; i < DataSource.maxProducts; i++)
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
        if (!found)
        {
            throw new Exception("product couldn't be found");
        }
    }

}
