// See https://aka.ms/new-console-template for more information
using Dal;
using System;
using System.Collections.Specialized;
using System.Data.Common;

namespace Program
{
    class Program
    {
         private DalOrder dalOrder = new DalOrder();
         private DalOrderItem dalOrderItem = new DalOrderItem();
         private DalProduct dalProduct = new DalProduct();
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
                        DalOrder order = new DalOrder();
                        order.ID = 
                        dalOrder.addOrder(order);
                        break;
                    case "b":
                        break;
                    case "c":
                        break;
                    case "d":
                        break;
                    case "e":
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                };
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
                choiceInSubSwitch switch
                {
                    "a" => ,
                    "b" => ,
                    "b" => ,
                    "d" => ,
                    "e" => ,
                                    => Console.WriteLine("Invalid choice");
                            };
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
                choiceInSubSwitch switch
                {
                    "a" => ,
                    "b" => ,
                    "b" => ,
                    "d" => ,
                    "e" => ,
                    "f" => ,
                    "g" => ,
                                    => Console.WriteLine("Invalid choice");
                            };
            }
            catch (InvalidCastException msgError)
            {
                Console.WriteLine(msgError.Message);
            }
        }
    }
}