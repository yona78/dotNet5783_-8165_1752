using DO;
namespace Dal;

public class DalProduct
{
    public DalProduct() { }
    public int addProduct(Product newProduct)
    {

        return newProduct.ID;
    }
    public Product getProduct(int id)
    {
        Product product = null;

        return product;
    }
    public string getDataOfProduct()
    {

        return "the data of the product";
    }
    public void deleteProduct(int id)
    {

    }
    public void updateProduct(Order newProduct)
    {

    }

}
