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
            choiceInSubSwitch = Console.ReadLine();

            switch (choiceInSubSwitch)
            {
                case "a":
                    string CustomerName, CustomerEmail, CustomerAdrress;
                    DateTime OrderDate, ShipDate, DeliveryrDate;
                    DalOrder newDalOrder = new DalOrder();
                    Console.Write("please enter CustomerName: ");
                    CustomerName = Console.ReadLine();
                    Console.Write("please enter CustomerEmail: ");
                    CustomerEmail = Console.ReadLine();
                    Console.Write("please enter CustomerAdrress: ");
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
                case "b":
                    Console.Write("please enter me an id: ");
                    int id = Console.Read();
                    try
                    {
                        Console.WriteLine(dalOrder.getOrder(id));
                    }
                    catch (InvalidCastException msgError)
                    {
                        Console.WriteLine(msgError.Message);
                    }
                    break;
                case "c":
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
                case "d":
                    Console.Write("please enter CustomerName: ");
                    CustomerName = Console.ReadLine();
                    Console.Write("please enter CustomerEmail: ");
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
                case "e":
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
                default:
                    Console.WriteLine("Invalid choice");
                    break;
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

            switch (choiceInSubSwitch)
            {
                case "a":
                    Product productToAdd = new Product();
                    Console.WriteLine("Enter id of the product: ");
                    productToAdd.ID = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter name of the product: ");
                    productToAdd.Name = Console.ReadLine();
                    Console.WriteLine("Enter price of the product: ");
                    productToAdd.Price = double.Parse(Console.ReadLine());
                    Console.WriteLine("Enter amount of the product: ");
                    productToAdd.InStock = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter category of the product: ");
                    productToAdd.Category = (Enums.Category)Enums.Category.Parse(typeof(Enums.Category), Console.ReadLine());
                    try
                    {
                        Console.WriteLine("id of new product: " + dalProduct.addProduct(productToAdd));
                    }
                    catch (InvalidCastException msgError)
                    {
                        Console.WriteLine(msgError.Message);
                    }
                    break;
                case "b":
                    Console.WriteLine("Enter id of the product: ");
                    int id = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.WriteLine(dalProduct.getProduct(id));
                    }
                    catch (InvalidCastException msgError)
                    {
                        Console.WriteLine(msgError.Message);
                    }
                    break;
                case "c":
                    Product[] array = dalProduct.getDataOfProduct();
                    foreach (Product item in array)
                    {
                        Console.WriteLine(item);
                    }
                    break;
                case "d":
                    Product productToUpdate = new Product();
                    Console.WriteLine("Enter id of the product: ");
                    productToUpdate.ID = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter name of the product: ");
                    productToUpdate.Name = Console.ReadLine();
                    Console.WriteLine("Enter price of the product: ");
                    productToUpdate.Price = double.Parse(Console.ReadLine());
                    Console.WriteLine("Enter amount of the product: ");
                    productToUpdate.InStock = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter category of the product: ");
                    productToUpdate.Category = (Enums.Category)Enums.Category.Parse(typeof(Enums.Category), Console.ReadLine());
                    try
                    {
                        dalProduct.updateProduct(productToUpdate);
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

            switch (choiceInSubSwitch)
            {
                case "a":
                    Console.Write("please enter ProductID: ");
                    int ProductID = int.Parse(Console.ReadLine());
                    Console.Write("please enter OrderID: ");
                    int OrderID = int.Parse(Console.ReadLine());
                    Console.Write("please enter Price: ");
                    double Price = double.Parse(Console.ReadLine());
                    Console.Write("please enter Amount: ");
                    int Amount = int.Parse(Console.ReadLine());
                    OrderItem orderItemToAdd = new OrderItem();

                    orderItemToAdd.ProductID = ProductID;
                    orderItemToAdd.OrderID = OrderID;
                    orderItemToAdd.Price = Price;
                    orderItemToAdd.Amount = Amount;
                    try
                    {
                        Console.WriteLine("id of new orderItem: " + dalOrderItem.addOrderItem(orderItemToAdd));
                    }
                    catch (InvalidCastException msgError)
                    {
                        Console.WriteLine(msgError.Message);
                    }

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
                    OrderItem orderItemToUpdate = new OrderItem();
                    Console.Write("please enter OrderItemID: ");
                    orderItemToUpdate.OrderItemID = int.Parse(Console.ReadLine());
                    Console.Write("please enter ProductID: ");
                    orderItemToUpdate.ProductID = int.Parse(Console.ReadLine());
                    Console.Write("please enter Price: ");
                    orderItemToUpdate.Price = double.Parse(Console.ReadLine());
                    Console.Write("please enter Amount: ");
                    orderItemToUpdate.Amount = int.Parse(Console.ReadLine());
                    try
                    {
                        dalOrderItem.updateOrderItem(orderItemToUpdate);
                    }
                    catch (InvalidCastException msgError)
                    {
                        Console.WriteLine(msgError.Message);
                    }
                    break;
                case "e":
                    Console.Write("please enter me an id of order: ");
                    int idToDeleteOrder = int.Parse(Console.ReadLine());
                    Console.Write("please enter me an id of product: ");
                    int idToDeleteProdcut = int.Parse(Console.ReadLine());
                    try
                    {
                        dalOrderItem.deleteOrderItem(idToDeleteOrder, idToDeleteProdcut);
                    }
                    catch (InvalidCastException msgError)
                    {
                        Console.WriteLine(msgError.Message);
                    }
                    break;
                case "f":
                    Console.Write("please enter me an id of order: ");
                    int idForOrder = int.Parse(Console.ReadLine());
                    Console.Write("please enter me an id of product: ");
                    int idForProduct = int.Parse(Console.ReadLine());
                    Console.WriteLine(dalOrderItem.getOrderItem(idForOrder, idForProduct));
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
    }
}