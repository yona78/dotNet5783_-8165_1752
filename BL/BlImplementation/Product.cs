
using BO;
using DalApi;
using DO;
using System.Linq;

namespace BlImplementation;
/// <summary>
/// The functions of product object
/// </summary>
internal class Product : BlApi.IProduct // class for product, that the manager can deal with.
{ // otherwise there is ambigiouty, because he doesn't know whether it's BlApi.IProduct or DalApi.IProduct
    private DalApi.IDal dal = DalApi.Factory.Get()!; // a way to communicate with dBase level

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
        prod.Category = (DO.Enums.Category?)product.Category;
        try
        {
            dal.Product.Add(prod);
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
        IEnumerable<DO.Order?> listOfOrders = dal.Order.GetDataOf();
        IEnumerable<DO.OrderItem?> listOfItemOrders;
        foreach (DO.Order? item in listOfOrders) // foreach order in the dBase
        {
            listOfItemOrders = dal.OrderItem.GetDataOfOrderItem((item ?? new DO.Order()).ID);
            foreach (DO.OrderItem? item2 in listOfItemOrders) // foreach orderItem in order we are looking now
                if ((item2 ?? new DO.OrderItem()).ProductID == idProduct) // if the product of this specific orderItem is equal to the idProduct, it means this product already found in one order at least, and we can't delete him.
                    throw new ExceptionLogicObjectAlreadyExist("product");
        }
        try
        {
            dal.Product.Delete(idProduct);
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
        DO.Product product = new DO.Product(); // i want to get the specific product from the dBase
        if (idProduct >= 0)
        {
            try
            {
                product = dal.Product.Get(idProduct);
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
        item.Category = (BO.Enums.Category?)product.Category;
        item.InStock = (product.InStock > 0); // it is aviliable if the items from this specific product in the dBase is bigger than zero.
        int num = 0;
        if (cart.Items == null || cart.Items.Count == 0)
        {
            item.Amount = 0;
            return item;
        }
        List<int> list = cart.Items.Select(x=>x.Amount).ToList();
        num = list.Sum();
        //foreach (BO.OrderItem i in cart.Items)
        //{
        //    if (i.ProductID == idProduct)
        //        num += i.Amount;
        //}
        if (num == 0)
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
                product = dal.Product.Get(idProduct);
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
        item.Category = (BO.Enums.Category?)product.Category;
        return item;
    }
    /// <summary>
    /// The function return all the products in the store
    /// </summary>
    /// <returns>the list with all the products</returns>
    public IEnumerable<ProductForList?> GetList(Func<BO.ProductForList?, bool>? func = null) // func that returns all the products in a special logic object, which either the manager can use it or it will be printed to the customer screen
    {
        IEnumerable<DO.Product?> listOfProducts = dal.Product.GetDataOf();
        IEnumerable<BO.ProductForList> list = from product in listOfProducts select new BO.ProductForList
        {
            ID = (product ?? new DO.Product()).ID,
            Name = (product ?? new DO.Product()).Name,
            Price = (product ?? new DO.Product()).Price,
            Category = (BO.Enums.Category?)(product ?? new DO.Product()).Category
        }
        ;
        //BO.ProductForList product1 = new BO.ProductForList();
        //foreach (DO.Product? product in listOfProducts)  // for each product in the dBase, i would like to initalize a similar product in the ProductForList. The only that can use this func is the manager
        //{
        //    product1.ID = (product ?? new DO.Product()).ID;
        //    product1.Name = (product ?? new DO.Product()).Name;
        //    product1.Price = (product ?? new DO.Product()).Price;
        //    product1.Category = (BO.Enums.Category?)(product ?? new DO.Product()).Category;
        //    list.Add(product1);
        //    product1 = new BO.ProductForList();
        //}
        if (func == null)
            return list;
        IEnumerable<ProductForList?> data = list.Where(x => func(x));
        return data.OrderBy<BO.ProductForList, int>(x=>x.ID); // return the list, orderred by ID
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
        prod.Category = (DO.Enums.Category?)product.Category;
        try
        {
            dal.Product.Update(prod);
        }
        catch (ExceptionObjectCouldNotBeFound inner)
        {
            throw new ExceptionLogicObjectCouldNotBeFound("product", inner);
        }
    }
    /// <summary>
    /// func that returns a product that is being choosing by a specified condition
    /// </summary>
    /// <param name="func"></param>the predict, the specific condition
    /// <returns>the specified product</returns>
    /// <exception cref="ExceptionObjectCouldNotBeFound"></exception>
    public BO.Product Get(Func<BO.Product?, bool>? func) // func that returns proudct by a term it gets.
    {
        IEnumerable<DO.Product?> products = dal.Product.GetDataOf();
        //List<BO.Product?> listOfLogicEntities = new List<BO.Product?>();
        //BO.Product product = new BO.Product();
        IEnumerable<BO.Product?> listOfLogicEntities = from product in products
                                              select new BO.Product
                                              {
                                                  InStock = (product ?? new DO.Product()).InStock,
                                                  ID = (product ?? new DO.Product()).ID,
                                                  Name = (product ?? new DO.Product()).Name,
                                                  Price = (product ?? new DO.Product()).Price,
                                                  Category = (BO.Enums.Category?)(product ?? new DO.Product()).Category
                                              }
        ;
        //foreach (var item in products)
        //{

        //    product.InStock = (item ?? new DO.Product()).InStock;
        //    product.ID = (item ?? new DO.Product()).ID;
        //    product.Price = (item ?? new DO.Product()).Price;
        //    product.Name = (item ?? new DO.Product()).Name;
        //    product.Category = (BO.Enums.Category?)(item ?? new DO.Product()).Category;

        //    listOfLogicEntities.Add(product);
        //} // now, i've created like "DataSouce._products

        BO.Product ret = listOfLogicEntities.FirstOrDefault(p=>func(p));
        if(ret == null)
        {
            throw new ExceptionObjectCouldNotBeFound("product"); // else, if i couldn't have found this product, i will throw an exception
        }
        return (ret ?? new BO.Product());
        //foreach (var item in listOfLogicEntities)
        //{

        //    if ((func ?? (x => false))(item)) // if the func is null, i will return false
        //        return (item ?? new BO.Product()); // if item is null, i will return a default value
        //}
        //throw new ExceptionObjectCouldNotBeFound("product"); // else, if i couldn't have found this product, i will throw an exception
    }
    /// <summary>
    /// func that returns a list of product that are being chosed by a specified condition
    /// </summary>
    /// <param name="predict"></param>the condition we get
    /// <returns>the specified product</returns>
    public IEnumerable<BO.Product?> GetDataOf(Func<BO.Product?, bool>? predict = null) // func that returns all of the products
    {
        IEnumerable<DO.Product?> products = dal.Product.GetDataOf();
        IEnumerable<BO.Product> productsToReturn = from product in products
                                                   select new BO.Product
                                                   {
                                                       InStock = (product ?? new DO.Product()).InStock,
                                                       ID = (product ?? new DO.Product()).ID,
                                                       Name = (product ?? new DO.Product()).Name,
                                                       Price = (product ?? new DO.Product()).Price,
                                                       Category = (BO.Enums.Category?)(product ?? new DO.Product()).Category
                                                   }
        ;

        //BO.Product product = new BO.Product();
        //foreach (var item in products)
        //{

        //    product.InStock = (item ?? new DO.Product()).InStock;
        //    product.ID = (item ?? new DO.Product()).ID;
        //    product.Price = (item ?? new DO.Product()).Price;
        //    product.Name = (item ?? new DO.Product()).Name;
        //    product.Category = (BO.Enums.Category?)(item ?? new DO.Product()).Category;

        //    productsToReturn.Add(product);

        //}

        if (predict == null)
            return productsToReturn;
        IEnumerable<BO.Product?> data = productsToReturn.Where(x => predict(x));
        return data.OrderBy<BO.Product, int>(x => x.ID); // return the list, orderred by ID
    }
}


