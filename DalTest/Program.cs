using DalApi;
using DO;

namespace Program
{

    class Program
    {
        static IDal? dal = Factory.Get();

        // static private DalOrder _dalOrder = new DalOrder();
        // static private DalOrderItem _dalOrderItem = new DalOrderItem();
        // static private DalProduct _dalProduct = new DalProduct();

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
                    3 ==> OrderItem");
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
                        OrderItemOption();
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            } while (choice != 0);

        }
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
                   a ==> for adding a new order
                   b ==> for getting an order by its id
                   c ==> for getting a data of an order
                   d ==> for updating an order
                   e ==> for deleting an order");
                    validInput = char.TryParse(Console.ReadLine(), out choiceInSubSwitch);
                    if (!validInput)
                        Console.WriteLine("please enter a valid input");
                } while (!validInput);
                int id;

                switch (choiceInSubSwitch)
                {
                    case 'a': // addOrder option
                        string? customerName, customerEmail, customerAdrress;
                        DateTime orderDate, shipDate, deliveryrDate;
                        Console.Write("please enter CustomerName (first name only): ");
                        customerName = Console.ReadLine();
                        Console.Write("please enter CustomerEmail (format: example@gmail.com): ");
                        customerEmail = Console.ReadLine();
                        Console.Write("please enter CustomerAddress (format: (number of apartment) (name of street) Street       (Zip number) (name of city)       Israel): ");
                        customerAdrress = Console.ReadLine();
                        orderDate = DateTime.Now;
                        shipDate = DateTime.MinValue;
                        deliveryrDate = DateTime.MinValue;
                        Order orderToAdd = new Order();
                        orderToAdd.CustomerName = customerName;
                        orderToAdd.CustomerEmail = customerEmail;
                        orderToAdd.CustomerAddress = customerAdrress;
                        orderToAdd.OrderDate = orderDate;
                        orderToAdd.ShipDate = shipDate;
                        orderToAdd.DeliveryDate = deliveryrDate;
                        try
                        {
                            Console.WriteLine("id of new order: " + dal.Order.Add(orderToAdd));
                        }
                        catch (Exception msgError)
                        {
                            Console.WriteLine(msgError.Message);
                        }
                        break;
                    case 'b': // getting order by its id option
                        do
                        {
                            Console.Write("please enter me an id: ");
                            validInput = int.TryParse(Console.ReadLine(), out id);
                            if (!validInput)
                                Console.WriteLine("please enter a valid input");
                        } while (!validInput);

                        try
                        {
                            Console.WriteLine(dal.Order.Get(id));
                        }
                        catch (Exception msgError)
                        {
                            Console.WriteLine(msgError.Message);
                        }
                        break;
                    case 'c': // print all orders option
                        try
                        {
                            IEnumerable<Order?> array = dal.Order.GetDataOf();

                            foreach (Order? item in array)
                            {
                                if ((item ?? new Order()).ID != 0)
                                {
                                    Console.WriteLine(item);
                                }
                            }
                        }
                        catch (Exception msgError)
                        {
                            Console.WriteLine(msgError.Message);
                        }
                        break;
                    case 'd': // update order option
                        Console.Write("please enter ID to update");
                        int idToUpdate;
                        do
                        {
                            Console.Write("please enter me an id: ");
                            validInput = int.TryParse(Console.ReadLine(), out idToUpdate);
                            if (!validInput)
                                Console.WriteLine("please enter a valid input");
                        } while (!validInput);
                        Console.Write("please enter CustomerName (first name only): ");
                        customerName = Console.ReadLine();
                        Console.Write("please enter CustomerEmail (format: example@gmail.com): ");
                        customerEmail = Console.ReadLine();
                        Console.Write("please enter CustomerAddress: ");
                        customerAdrress = Console.ReadLine();
                        orderDate = DateTime.Now;
                        shipDate = DateTime.MinValue;
                        deliveryrDate = DateTime.MinValue;
                        Order orderToUpdate = new Order();
                        orderToUpdate.ID = idToUpdate;
                        orderToUpdate.CustomerName = customerName;
                        orderToUpdate.CustomerEmail = customerEmail;
                        orderToUpdate.CustomerAddress = customerAdrress;
                        orderToUpdate.OrderDate = orderDate;
                        orderToUpdate.ShipDate = shipDate;
                        orderToUpdate.DeliveryDate = deliveryrDate;
                        try
                        {
                            dal.Order.Update(orderToUpdate);
                        }
                        catch (Exception msgError)
                        {
                            Console.WriteLine(msgError.Message);
                        }
                        break;
                    case 'e': // delete order option
                        do
                        {
                            Console.Write("please enter me an id: ");
                            validInput = int.TryParse(Console.ReadLine(), out id);
                            if (!validInput)
                                Console.WriteLine("please enter a valid input");
                        } while (!validInput);

                        try
                        {
                            dal.Order.Delete(id);
                        }
                        catch (Exception msgError)
                        {
                            Console.WriteLine(msgError.Message);
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        didSomethingInThisSession = false;
                        break;
                }
            } while (!didSomethingInThisSession);
        }
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
                   b ==> for getting an product by its id
                   c ==> for getting a data of an product
                   d ==> for updating an product
                   e ==> for deleting an product");
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
                            Console.WriteLine("index of new product: " + dal.Product.Add(productToAdd));
                        }
                        catch (Exception msgError)
                        {
                            Console.WriteLine(msgError.Message);
                        }
                        break;
                    case 'b': // getting product by its id option
                        do
                        {
                            Console.Write("Enter id of the product: ");
                            validInput = int.TryParse(Console.ReadLine(), out id);
                            if (!validInput)
                                Console.WriteLine("please enter a valid input");
                        } while (!validInput);
                        try
                        {
                            Console.WriteLine(dal.Product.Get(id));
                        }
                        catch (Exception msgError)
                        {
                            Console.WriteLine(msgError.Message);
                        }
                        break;
                    case 'c': // print all products option
                        IEnumerable<Product?> array = dal.Product.GetDataOf();
                        foreach (Product? item in array)
                        {
                            if ((item ?? new Product()).ID != 0)
                            {
                                Console.WriteLine(item);
                            }
                        }
                        break;
                    case 'd': // update product option
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
                            dal.Product.Update(productToUpdate);
                        }
                        catch (Exception msgError)
                        {
                            Console.WriteLine(msgError.Message);
                        }
                        break;
                    case 'e': // delete product option
                        do
                        {
                            Console.Write("Enter the id of the product you want to delete: ");
                            validInput = int.TryParse(Console.ReadLine(), out id);
                            if (!validInput)
                                Console.WriteLine("please enter a valid input");
                        } while (!validInput);
                        try
                        {
                            dal.Product.Delete(id);
                        }
                        catch (Exception msgError)
                        {
                            Console.WriteLine(msgError.Message);
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        didSomethingInThisSession = false;
                        break;
                }
            } while (!didSomethingInThisSession);
        }
        static void OrderItemOption() // orderItem option
        {
            char choiceInSubSwitch = 'x';
            bool validInput = true;
            bool didSomethingInThisSession;

            do
            {
                didSomethingInThisSession = true;

                do
                {
                    Console.WriteLine(@"you chose: OrderItem 
                   a ==> for adding a new orderItem 
                   b ==> for getting an orderItem by its id
                   c ==> for getting a data of an orderItem
                   d ==> for updating an orderItem 
                   e ==> for deleting an orderItem
                   f ==> for getting an orderItem by order and product
                   g ==> for getting the data of an order by its id");

                    validInput = char.TryParse(Console.ReadLine(), out choiceInSubSwitch);
                    if (!validInput)
                        Console.WriteLine("please enter a valid input");
                } while (!validInput);
                int id;
                int amount;
                switch (choiceInSubSwitch)
                {
                    case 'a': // add orderItem option
                        do
                        {
                            Console.Write("please enter ProductID: ");
                            validInput = int.TryParse(Console.ReadLine(), out id);
                            if (!validInput)
                                Console.WriteLine("please enter a valid input");
                        } while (!validInput);
                        int productId = id;
                        do
                        {
                            Console.Write("please enter OrderID: ");
                            validInput = int.TryParse(Console.ReadLine(), out id);
                            if (!validInput)
                                Console.WriteLine("please enter a valid input");
                        } while (!validInput);
                        int orderId = id;
                        do
                        {
                            Console.Write("please enter Amount: ");
                            validInput = int.TryParse(Console.ReadLine(), out amount);
                            if (!validInput)
                                Console.WriteLine("please enter a valid input");
                        } while (!validInput);
                        int amountOrdetItem = amount;

                        OrderItem orderItemToAdd = new OrderItem();
                        orderItemToAdd.ProductID = productId;
                        orderItemToAdd.OrderID = orderId;
                        orderItemToAdd.Price = 0;
                        orderItemToAdd.Amount = amountOrdetItem;
                        try
                        {
                            Console.WriteLine("id of new orderItem: " + dal.OrderItem.Add(orderItemToAdd));
                        }
                        catch (Exception msgError)
                        {
                            Console.WriteLine(msgError.Message);
                        }

                        break;
                    case 'b': // get orderItem by its id option
                        do
                        {
                            Console.Write("please enter me an id of OrderItem: ");
                            validInput = int.TryParse(Console.ReadLine(), out id);
                            if (!validInput)
                                Console.WriteLine("please enter a valid input");
                        } while (!validInput);
                        int idForOrderItem = id;
                        try
                        {
                            Console.WriteLine(dal.OrderItem.Get(idForOrderItem));
                        }
                        catch (Exception msgError)
                        {
                            Console.WriteLine(msgError.Message);
                        }
                        break;
                    case 'c': // print all orderItem option
                        IEnumerable<OrderItem?> array = dal.OrderItem.GetDataOf();
                        foreach (OrderItem? item in array)
                        {
                            if ((item ?? new OrderItem()).OrderItemID != 0)
                            {
                                Console.WriteLine(item);
                            }
                        }
                        break;
                    case 'd': // update orderItem option
                        do
                        {
                            Console.Write("please enter OrderItemID: ");
                            validInput = int.TryParse(Console.ReadLine(), out id);
                            if (!validInput)
                                Console.WriteLine("please enter a valid input");
                        } while (!validInput);
                        int OrderItemIdUpdate = id;
                        do
                        {
                            Console.Write("please enter ProductID: ");
                            validInput = int.TryParse(Console.ReadLine(), out id);
                            if (!validInput)
                                Console.WriteLine("please enter a valid input");
                        } while (!validInput);
                        int productIdUpdate = id;
                        do
                        {
                            Console.Write("please enter OrderID: ");
                            validInput = int.TryParse(Console.ReadLine(), out id);
                            if (!validInput)
                                Console.WriteLine("please enter a valid input");
                        } while (!validInput);
                        int orderIdUpdate = id;
                        do
                        {
                            Console.Write("please enter Amount: ");
                            validInput = int.TryParse(Console.ReadLine(), out amount);
                            if (!validInput)
                                Console.WriteLine("please enter a valid input");
                        } while (!validInput);
                        int amountOrdetItemUpdate = amount;

                        OrderItem orderItemToUpdate = new OrderItem();
                        orderItemToUpdate.OrderItemID = OrderItemIdUpdate;
                        orderItemToUpdate.ProductID = productIdUpdate;
                        orderItemToUpdate.OrderID = orderIdUpdate;
                        orderItemToUpdate.Price = 0;
                        orderItemToUpdate.Amount = amountOrdetItemUpdate;
                        try
                        {
                            dal.OrderItem.Update(orderItemToUpdate);
                        }
                        catch (Exception msgError)
                        {
                            Console.WriteLine(msgError.Message);
                        }
                        break;
                    case 'e': // delete orderItem option
                        do
                        {
                            Console.Write("please enter me an id of orderItem: ");
                            validInput = int.TryParse(Console.ReadLine(), out id);
                            if (!validInput)
                                Console.WriteLine("please enter a valid input");
                        } while (!validInput);
                        int idToDelete = id;
                        try
                        {
                            dal.OrderItem.Delete(idToDelete);
                        }
                        catch (Exception msgError)
                        {
                            Console.WriteLine(msgError.Message);
                        }
                        break;
                    case 'f': // get order item by its product id and order id
                        do
                        {
                            Console.Write("please enter me an id of order: ");
                            validInput = int.TryParse(Console.ReadLine(), out id);
                            if (!validInput)
                                Console.WriteLine("please enter a valid input");
                        } while (!validInput);
                        int idForOrder = id;
                        do
                        {
                            Console.Write("please enter me an id of product: ");
                            validInput = int.TryParse(Console.ReadLine(), out id);
                            if (!validInput)
                                Console.WriteLine("please enter a valid input");
                        } while (!validInput);
                        int idForProduct = id;
                        try
                        {
                            Console.WriteLine(dal.OrderItem.GetOrderItem(idForOrder, idForProduct));
                        }
                        catch (Exception msgError)
                        {
                            Console.WriteLine(msgError.Message);
                        }
                        break;
                    case 'g': // get all order items from specific order by its id option
                        do
                        {
                            Console.Write("please enter me an id: ");
                            validInput = int.TryParse(Console.ReadLine(), out id);
                            if (!validInput)
                                Console.WriteLine("please enter a valid input");
                        } while (!validInput);
                        int idForList = id;
                        try
                        {
                            IEnumerable<OrderItem?> ret = dal.OrderItem.GetDataOfOrderItem(idForList);
                            foreach (OrderItem? item in ret)
                            {
                                Console.WriteLine(item);
                            }
                        }
                        catch (Exception msgError)
                        {
                            Console.WriteLine(msgError.Message);
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
}