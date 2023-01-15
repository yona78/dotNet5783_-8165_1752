using BlApi;
using BO;
using DalApi;
using System.Runtime.CompilerServices;



namespace BlImplementation;
/// <summary>
/// Class the imploanet all the functions of cart
/// </summary>
internal class Cart : ICart // cart of customer
{
    private DalApi.IDal dal = DalApi.Factory.Get()!; // a way to communicate with dBase level
    /// <summary>
    /// Function to add product to cart
    /// </summary>
    /// <param name="cart">the cart to add the product</param>
    /// <param name="idProduct"> id of the product to return</param>
    /// <returns>The new cart</returns>
    /// <exception cref="ExceptionLogicObjectCouldNotBeFound"></exception>
    /// <exception cref="ExceptionObjectIsNotAviliable"></exception>
    /// <exception cref="ExceptionNotEnoughInDataBase"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Cart AddProduct(BO.Cart cart, int idProduct) // add a product to the cart of the customer. The customer will use this func
    {
        bool productExistInCart = false;
        BO.OrderItem itemToChange = new BO.OrderItem();
        DO.Product product = new DO.Product();
        try
        {
            product = dal.Product.Get(idProduct);
        }
        catch (ExceptionObjectCouldNotBeFound inner) // the product isn't exist in the dBase
        {
            throw new ExceptionLogicObjectCouldNotBeFound("product", inner);
        }

        if (cart.Items != null)
        {
            itemToChange = cart.Items.FirstOrDefault(p => p.ProductID == idProduct);
            productExistInCart = cart.Items.Any(p => p.ProductID == idProduct);
            //foreach (BO.OrderItem item in cart.Items)
            //{
            //    if (item.ProductID == idProduct)
            //    {
            //        productExistInCart = true;
            //        itemToChange = item;
            //        break;
            //    }
            //}
        }
        if (!productExistInCart) // the product isn't found in the cart
        {
            if (product.InStock <= 0) // there isn't enugh in stock
                throw new ExceptionObjectIsNotAviliable("product");

            BO.OrderItem item = new BO.OrderItem();
            item.Name = product.Name;
            item.Amount = 1;
            item.ProductID = product.ID;
            item.Price = product.Price;
            item.TotalPrice = product.Price;
            cart.TotelPrice += product.Price;

            item.ID = (cart.Items ?? new List<OrderItem>()).Count();
            if (cart.Items == null)
                cart.Items = new List<BO.OrderItem>();
            cart.Items.Add(item);


        }
        else // the product is already found in the cart
        {
            if (product.InStock <= 0) // there isn't enough in stock
                throw new ExceptionObjectIsNotAviliable("product");
            if (itemToChange.Amount == product.InStock)
                throw new ExceptionNotEnoughInDataBase("to many");
            itemToChange.Amount += 1;
            itemToChange.TotalPrice += product.Price;
            cart.TotelPrice += product.Price;
        }

        return cart;
    }

    /// <summary>
    /// The functions makes order from the cart data
    /// </summary>
    /// <param name="cart">the users cart</param>
    /// <param name="name">name of the customer</param>
    /// <param name="address">address of the customer</param>
    /// <param name="email">email of customer</param>
    /// <exception cref="ExceptionDataIsInvalid"></exception>
    /// <exception cref="ExceptionNotEnoughInDataBase"></exception>
    /// <exception cref="ExceptionLogicObjectAlreadyExist"></exception>
    /// <exception cref="ExceptionLogicObjectCouldNotBeFound"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void MakeOrder(BO.Cart cart, string name, string address, string email) // func that take the customer cart and make it into a real order in the dBase
    {
        // checks if all the data in the cart is valid
        if (cart.Items == null || cart.Items.Count == 0)
            throw new ExceptionDataIsInvalid("cart is empty");
        DO.Product product = new DO.Product();
        foreach (var item in cart.Items) // checks if all the items are realy exist, if the amounts are positive
        {
            try
            {
                product = dal.Product.Get(item.ProductID);
            }
            catch (ExceptionObjectCouldNotBeFound) // the product isn't exist in the dBase
            {
                cart.Items.Remove(item);
                break;
            }
            if (item.Amount < 0) // amount negative
                throw new ExceptionDataIsInvalid("orderItem");
            if (product.InStock < item.Amount) // not enough in dBase
                throw new ExceptionNotEnoughInDataBase("orderItem");
        }
        if (name == null || address == null || email == null || name == "" || address == "" || email == "") // checks if the string are valids. ### TO ADD - that email and address will be in a specific format.
            throw new ExceptionDataIsInvalid("cart");
        DO.Order order = new DO.Order();
        order.CustomerAddress = address;
        order.CustomerEmail = email;
        order.CustomerName = name;
        order.OrderDate = DateTime.Now; // initalize the orderDate to be now.
        int id;
        try
        {
            id = dal.Order.Add(order);
        }
        catch (ExceptionObjectAlreadyExist inner)
        {
            throw new ExceptionLogicObjectAlreadyExist("orderItem", inner);
        }
        DO.OrderItem orderItem = new DO.OrderItem();
        foreach (var item in cart.Items)
        {
            orderItem.OrderID = id;
            orderItem.Price = item.Price;
            orderItem.ProductID = item.ProductID;
            orderItem.Amount = item.Amount;
            try
            {
                dal.OrderItem.Add(orderItem);
            }
            catch (ExceptionObjectCouldNotBeFound inner)
            {
                throw new ExceptionLogicObjectCouldNotBeFound("orderItem", inner);
            }
            catch (ExceptionObjectAlreadyExist inner)
            {
                throw new ExceptionLogicObjectAlreadyExist("orderItem", inner);
            }
            try
            {
                product = dal.Product.Get(orderItem.ProductID);
            }
            catch (ExceptionObjectCouldNotBeFound inner)
            {
                throw new ExceptionLogicObjectCouldNotBeFound("product", inner);
            }
            product.InStock -= orderItem.Amount;
            try
            {
                dal.Product.Update(product);
            }
            catch (ExceptionObjectCouldNotBeFound inner)
            {
                throw new ExceptionLogicObjectCouldNotBeFound("product", inner);
            }

        }
    }

    /// <summary>
    /// The function update the amount of product in cart
    /// </summary>
    /// <param name="cart">the users cart</param>
    /// <param name="idProduct">the id of product to upstae his amount</param>
    /// <param name="amount">the new amount</param>
    /// <returns>the update cart</returns>
    /// <exception cref="ExceptionDataIsInvalid"></exception>
    /// <exception cref="ExceptionLogicObjectCouldNotBeFound"></exception>
    /// <exception cref="ExceptionObjectIsNotAviliable"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Cart UpdateAmount(BO.Cart cart, int idProduct, int amount) // func that updates the amount of a product in the cart
    {
        if (cart.Items == null || cart.Items.Count == 0)
            throw new ExceptionDataIsInvalid("cart is empty");
        if (amount < 0)
        {
            throw new ExceptionDataIsInvalid("cart");
        }
        bool productExistInCart = false;
        BO.OrderItem itemToChange = new BO.OrderItem();
        DO.Product product = new DO.Product();
        itemToChange = cart.Items.FirstOrDefault(p => p.ProductID == idProduct);
        productExistInCart = cart.Items.Any(p => p.ProductID == idProduct);
        //foreach (var item in cart.Items)
        //{
        //    if (item.ProductID == idProduct) // checks if the specific product is found in the cart
        //    {
        //        productExistInCart = true;
        //        itemToChange = item;
        //        break;
        //    }
        //}
        if (!productExistInCart) // the product isn't exist in the cart
        {
            throw new ExceptionLogicObjectCouldNotBeFound("product");

        }
        else // the product is exist in the cart
        {
            try
            {
                product = dal.Product.Get(idProduct);
            }
            catch (ExceptionObjectCouldNotBeFound inner)
            {
                throw new ExceptionLogicObjectCouldNotBeFound("product", inner);
            }
            if (product.InStock < amount)
                throw new ExceptionDataIsInvalid("cart");
            if (product.InStock <= 0) // there isn't enough in stock
                throw new ExceptionObjectIsNotAviliable("product");
            if (amount == 0)
            {
                cart.TotelPrice -= (product.Price) * (itemToChange.Amount); // becuase if the last amount was 3, and now the new amount is 0, it means we add remove three items.
                cart.Items.Remove(itemToChange);
            }
            else if (itemToChange.Amount < amount) // we want to enlarge the amount
            {
                cart.TotelPrice += (product.Price) * (amount - itemToChange.Amount); // becuase if the last amount was 3, and now the new amount is 5, it means we add only two items.
                itemToChange.Amount = amount;
                itemToChange.TotalPrice = (product.Price) * amount;
            }
            else if (itemToChange.Amount > amount) // we want to small the amount
            {
                cart.TotelPrice -= (product.Price) * (itemToChange.Amount - amount); // becuase if the last amount was 5, and now the new amount is 3, it means we remove only two items.
                itemToChange.Amount = amount;
                itemToChange.TotalPrice = (product.Price) * amount;
            }
        }

        return cart;
    }
}

