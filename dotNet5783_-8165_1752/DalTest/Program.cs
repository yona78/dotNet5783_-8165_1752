// See https://aka.ms/new-console-template for more information
using Dal;
using DO;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;

namespace Program
{
    class Program
    {
        static private DalOrder dalOrder = new DalOrder();
        static private DalOrderItem dalOrderItem = new DalOrderItem();
        static private DalProduct dalProduct = new DalProduct();

        static void Main()
        {
            bool validInput = true;

            int choice = 0;
            do
            {
                do
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
        static void OrderOption()
        {
            char choiceInSubSwitch = 'x';
            bool validInput = true;
            bool didSomethingInThisSession;

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
                    case 'a':
                        string CustomerName, CustomerEmail, CustomerAdrress;
                        DateTime OrderDate, ShipDate, DeliveryrDate;
                        DalOrder newDalOrder = new DalOrder();
                        Console.Write("please enter CustomerName (first name only): ");
                        CustomerName = Console.ReadLine();
                        Console.Write("please enter CustomerEmail (format: example@gmail.com): ");
                        CustomerEmail = Console.ReadLine();
                        Console.Write("please enter CustomerAdrress (format: ): ");
                        CustomerAdrress = Console.ReadLine();
                        OrderDate = DateTime.Now;
                        ShipDate = DateTime.MinValue;
                        DeliveryrDate = DateTime.MinValue;
                        Order orderToAdd = new Order();
                        orderToAdd.CustomerName = CustomerName;
                        orderToAdd.CustomerEmail = CustomerEmail;
                        orderToAdd.CustomerAdrress = CustomerAdrress;
                        orderToAdd.OrderDate = OrderDate;
                        orderToAdd.ShipDate = ShipDate;
                        orderToAdd.DeliveryrDate = DeliveryrDate;
                        try
                        {
                            Console.WriteLine("id of new order: " + dalOrder.addOrder(orderToAdd));
                        }
                        catch (InvalidCastException msgError)
                        {
                            Console.WriteLine(msgError.Message);
                        }
                        break;
                    case 'b':
                        do
                        {
                            Console.Write("please enter me an id: ");
                            validInput = int.TryParse(Console.ReadLine(), out id);
                            if (!validInput)
                                Console.WriteLine("please enter a valid input");
                        } while (!validInput);

                        try
                        {
                            Console.WriteLine(dalOrder.getOrder(id));
                        }
                        catch (InvalidCastException msgError)
                        {
                            Console.WriteLine(msgError.Message);
                        }
                        break;
                    case 'c':
                        try
                        {
                            Order[] array = dalOrder.getDataOfOrder();

                            foreach (Order item in array)
                            {
                                Console.WriteLine(item);
                            }
                        }
                        catch (InvalidCastException msgError)
                        {
                            Console.WriteLine(msgError.Message);
                        }
                        break;
                    case 'd':
                        Console.Write("please enter CustomerName (first name only): ");
                        CustomerName = Console.ReadLine();
                        Console.Write("please enter CustomerEmail (format: example@gmail.com): ");
                        CustomerEmail = Console.ReadLine();
                        Console.Write("please enter CustomerAdrress: ");
                        CustomerAdrress = Console.ReadLine();
                        OrderDate = DateTime.Now;
                        ShipDate = DateTime.MinValue;
                        DeliveryrDate = DateTime.MinValue;
                        Order orderToUpdate = new Order();
                        orderToUpdate.CustomerName = CustomerName;
                        orderToUpdate.CustomerEmail = CustomerEmail;
                        orderToUpdate.CustomerAdrress = CustomerAdrress;
                        orderToUpdate.OrderDate = OrderDate;
                        orderToUpdate.ShipDate = ShipDate;
                        orderToUpdate.DeliveryrDate = DeliveryrDate;
                        try
                        {
                            dalOrder.updateOrder(orderToUpdate);
                        }
                        catch (InvalidCastException msgError)
                        {
                            Console.WriteLine(msgError.Message);
                        }
                        break;
                    case 'e':
                        do
                        {
                            Console.Write("please enter me an id: ");
                            validInput = int.TryParse(Console.ReadLine(), out id);
                            if (!validInput)
                                Console.WriteLine("please enter a valid input");
                        } while (!validInput);

                        try
                        {
                            dalOrder.deleteOrder(id);
                        }
                        catch (InvalidCastException msgError)
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
        static void ProductOption()
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
                    case 'a':
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
                        {
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
                            Console.WriteLine("id of new product: " + dalProduct.addProduct(productToAdd));
                        }
                        catch (InvalidCastException msgError)
                        {
                            Console.WriteLine(msgError.Message);
                        }
                        break;
                    case 'b':
                        do
                        {
                            Console.Write("Enter id of the product: ");
                            validInput = int.TryParse(Console.ReadLine(), out id);
                            if (!validInput)
                                Console.WriteLine("please enter a valid input");
                        } while (!validInput);
                        try
                        {
                            Console.WriteLine(dalProduct.getProduct(id));
                        }
                        catch (InvalidCastException msgError)
                        {
                            Console.WriteLine(msgError.Message);
                        }
                        break;
                    case 'c':
                        Product[] array = dalProduct.getDataOfProduct();
                        foreach (Product item in array)
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    case 'd':
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
                        {
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
                            dalProduct.updateProduct(productToUpdate);
                        }
                        catch (InvalidCastException msgError)
                        {
                            Console.WriteLine(msgError.Message);
                        }
                        break;
                    case 'e':
                        do
                        {
                            Console.Write("Enter the id of the product you want to delete: ");
                            validInput = int.TryParse(Console.ReadLine(), out id);
                            if (!validInput)
                                Console.WriteLine("please enter a valid input");
                        } while (!validInput);
                        try
                        {
                            dalProduct.deleteProduct(id);
                        }
                        catch (InvalidCastException msgError)
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
        static void OrderItemOption()
        {
            char choiceInSubSwitch = 'x';
            bool validInput = true;
            bool didSomethingInThisSession;

            do
            {
                didSomethingInThisSession = true;

                do
                {
                    Console.WriteLine("you chose: OrderItem" +
                   "a ==> for adding a new orderItem" +
                   "b ==> for getting an orderItem by its id" +
                   "c ==> for getting a data of an orderItem" +
                   "d ==> for updating an orderItem" +
                   "e ==> for deleting an orderItem" +
                   "f ==> for getting an orderItem by order and product" +
                   "g ==> for getting the data of an order by its id");

                    validInput = char.TryParse(Console.ReadLine(), out choiceInSubSwitch);
                    if (!validInput)
                        Console.WriteLine("please enter a valid input");
                } while (!validInput);
                int id;
                double price;
                int amount;
                switch (choiceInSubSwitch)
                {
                    case 'a':
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
                            Console.Write("please enter Price: ");
                            validInput = double.TryParse(Console.ReadLine(), out price);
                            if (!validInput)
                                Console.WriteLine("please enter a valid input");
                        } while (!validInput);
                        double priceOrderItem = price;
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
                        orderItemToAdd.Price = priceOrderItem;
                        orderItemToAdd.Amount = amountOrdetItem;
                        try
                        {
                            Console.WriteLine("id of new orderItem: " + dalOrderItem.addOrderItem(orderItemToAdd));
                        }
                        catch (InvalidCastException msgError)
                        {
                            Console.WriteLine(msgError.Message);
                        }

                        break;
                    case 'b':
                        do
                        {
                            Console.Write("please enter me an id of OrderItem: ");
                            validInput = int.TryParse(Console.ReadLine(), out id);
                            if (!validInput)
                                Console.WriteLine("please enter a valid input");
                        } while (!validInput);
                        int idForOrderItem = id;
                        Console.WriteLine(dalOrderItem.getOrderItem(idForOrderItem));
                        break;
                    case 'c':
                        OrderItem[] array = dalOrderItem.getDataOfOrderItem();
                        foreach (OrderItem item in array)
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    case 'd':
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
                            Console.Write("please enter Price: ");
                            validInput = double.TryParse(Console.ReadLine(), out price);
                            if (!validInput)
                                Console.WriteLine("please enter a valid input");
                        } while (!validInput);
                        double priceOrderItemUpdate = price;
                        do
                        {
                            Console.Write("please enter Amount: ");
                            validInput = int.TryParse(Console.ReadLine(), out amount);
                            if (!validInput)
                                Console.WriteLine("please enter a valid input");
                        } while (!validInput);
                        int amountOrdetItemUpdate = amount;

                        OrderItem orderItemToUpdate = new OrderItem();
                        orderItemToUpdate.ProductID = productIdUpdate;
                        orderItemToUpdate.OrderID = orderIdUpdate;
                        orderItemToUpdate.Price = priceOrderItemUpdate;
                        orderItemToUpdate.Amount = amountOrdetItemUpdate;
                        try
                        {
                            dalOrderItem.updateOrderItem(orderItemToUpdate);
                        }
                        catch (InvalidCastException msgError)
                        {
                            Console.WriteLine(msgError.Message);
                        }
                        break;
                    case 'e':
                        do
                        {
                            Console.Write("please enter me an id of order: ");
                            validInput = int.TryParse(Console.ReadLine(), out id);
                            if (!validInput)
                                Console.WriteLine("please enter a valid input");
                        } while (!validInput);
                        int idToDeleteOrder = id;
                        do
                        {
                            Console.Write("please enter me an id of product: ");
                            validInput = int.TryParse(Console.ReadLine(), out id);
                            if (!validInput)
                                Console.WriteLine("please enter a valid input");
                        } while (!validInput);
                        int idToDeleteProdcut = id;
                        try
                        {
                            dalOrderItem.deleteOrderItem(idToDeleteOrder, idToDeleteProdcut);
                        }
                        catch (InvalidCastException msgError)
                        {
                            Console.WriteLine(msgError.Message);
                        }
                        break;
                    case 'f':
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
                        Console.WriteLine(dalOrderItem.getOrderItem(idForOrder, idForProduct));
                        break;
                    case 'g':
                        do
                        {
                            Console.Write("please enter me an id: ");
                            validInput = int.TryParse(Console.ReadLine(), out id);
                            if (!validInput)
                                Console.WriteLine("please enter a valid input");
                        } while (!validInput);
                        int idForList = id;
                        List<OrderItem> ret = dalOrderItem.getDataOfOrderItem(idForList);
                        foreach (OrderItem item in ret)
                        {
                            Console.WriteLine(item);
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