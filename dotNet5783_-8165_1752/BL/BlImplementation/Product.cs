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
/// <summary>
/// The functions of product object
/// </summary>
internal class Product : BlApi.IProduct // class for product, that the manager can deal with.
{ // otherwise there is ambigiouty, because he doesn't know whether it's BlApi.IProduct or DalApi.IProduct
    private IDal Dal = new DalList(); // a way to communicate with dBase level

    /// <summary>
    /// The function add new product to the store
    /// </summary>
    /// <param name="product">the new product to add</param>
    /// <exception cref="ExceptionDataIsInvalid"></exception>
    /// <exception cref="ExceptionLogicObjectCouldNotBeFound"></exception>
    /// <exception cref="ExceptionLogicObjectAlreadyExist"></exception>
    public void Add(BO.Product product) // func that gets a proudct, and add it into the dBase
    {
        if (product.ID <= 0 || product.Name == null || product.Price <= 0 || product.InStock < 0)
            throw new ExceptionDataIsInvalid("id is negetive");
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
        catch (ExceptionObjectAlreadyExist inner)
        {
            throw new ExceptionLogicObjectAlreadyExist("product", inner);
        }
    }
    /// <summary>
    /// The functions delete product from the store
    /// </summary>
    /// <param name="idProduct">the id of product to delete</param>
    /// <exception cref="ExceptionLogicObjectAlreadyExist"></exception>
    /// <exception cref="ExceptionLogicObjectCouldNotBeFound"></exception>
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
    /// <summary>
    /// The function returns the amount of product in the users cart
    /// </summary>
    /// <param name="idProduct">the produt to check</param>
    /// <param name="cart">the users cart</param>
    /// <returns>the data of the product in cart</returns>
    /// <exception cref="ExceptionDataIsInvalid"></exception>
    /// <exception cref="ExceptionLogicObjectCouldNotBeFound"></exception>
    public ProductItem GetForCustomer(int idProduct, BO.Cart cart) // func that gets an id of product in the client's cart, and his cart, and return the data of the specific product and the cart, as an item in the cart. 
    {
        if (cart.Items == null || cart.Items.Count == 0)
            throw new ExceptionDataIsInvalid("cart empty");
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
        else
            throw new ExceptionDataIsInvalid("id is negetive");
        BO.ProductItem item = new BO.ProductItem();
        item.ID = idProduct;
        item.Price = product.Price;
        item.Name = product.Name;
        item.Category = (BO.Enums.Category)product.Category;
        item.InStock = (product.InStock > 0); // it is aviliable if the items from this specific product in the dBase is bigger than zero.
        int num = 0;
        foreach(BO.OrderItem i in cart.Items)
        {
            if (i.ProductID == idProduct)
                num+=i.Amount;
        }
        if(num==0)
        {
            throw new ExceptionObjectCouldNotBeFound("product in cart");
        }
        item.Amount = num; // the amount of the items from this specific product in the customer's cart
        return item;
    }
    /// <summary>
    /// The funcation returns for the manager the data about product
    /// </summary>
    /// <param name="idProduct">the id of product to get data</param>
    /// <returns>the product the user wants</returns>
    /// <exception cref="ExceptionLogicObjectCouldNotBeFound"></exception>
    /// <exception cref="ExceptionDataIsInvalid"></exception>
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
        else
            throw new ExceptionDataIsInvalid("id is negetive");
        BO.Product item = new BO.Product();
        item.InStock = product.InStock;
        item.ID = product.ID;
        item.Price = product.Price;
        item.Name = product.Name;
        item.Category = (BO.Enums.Category)product.Category;
        return item;
    }
    /// <summary>
    /// The function return all the products in the store
    /// </summary>
    /// <returns>the list with all the products</returns>
    public IEnumerable<ProductForList?> GetList(Func<BO.ProductForList?, bool>? func = null) // func that returns all the products in a special logic object, which either the manager can use it or it will be printed to the customer screen
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
        if (func == null)
            return list;
        IEnumerable<ProductForList?> data = list.Where(x => func(x));
        return data;
    }
    /// <summary>
    /// the function update a product in the store
    /// </summary>
    /// <param name="product">the new product</param>
    /// <exception cref="ExceptionDataIsInvalid"></exception>
    /// <exception cref="ExceptionLogicObjectCouldNotBeFound"></exception>
    public void Update(BO.Product product) // func that gets a proudct, and update him in the dBase
    {

        if (product.ID <= 0 || product.Name == null || product.Price <= 0 || product.InStock < 0)
            throw new ExceptionDataIsInvalid("product");
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

