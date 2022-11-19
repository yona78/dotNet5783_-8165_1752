using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using BlApi;
using BO;
using Dal;
using DalApi;
using DO;

namespace BlImplementation;
internal class Product : BlApi.IProduct // class for product, that the manager can deal with.
{ // otherwise there is ambigiouty, because he doesn't know whether it's BlApi.IProduct or DalApi.IProduct
    private IDal Dal = new DalList(); // a way to communicate with dBase level

    public void Add(BO.Product product) // func that gets a proudct, and add it into the dBase
    {
        if (product.ID <= 0 || product.Name == "" || product.Price <= 0 || product.InStock < 0)
            throw new ExceptionDataInvalid("product");
        DO.Product prod = new DO.Product();
        prod.ID = product.ID;
        prod.Name = product.Name;
        prod.Price = product.Price;
        prod.InStock = product.InStock;
        prod.Category = (DO.Enums.Category)product.Category;
        try
        {
            Dal.Product.Add(prod);
        }
        catch (ExceptionObjectCouldNotBeFound inner)
        {
            throw new ExceptionLogicObjectCouldNotBeFound("product", inner);
        }
    }
    public void Delete(int idProduct) // func that gets and id of product, and deletes him from the dBase. The only that can use this func is the manager
    {
        IEnumerable<DO.Order> listOfOrders = Dal.Order.GetDataOf();
        IEnumerable<DO.OrderItem> listOfItemOrders;
        foreach (DO.Order item in listOfOrders) // foreach order in the dBase
        {
            listOfItemOrders = Dal.OrderItem.GetDataOfOrderItem(item.ID);
            foreach (DO.OrderItem item2 in listOfItemOrders) // foreach orderItem in order we are looking now
                if (item2.ProductID == idProduct) // if the product of this specific orderItem is equal to the idProduct, it means this product already found in one order at least, and we can't delete him.
                    throw new ExceptionLogicObjectAlreadyExist("product");
        }
        try
        {
            Dal.Product.Delete(idProduct);
        }
        catch (ExceptionObjectCouldNotBeFound inner)
        {
            throw new ExceptionLogicObjectCouldNotBeFound("product", inner);
        }
    }
    public ProductItem GetForCustomer(int idProduct, BO.Cart cart) // func that gets an id of product in the client's cart, and his cart, and return the data of the specific product and the cart, as an item in the cart. 
    {
        DO.Product product = new DO.Product(); // i want to get the specific product from the dBase
        if (idProduct >= 0)
        {
            try
            {
                product = Dal.Product.Get(idProduct);
            }
            catch (ExceptionObjectCouldNotBeFound inner)
            {
                throw new ExceptionLogicObjectCouldNotBeFound("product", inner);
            }
        }
        BO.ProductItem item = new BO.ProductItem();
        item.ID = idProduct;
        item.Price = product.Price;
        item.Name = product.Name;
        item.Category = (BO.Enums.Category)product.Category;
        item.InStock = (product.InStock > 0); // it is aviliable if the items from this specific product in the dBase is bigger than zero.
        item.Amount = cart.Items.Count(); // the amount of the items from this specific product in the customer's cart
        return item;
    }
    public BO.Product GetForManager(int idProduct) // func that gets an id of product, and returns the product from this specific id. The manager will use this logic object, in oppsite from the last func, when the customer is going to use it, as it will be printed to the screen
    {
        DO.Product product = new DO.Product();
        if (idProduct >= 0) // checks if the id of product is positive
        {
            try
            {
                product = Dal.Product.Get(idProduct);
            }
            catch (ExceptionObjectCouldNotBeFound inner)
            {
                throw new ExceptionLogicObjectCouldNotBeFound("product", inner);
            }
        }
        BO.Product item = new BO.Product();
        item.Price = product.Price;
        item.Name = product.Name;
        item.Category = (BO.Enums.Category)product.Category;
        return item;
    }
    public List<ProductForList> GetList() // func that returns all the products in a special logic object, which either the manager can use it or it will be printed to the customer screen
    {
        IEnumerable<DO.Product> listOfProducts = Dal.Product.GetDataOf();
        List<BO.ProductForList> list = new List<BO.ProductForList>();
        BO.ProductForList product1 = new BO.ProductForList();
        foreach (DO.Product product in listOfProducts)  // for each product in the dBase, i would like to initalize a similar product in the ProductForList. The only that can use this func is the manager
        {
            product1.ID = product.ID;
            product1.Name = product.Name;
            product1.Price = product.Price;
            product1.Category = (BO.Enums.Category)product.Category;
            list.Add(product1);
            product1 = new BO.ProductForList();
        }
        return list;
    }
    public void Update(BO.Product product) // func that gets a proudct, and update him in the dBase
    {

        if (product.ID <= 0 || product.Name == "" || product.Price <= 0 || product.InStock < 0)
            throw new ExceptionDataInvalid("product");
        DO.Product prod = new DO.Product();
        prod.ID = product.ID;
        prod.Name = product.Name;
        prod.Price = product.Price;
        prod.InStock = product.InStock;
        prod.Category = (DO.Enums.Category)product.Category;
        try
        {
            Dal.Product.Update(prod);
        }
        catch (ExceptionObjectCouldNotBeFound inner)
        {
            throw new ExceptionLogicObjectCouldNotBeFound("product", inner);
        }
    }
}

