using DO;
namespace Dal;

public class DalProduct
{
    public DalProduct() { }
    public int addProduct(Product newProduct)
    {
        if (DataSource.Config.FirstIndexProducts == DataSource.maxProducts)
            throw new Exception("array is full");
        for (int i = 0; i < DataSource.maxProducts; i++)
        {
            if (DataSource.products[i].ID == newProduct.ID)
                throw new Exception("order already exist");
        }
        DataSource.products[DataSource.Config.FirstIndexProducts] = newProduct;
        int newFirstIndexProducts = DataSource.maxProducts;
        int cpyFirstIndexProducts = DataSource.Config.FirstIndexProducts;
        for (int i = DataSource.Config.FirstIndexProducts; i < DataSource.maxProducts; i++)
        {
            if (DataSource.products[i].ID == 0)
            {
                newFirstIndexProducts = i;
                break;
            }
        }
        DataSource.Config.FirstIndexProducts = newFirstIndexProducts;
        return cpyFirstIndexProducts;
    }
    public Product getProduct(int id)
    {
        for (int i = 0; i < DataSource.maxProducts; i++)
        {
            if (DataSource.products[i].ID == id)
                return DataSource.products[i];
        }
        throw new Exception("product couldn't be found");
    }
    public string getDataOfProduct()
    {
        return this.ToString();
    }
    public void deleteProduct(int id)
    {
        for (int i = 0; i < DataSource.maxProducts; i++)
        {
            if (DataSource.products[i].ID == id)
            {
                DataSource.products[i] = new Product();
                DataSource.Config.FirstIndexProducts = i;
            }
        }
        throw new Exception("product couldn't be found");
    }
    public void updateProduct(Product newProduct)
    {
        for (int i = 0; i < DataSource.maxProducts; i++)
        {
            if (DataSource.products[i].ID == newProduct.ID)
            {
                DataSource.products[i].Name = newProduct.Name;
                DataSource.products[i].Price = newProduct.Price;
                DataSource.products[i].Category = newProduct.Category;
                DataSource.products[i].InStock = newProduct.InStock;
                break;
            }
        }
        throw new Exception("product couldn't be found");
    }

}
