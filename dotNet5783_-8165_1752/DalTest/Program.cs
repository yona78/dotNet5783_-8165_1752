// See https://aka.ms/new-console-template for more information
using Dal;
using DO;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Common;

namespace Program
{
    class Program
    {
        static private DalOrder dalOrder = new DalOrder();
        static private DalOrderItem dalOrderItem = new DalOrderItem();
        static private DalProduct dalProduct = new DalProduct();
        static void Main()
        {

            int choice = 0;
            do
            {
                Console.WriteLine("Welcoome to Yona's and Avishai's shop, you might choose to do some things on our shop." +
                    "0 for exit" +
                    "1 for Order" +
                    "2 for Product" +
                    "3 OrderItem");
                choice = Convert.ToInt32(Console.ReadLine());
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
            string choiceInSubSwitch = "x";

            Console.WriteLine("you chose: Order" +
                   "a for adding a new order" +
                   "b for getting an order by its id" +
                   "c for getting a data of an order" +
                   "d for updating an order" +
                   "e for deleting an order");
            string CustomerName, CustomerEmail, CustomerAdrress;
            DateTime OrderDate, ShipDate, DeliveryrDate;
            DalOrder newDalOrder = new DalOrder();

            choiceInSubSwitch = Console.ReadLine();
            try
            {
                switch (choiceInSubSwitch)
                {
                    case "a":
                        Console.Write("please enter CustomerName: ");
                        CustomerName = Console.ReadLine();
                        Console.Write("please enter CustomerEmail: ");
                        CustomerEmail = Console.ReadLine();
                        Console.Write("please enter CustomerAdrress: ");
                        CustomerAdrress = Console.ReadLine();
                        OrderDate = DateTime.Now;
                        ShipDate = DateTime.MinValue;
                        DeliveryrDate = DateTime.MinValue;
                        Order order = new Order();
                        order.CustomerName = CustomerName;
                        order.CustomerEmail = CustomerEmail;
                        order.CustomerAdrress = CustomerAdrress;
                        order.OrderDate = OrderDate;
                        order.ShipDate = ShipDate;
                        order.DeliveryrDate = DeliveryrDate;
                        Console.WriteLine("id of new order: "+dalOrder.addOrder(order));
                        break;
                    case "b":
                        Console.Write("please enter me an id: ");
                        int id = Console.Read();
                        Console.WriteLine(dalOrder.getOrder(id));
                        break;
                    case "c":
                        Order[] array = dalOrder.getDataOfOrder();
                        foreach (Order item in array)
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    case "d":
                        Console.Write("please enter me an id: ");
                        int idToDelete = int.Parse(Console.ReadLine());
                        try
                        {
                            dalOrder.deleteOrder(idToDelete);
                        }
                        catch (InvalidCastException msgError)
                        {
                            Console.WriteLine(msgError.Message);
                        }
                        break;
                    case "e":
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
            catch (InvalidCastException msgError)
            {
                Console.WriteLine(msgError.Message);
            }
        }

        static void ProductOption()
        {
            string choiceInSubSwitch = "x";

            Console.WriteLine("you chose: Product" +
                   "a for adding a new product" +
                   "b for getting an product by its id" +
                   "c for getting a data of an product" +
                   "d for updating an product" +
                   "e for deleting an product");
            choiceInSubSwitch = Console.ReadLine();
            try
            {
                switch (choiceInSubSwitch)
                {
                    case "a":
                        Product help = new Product();
                        Console.WriteLine("Enter id of the product");
                        help.ID = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter name of the product");
                        help.Name = Console.ReadLine();
                        Console.WriteLine("Enter price of the product");
                        help.Price = double.Parse(Console.ReadLine());
                        Console.WriteLine("Enter amount of the product");
                        help.InStock = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter category of the product");
                        Console.WriteLine(Enums.Category
                        help.Category = Console.ReadLine();
                        break;
                    case "b":
                        Console.WriteLine("Enter id of the product");
                        int id = int.Parse(Console.ReadLine());
                        Product got = dalProduct.getProduct(id);
                        Console.WriteLine(got);
                        break;
                    case "c":
                        Product [] array = dalProduct.getDataOfProduct();
                        foreach (Product item in array)
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    case "d":
                        Product help = new Product();
                        Console.WriteLine("Enter id of the product");
                        help.ID = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter name of the product");
                        help.Name = Console.ReadLine();
                        Console.WriteLine("Enter price of the product");
                        help.Price = double.Parse(Console.ReadLine());
                        Console.WriteLine("Enter amount of the product");
                        help.InStock = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter category of the product");
                        Console.WriteLine(Enums.Category
                        help.Category = Console.ReadLine();
                        try
                        {
                            dalProduct.updateProduct(help);
                        }
                        catch (InvalidCastException msgError)
                        {
                            Console.WriteLine(msgError.Message);
                        }
                        break;
                    case "e":
                        Console.WriteLine("Enter id of the product you want to delete");
                        int idGot = int.Parse(Console.ReadLine());
                        try
                        {
                            dalProduct.deleteProduct(idGot);
                        }
                        catch (InvalidCastException msgError)
                        {
                            Console.WriteLine(msgError.Message);
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
            catch (InvalidCastException msgError)
            {
                Console.WriteLine(msgError.Message);
            }
        }

        static void OrderItemOption()
        {
            string choiceInSubSwitch = "x";

            Console.WriteLine("you chose: OrderItem" +
                   "a for adding a new orderItem" +
                   "b for getting an orderItem by its id" +
                   "c for getting a data of an orderItem" +
                   "d for updating an orderItem" +
                   "e for deleting an orderItem" +
                   "f for getting an orderItem by order and product" +
                   "g for getting the data of an order by its id");
            choiceInSubSwitch = Console.ReadLine();
            try
            {
                switch (choiceInSubSwitch)
                {
                    case "a":
                        
                        break;
                    case "b":
                        Console.Write("please enter me an id of OrderItem: ");
                        int idForOrderItem = int.Parse(Console.ReadLine());
                        Console.WriteLine(dalOrderItem.getOrderItem(idForOrderItem));
                        break;
                    case "c":
                        OrderItem[] array = dalOrderItem.getDataOfOrderItem();
                        foreach (OrderItem item in array)
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    case "d":
                        Console.Write("please enter me an id of order: ");
                        int idToDeleteOrder = int.Parse(Console.ReadLine());
                        Console.Write("please enter me an id of product: ");
                        int idToDeleteProdcut = int.Parse(Console.ReadLine());
                        try
                        {
                            dalOrderItem.deleteOrderItem(idToDeleteOrder,idToDeleteProdcut);
                        }
                        catch (InvalidCastException msgError)
                        {
                            Console.WriteLine(msgError.Message);
                        }
                        break;
                    case "e":

                        break;
                    case "f":
                        Console.Write("please enter me an id of order: ");
                        int idForOrder = int.Parse(Console.ReadLine());
                        Console.Write("please enter me an id of product: ");
                        int idForProduct = int.Parse(Console.ReadLine());
                        Console.WriteLine(dalOrderItem.getOrderItem(idForOrder,idForProduct));
                        break;
                    case "g":
                        Console.Write("please enter me an id: ");
                        int idForList = int.Parse(Console.ReadLine());
                        List<OrderItem> ret = dalOrderItem.getDataOfOrderItem(idForList);
                        foreach (OrderItem item in ret)
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
            catch (InvalidCastException msgError)
            {
                Console.WriteLine(msgError.Message);
            }
        }
    }
}