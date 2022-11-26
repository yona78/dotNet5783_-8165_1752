using BO;
using BlApi;
using BlImplementation;
using System.Collections.Generic;
using DalApi;

namespace Program;
/// <summary>
/// The main progrem to test our code
/// </summary>
class Program
{
    static IBl blList = new Bl();
    static Cart cart = new Cart();
    /// <summary>
    /// The main menu for all the objects
    /// </summary>
    static void Main()
    {
        bool validInput = true;
        int choice = 0;
        do
        {
            do // you will see this do_ while loop every time we will use TryParse...
            {

                Console.WriteLine(@"Welcoome to Yona's and Avishai's shop, you might choose to do some things on our shop.
                    0 ==> for exit
                    1 ==> for Order
                    2 ==> for Product
                    3 ==> Cart");
                validInput = int.TryParse(Console.ReadLine(), out choice);
                if (!validInput)
                    Console.WriteLine("please enter a valid input");
            } while (!validInput);
            switch (choice)
            {
                case 0:
                    break;
                case 1:
                    OrderOption();
                    break;
                case 2:
                    ProductOption();
                    break;
                case 3:
                    CartOption();
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        } while (choice != 0);

    }
    /// <summary>
    /// The function to test all the order functions
    /// </summary>
    static void OrderOption() // order option
    {
        char choiceInSubSwitch = 'x';
        bool validInput = true;
        bool didSomethingInThisSession = false;

        do
        {
            didSomethingInThisSession = true;

            do
            {

                Console.WriteLine(@"you chose: Order
                   a ==> for getting all the orders
                   b ==> for getting an access to the order: Manager
                   c ==> for updating that an order has been sent
                   d ==> for updating that an order has been arrived
                   e ==> for getting an Entity that helps me to control the orders
                   f ==> for updating an order: Manager");
                validInput = char.TryParse(Console.ReadLine(), out choiceInSubSwitch);
                if (!validInput)
                    Console.WriteLine("please enter a valid input");
            } while (!validInput);
            int id;
            int amount;

            switch (choiceInSubSwitch)
            {
                case 'a': // getOrderList option
                    IEnumerable<BO.OrderForList> list = blList.Order.GetOrderList();
                    foreach (BO.OrderForList item in list)
                        Console.WriteLine(item);
                    break;
                case 'b': // getting access to an order: Manager 
                    do
                    {
                        Console.Write("please enter me an id: ");
                        validInput = int.TryParse(Console.ReadLine(), out id);
                        if (!validInput)
                            Console.WriteLine("please enter a valid input");
                    } while (!validInput);
                    try
                    {
                        Console.WriteLine(blList.Order.GetOrderManager(id));
                    }
                    catch (ExceptionDataIsInvalid error)
                    {
                        Console.WriteLine("Name Of Exception: {0}\nMassage In Exception: {1}", error.GetType().Name, error.Message);
                    }
                    catch (ExceptionLogicObjectCouldNotBeFound error)
                    {
                        Console.WriteLine("Name Of Exception: {0}\nMassage In Exception: {1}\nName Of Inner Exception: {2}\nMassage In Inner Exception: {3}", error.GetType().Name, error.Message, error.InnerException.GetType().Name, error.InnerException.Message);
                    }
                    break;
                case 'c': // updating that an order has been sent
                    do
                    {
                        Console.Write("please enter me an id: ");
                        validInput = int.TryParse(Console.ReadLine(), out id);
                        if (!validInput)
                            Console.WriteLine("please enter a valid input");
                    } while (!validInput);
                    try
                    {
                        Console.WriteLine(blList.Order.UpdateSent(id));
                    }
                    catch (ExceptionObjectIsNotAviliable error)
                    {
                        Console.WriteLine("Name Of Exception: {0}\nMassage In Exception: {1}", error.GetType().Name, error.Message);
                    }
                    catch (ExceptionLogicObjectCouldNotBeFound error)
                    {
                        Console.WriteLine("Name Of Exception: {0}\nMassage In Exception: {1}\nName Of Inner Exception: {2}\nMassage In Inner Exception: {3}", error.GetType().Name, error.Message, error.InnerException.GetType().Name, error.InnerException.Message);
                    }
                    break;
                case 'd': // updating that an order has been arrived
                    do
                    {
                        Console.Write("please enter me an id: ");
                        validInput = int.TryParse(Console.ReadLine(), out id);
                        if (!validInput)
                            Console.WriteLine("please enter a valid input");
                    } while (!validInput);
                    try
                    {
                        Console.WriteLine(blList.Order.UpdateArrived(id));
                    }
                    catch (ExceptionObjectIsNotAviliable error)
                    {
                        Console.WriteLine("Name Of Exception: {0}\nMassage In Exception: {1}", error.GetType().Name, error.Message);
                    }
                    catch (ExceptionLogicObjectCouldNotBeFound error)
                    {
                        Console.WriteLine("Name Of Exception: {0}\nMassage In Exception: {1}\nName Of Inner Exception: {2}\nMassage In Inner Exception: {3}", error.GetType().Name, error.Message, error.InnerException.GetType().Name, error.InnerException.Message);
                    }
                    break;
                case 'e': // for getting an Entity that helps me to control the orders
                    do
                    {
                        Console.Write("please enter me an id: ");
                        validInput = int.TryParse(Console.ReadLine(), out id);
                        if (!validInput)
                            Console.WriteLine("please enter a valid input");
                    } while (!validInput);
                    try
                    {
                        Console.WriteLine(blList.Order.TrackOrder(id));
                    }
                    catch (ExceptionLogicObjectCouldNotBeFound error)
                    {
                        Console.WriteLine("Name Of Exception: {0}\nMassage In Exception: {1}\nName Of Inner Exception: {2}\nMassage In Inner Exception: {3}", error.GetType().Name, error.Message, error.InnerException.GetType().Name, error.InnerException.Message);
                    }
                    break;
                case 'f': // for updating an order
                    do
                    {
                        Console.Write("please enter me an id: ");
                        validInput = int.TryParse(Console.ReadLine(), out id);
                        if (!validInput)
                            Console.WriteLine("please enter a valid input");
                    } while (!validInput);
                    int idOfProduct;
                    do // getting the new amount
                    {
                        Console.Write("please enter me an amount: ");
                        validInput = int.TryParse(Console.ReadLine(), out amount);
                        if (!validInput)
                            Console.WriteLine("please enter a valid input");
                    } while (!validInput);
                    do // getting the new amount
                    {
                        Console.Write("please enter me an id of protuct: ");
                        validInput = int.TryParse(Console.ReadLine(), out idOfProduct);
                        if (!validInput)
                            Console.WriteLine("please enter a valid input");
                    } while (!validInput);
                    try
                    {
                        blList.Order.Update(id,idOfProduct, amount);
                    }
                    catch (ExceptionLogicObjectCouldNotBeFound error)
                    {
                       Console.WriteLine("Name Of Exception: {0}\nMassage In Exception: {1}\nName Of Inner Exception: {2}\nMassage In Inner Exception: {3}", error.GetType().Name, error.Message, error.InnerException.GetType().Name, error.InnerException.Message);
                    }
                    catch (ExceptionNotEnoughInDataBase error)
                    {
                        Console.WriteLine("Name Of Exception: {0}\nMassage In Exception: {1}\n", error.GetType().Name, error.Message);
                    }
                    catch (ExceptionDataIsInvalid error)
                    {
                        Console.WriteLine("Name Of Exception: {0}\nMassage In Exception: {1}\n", error.GetType().Name, error.Message);
                    }
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    didSomethingInThisSession = false;
                    break;
            }
        } while (!didSomethingInThisSession);
    }
    /// <summary>
    /// The function to test all the product functions
    /// </summary>
    static void ProductOption() // product option
    {
        char choiceInSubSwitch = 'x';
        bool validInput = true;
        bool didSomethingInThisSession;

        do
        {
            didSomethingInThisSession = true;
            do
            {

                Console.WriteLine(@"you chose: Product
                   a ==> for adding a new product
                   b ==> for getting a product by its id
                   c ==> for getting all of the products
                   d ==> for updating a product
                   e ==> for deleting a product
                   f ==> for getting data of a product in a cart");
                validInput = char.TryParse(Console.ReadLine(), out choiceInSubSwitch);
                if (!validInput)
                    Console.WriteLine("please enter a valid input");
            } while (!validInput);
            int id;
            double price;
            int amount;
            switch (choiceInSubSwitch)
            {
                case 'a': // add product option
                    Enums.Category category;
                    Product productToAdd = new Product();
                    do
                    {
                        Console.Write("Enter id of the product: ");
                        validInput = int.TryParse(Console.ReadLine(), out id);
                        if (!validInput)
                            Console.WriteLine("please enter a valid input");
                    } while (!validInput);
                    productToAdd.ID = id;
                    Console.Write("Enter name of the product: ");
                    productToAdd.Name = Console.ReadLine();
                    do
                    {
                        Console.Write("Enter price of the product: ");
                        validInput = double.TryParse(Console.ReadLine(), out price);
                        if (!validInput)
                            Console.WriteLine("please enter a valid input");
                    } while (!validInput);
                    productToAdd.Price = price;
                    do
                    {
                        Console.Write("Enter amount of the product: ");
                        validInput = int.TryParse(Console.ReadLine(), out amount);
                        if (!validInput)
                            Console.WriteLine("please enter a valid input");
                    } while (!validInput);
                    productToAdd.InStock = amount;
                    do
                    { /// in the future, there would be a loop here that prints all the posible categories, because we might want to add more categories.
                        Console.Write(@"Optinal Categories: 
                            Dresses,
                            Shirts,
                            Hats,
                            Shoes, 
                            Socks, 
                            Skirts,
                            Coats
                            Enter category of the product: ");
                        validInput = Enums.Category.TryParse(Console.ReadLine(), out category);
                        if (!validInput)
                            Console.WriteLine("please enter a valid input");
                    } while (!validInput);
                    productToAdd.Category = category;
                    try
                    {
                        blList.Product.Add(productToAdd);
                    }
                    catch (ExceptionDataIsInvalid error)
                    {
                        Console.WriteLine("Name Of Exception: {0}\nMassage In Exception: {1}", error.GetType().Name, error.Message);
                    }
                    catch (ExceptionLogicObjectCouldNotBeFound error)
                    {
                        Console.WriteLine("Name Of Exception: {0}\nMassage In Exception: {1}\nName Of Inner Exception: {2}\nMassage In Inner Exception: {3}", error.GetType().Name, error.Message, error.InnerException.GetType().Name, error.InnerException.Message);
                    }
                    catch (ExceptionLogicObjectAlreadyExist error)
                    {
                        Console.WriteLine("Name Of Exception: {0}\nMassage In Exception: {1}\nName Of Inner Exception: {2}\nMassage In Inner Exception: {3}", error.GetType().Name, error.Message, error.InnerException.GetType().Name, error.InnerException.Message);
                    }
                    break;
                case 'b': // getting a product by its id
                    do
                    {
                        Console.Write("Enter id of the product: ");
                        validInput = int.TryParse(Console.ReadLine(), out id);
                        if (!validInput)
                            Console.WriteLine("please enter a valid input");
                    } while (!validInput);
                    try
                    {
                        Console.WriteLine(blList.Product.GetForManager(id));
                    }
                    catch (ExceptionLogicObjectCouldNotBeFound error)
                    {
                        Console.WriteLine("Name Of Exception: {0}\nMassage In Exception: {1}\nName Of Inner Exception: {2}\nMassage In Inner Exception: {3}", error.GetType().Name, error.Message, error.InnerException.GetType().Name, error.InnerException.Message);
                    }
                    catch (ExceptionDataIsInvalid error)
                    {
                        Console.WriteLine("Name Of Exception: {0}\nMassage In Exception: {1}", error.GetType().Name, error.Message);
                    }
                    break;
                case 'c': // getting all of the products
                    IEnumerable<ProductForList> list = blList.Product.GetList();
                    foreach (ProductForList item in list)
                    {
                        Console.WriteLine(item);
                    }
                    break;
                case 'd': // updating a product
                    Product productToUpdate = new Product();
                    do
                    {
                        Console.Write("Enter id of the product: ");
                        validInput = int.TryParse(Console.ReadLine(), out id);
                        if (!validInput)
                            Console.WriteLine("please enter a valid input");
                    } while (!validInput);
                    productToUpdate.ID = id;
                    Console.Write("Enter name of the product: ");
                    productToUpdate.Name = Console.ReadLine();
                    do
                    {
                        Console.Write("Enter price of the product: ");
                        validInput = double.TryParse(Console.ReadLine(), out price);
                        if (!validInput)
                            Console.WriteLine("please enter a valid input");
                    } while (!validInput);
                    productToUpdate.Price = price;
                    do
                    {
                        Console.Write("Enter amount of the product: ");
                        validInput = int.TryParse(Console.ReadLine(), out amount);
                        if (!validInput)
                            Console.WriteLine("please enter a valid input");
                    } while (!validInput);
                    productToUpdate.InStock = amount;
                    do
                    { /// in the future, there would be a loop here that prints all the posible categories, because we might want to add more categories.
                        Console.Write(@"Optinal Categories:
                            Dresses,
                            Shirts,
                            Hats,
                            Shoes, 
                            Socks, 
                            Skirts,
                            Coats
                            Enter category of the product: ");
                        validInput = Enums.Category.TryParse(Console.ReadLine(), out category);
                        if (!validInput)
                            Console.WriteLine("please enter a valid input");
                    } while (!validInput);
                    productToUpdate.Category = category;
                    try
                    {
                        blList.Product.Update(productToUpdate);
                    }
                    catch (ExceptionDataIsInvalid error)
                    {
                        Console.WriteLine("Name Of Exception: {0}\nMassage In Exception: {1}", error.GetType().Name, error.Message);
                    }
                    catch (ExceptionLogicObjectCouldNotBeFound error)
                    {
                        Console.WriteLine("Name Of Exception: {0}\nMassage In Exception: {1}\nName Of Inner Exception: {2}\nMassage In Inner Exception: {3}", error.GetType().Name, error.Message, error.InnerException.GetType().Name, error.InnerException.Message);
                    }
                    break;
                case 'e': // deleting a product
                    do
                    {
                        Console.Write("Enter the id of the product you want to delete: ");
                        validInput = int.TryParse(Console.ReadLine(), out id);
                        if (!validInput)
                            Console.WriteLine("please enter a valid input");
                    } while (!validInput);
                    try
                    {
                        blList.Product.Delete(id);
                    }
                    catch (ExceptionLogicObjectAlreadyExist error)
                    {
                        Console.WriteLine("Name Of Exception: {0}\nMassage In Exception: {1}", error.GetType().Name, error.Message);
                    }
                    catch (ExceptionLogicObjectCouldNotBeFound error)
                    {
                        Console.WriteLine("Name Of Exception: {0}\nMassage In Exception: {1}\nName Of Inner Exception: {2}\nMassage In Inner Exception: {3}", error.GetType().Name, error.Message, error.InnerException.GetType().Name, error.InnerException.Message);
                    }
                    break;
                case 'f': // getting data of a product in a cart
                    do
                    {
                        Console.Write("Enter id of the product: ");
                        validInput = int.TryParse(Console.ReadLine(), out id);
                        if (!validInput)
                            Console.WriteLine("please enter a valid input");
                    } while (!validInput);
                    try
                    {
                        Console.WriteLine(blList.Product.GetForCustomer(id, cart));
                    }
                    catch (ExceptionLogicObjectCouldNotBeFound error)
                    {
                        Console.WriteLine("Name Of Exception: {0}\nMassage In Exception: {1}\nName Of Inner Exception: {2}\nMassage In Inner Exception: {3}", error.GetType().Name, error.Message, error.InnerException.GetType().Name, error.InnerException.Message);
                    }
                    catch (ExceptionDataIsInvalid error)
                    {
                        Console.WriteLine("Name Of Exception: {0}\nMassage In Exception: {1}", error.GetType().Name, error.Message);
                    }
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    didSomethingInThisSession = false;
                    break;
            }
        } while (!didSomethingInThisSession);
    }
    /// <summary>
    /// The function to test all the cart functions
    /// </summary>
    static void CartOption() // cart option
    {
        char choiceInSubSwitch = 'x';
        bool validInput = true;
        bool didSomethingInThisSession;

        do
        {
            didSomethingInThisSession = true;

            do
            {
                Console.WriteLine(@"you chose: Cart 
                   a ==> for adding a product to a cart
                   b ==> for updating the amount of a product in a cart
                   c ==> for making a cart into a real order");

                validInput = char.TryParse(Console.ReadLine(), out choiceInSubSwitch);
                if (!validInput)
                    Console.WriteLine("please enter a valid input");
            } while (!validInput);
            int id;
            int amount;
            string name, address, email;
            switch (choiceInSubSwitch)
            {
                case 'a': // adding a product to a cart
                    do
                    {
                        Console.Write("please enter the id of the product: ");
                        validInput = int.TryParse(Console.ReadLine(), out id);
                        if (!validInput)
                            Console.WriteLine("please enter a valid input");
                    } while (!validInput);
                    try
                    {
                        cart = blList.Cart.AddProduct(cart, id); 
                    }
                    catch (ExceptionObjectIsNotAviliable error)
                    {
                        Console.WriteLine("Name Of Exception: {0}\nMassage In Exception: {1}", error.GetType().Name, error.Message);
                    }
                    catch (ExceptionLogicObjectCouldNotBeFound error)
                    {
                        Console.WriteLine("Name Of Exception: {0}\nMassage In Exception: {1}\nName Of Inner Exception: {2}\nMassage In Inner Exception: {3}", error.GetType().Name, error.Message, error.InnerException.GetType().Name, error.InnerException.Message);
                    }
                    catch (ExceptionNotEnoughInDataBase error)
                    {
                        Console.WriteLine("Name Of Exception: {0}\nMassage In Exception: {1}\n", error.GetType().Name, error.Message);
                    }
                    break;
                case 'b': // updating the amount of a product in a cart
                    do
                    {
                        Console.Write("please enter the id of the product: ");
                        validInput = int.TryParse(Console.ReadLine(), out id);
                        if (!validInput)
                            Console.WriteLine("please enter a valid input");
                    } while (!validInput);
                    do
                    {
                        Console.Write("please enter the new amount of the product in the cart: ");
                        validInput = int.TryParse(Console.ReadLine(), out amount);
                        if (!validInput)
                            Console.WriteLine("please enter a valid input");
                    } while (!validInput);
                    try
                    {
                        cart = blList.Cart.UpdateAmount(cart,id,amount);
                    }
                    catch (ExceptionObjectIsNotAviliable error)
                    {
                        Console.WriteLine("Name Of Exception: {0}\nMassage In Exception: {1}", error.GetType().Name, error.Message);
                    }
                    catch (ExceptionNotEnoughInDataBase error)
                    {
                        Console.WriteLine("Name Of Exception: {0}\nMassage In Exception: {1}", error.GetType().Name, error.Message);
                    }
                    catch (ExceptionLogicObjectAlreadyExist error)
                    {
                        Console.WriteLine("Name Of Exception: {0}\nMassage In Exception: {1}\nName Of Inner Exception: {2}\nMassage In Inner Exception: {3}", error.GetType().Name, error.Message, error.InnerException.GetType().Name, error.InnerException.Message);
                    }
                    catch (ExceptionLogicObjectCouldNotBeFound error)
                    {
                        Console.WriteLine("Name Of Exception: {0}\nMassage In Exception: {1}\n", error.GetType().Name, error.Message) ;
                    }
                    catch(ExceptionDataIsInvalid error)
                    {
                        Console.WriteLine("Name Of Exception: {0}\nMassage In Exception: {1}\n", error.GetType().Name, error.Message);
                    }
                    break;
                case 'c': // making a cart into a real order
                    Console.WriteLine("enter name ");
                    name = Console.ReadLine();
                    Console.WriteLine("Enter address");
                    address = Console.ReadLine();
                    Console.WriteLine("enter email");
                    email = Console.ReadLine();
                    try
                    {
                        blList.Cart.MakeOrder(cart,name, address, email); 
                        cart = new Cart();
                    }
                    catch (ExceptionObjectIsNotAviliable error)
                    {
                        Console.WriteLine("Name Of Exception: {0}\nMassage In Exception: {1}", error.GetType().Name, error.Message);
                    }
                    catch (ExceptionLogicObjectCouldNotBeFound error)
                    {
                        Console.WriteLine("Name Of Exception: {0}\nMassage In Exception: {1}\nName Of Inner Exception: {2}\nMassage In Inner Exception: {3}", error.GetType().Name, error.Message, error.InnerException.GetType().Name, error.InnerException.Message);
                    }
                    catch (ExceptionDataIsInvalid error)
                    {
                        Console.WriteLine("Name Of Exception: {0}\nMassage In Exception: {1}\n", error.GetType().Name, error.Message);
                    }
                    catch (ExceptionNotEnoughInDataBase error)
                    {
                        Console.WriteLine("Name Of Exception: {0}\nMassage In Exception: {1}\n", error.GetType().Name, error.Message);
                    }
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    didSomethingInThisSession = false;
                    break;
            }
        } while (!didSomethingInThisSession);
    }
}
