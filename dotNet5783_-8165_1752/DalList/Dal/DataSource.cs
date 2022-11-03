using DO;
namespace Dal;
internal static class DataSource
{

    static readonly Random rnd = new Random();
    static internal Order[] orders = new Order[100];
    public static int maxOrders = 100;
    static internal Product[] products = new Product[50];
    public static int maxProducts = 50;
    static internal OrderItem[] orderItems = new OrderItem[200];
    public static int maxOrderItems = 200;



    static DataSource()
    {
        s_Initialize();
    }

    private static void addOrder(Order newOrder)
    {

        orders[Config.FirstIndexOrders] = newOrder;
        Config.FirstIndexOrders++;
    }

    private static void addProduct(Product newProduct)
    {
        products[Config.FirstIndexProducts] = newProduct;
        Config.FirstIndexProducts++;
    }

    private static void addOrdersItem(OrderItem newOrderItem)
    {
        orderItems[Config.FirstIndexOrderItems] = newOrderItem;
        Config.FirstIndexOrderItems++;
    }

    static void s_Initialize()
    {
        /// constant for loop limit - right programing rules 
        const int ProductInit = 10;
        const int OrderInit = 20;
        const int OrderItemsInit = 40;

        /// Products initializetion
        int[] arrayOfRandomNumbersForProducts = new int[ProductInit];
        /// the next code will check whether there are duplicate Id's
        bool checkForDuplicateId = true;
        do
        {
            checkForDuplicateId = true;
            for (int i = 0; i < ProductInit; i++)
            {
                arrayOfRandomNumbersForProducts[i] = rnd.Next(100000, 999999);
            }
            for (int i = 0; i < ProductInit; i++)
            {
                int currentId = arrayOfRandomNumbersForProducts[i];
                for (int j = i + 1; j < ProductInit; j++)
                {
                    if(arrayOfRandomNumbersForProducts[j] == currentId)
                    {
                        checkForDuplicateId = false;
                    }
                }
            }
        } while (!checkForDuplicateId);
        /// there must be one product with empty amount (InStock=0)
        Product product;
        product = new Product { ID = arrayOfRandomNumbersForProducts[0], Name = "Black coat", Category = Enums.Category.Coats, InStock = 50, Price = 399.99 };
        addProduct(product);
        product = new Product { ID = arrayOfRandomNumbersForProducts[1], Name = "Winter dress", Category = Enums.Category.Dresses, InStock = 30, Price = 150 };
        addProduct(product);
        product = new Product { ID = arrayOfRandomNumbersForProducts[2], Name = "Cowboy hat", Category = Enums.Category.Hats, InStock = 20, Price = 30.99 };
        addProduct(product);
        product = new Product { ID = arrayOfRandomNumbersForProducts[3], Name = "Sport shoes", Category = Enums.Category.Shoes, InStock = 39, Price = 299.99 };
        addProduct(product);
        product = new Product { ID = arrayOfRandomNumbersForProducts[4], Name = "White socks", Category = Enums.Category.Socks, InStock = 24, Price = 15 };
        addProduct(product);
        product = new Product { ID = arrayOfRandomNumbersForProducts[5], Name = "Striped skirt", Category = Enums.Category.Skirts, InStock = 0, Price = 70.99 };
        addProduct(product);
        product = new Product { ID = arrayOfRandomNumbersForProducts[6], Name = "Blue shirt", Category = Enums.Category.Shirts, InStock = 12, Price = 35 };
        addProduct(product);
        product = new Product { ID = arrayOfRandomNumbersForProducts[7], Name = "Red shirt", Category = Enums.Category.Shirts, InStock = 61, Price = 35 };
        addProduct(product);
        product = new Product { ID = arrayOfRandomNumbersForProducts[8], Name = "Sha'abeth shoes", Category = Enums.Category.Shoes, InStock = 17, Price = 200 };
        addProduct(product);
        product = new Product { ID = arrayOfRandomNumbersForProducts[9], Name = "Goofy hat", Category = Enums.Category.Hats, InStock = 28, Price = 25.99 };
        addProduct(product);


        /// Orders initializetion
        string[] customerNames = new string[10] { "Yossi", "Chaim", "David", "Ariel", "Yona", "Avishai", "Binyamin", "Noam", "Ori", "Moshe" };
        string[] cities = new string[5] { "Jerusalem", "Tel Aviv", "Haifa", "Beer Sheva", "Petah Tiqwa" };
        string[] streets = new string[10] { "Arlozoroff", "Balfour", "Begin", "Ben Gurion", "Ben Yehuda", "Bialik", "Herzl", "Ibn Gabirol", "Jabotinsky", "Herzl\r\n" };

        DateTime[] orderDates = new DateTime[OrderInit];
        DateTime[] shipDataDates = new DateTime[OrderInit];
        DateTime[] deliveryDataDates = new DateTime[OrderInit];
        for (int i = 0; i < OrderInit; i++)
        {
            TimeSpan duration = new TimeSpan(-rnd.Next(50, 200), rnd.Next(24), rnd.Next(60), rnd.Next(60));
            orderDates[i] = DateTime.Now.Add(duration);
            duration = new TimeSpan(rnd.Next(20, 45), rnd.Next(24), rnd.Next(60), rnd.Next(60));
            shipDataDates[i] = orderDates[i].Add(duration);
            if (i % 5 == 0 || i % 4 == 0 || i % 3 == 0) // thats equal to 60% from all of the orders
            {
                duration = new TimeSpan(rnd.Next(2, 18), rnd.Next(24), rnd.Next(60), rnd.Next(60));
                shipDataDates[i] = orderDates[i].Add(duration);
            }
        }
        for (int i = 0; i < OrderInit; i++)
        {
            Order order = new Order { ID = Config.getLastIndexOrder, CustomerName = customerNames[rnd.Next(10)], CustomerEmail = customerNames[rnd.Next(10)] + rnd.Next(1000) + "@gmail.com", CustomerAdrress = rnd.Next(100) + " " + streets[rnd.Next(10)] + " Street \t" + rnd.Next(1000000, 9999999) + " " + cities[rnd.Next(5)] + " \t Israel", OrderDate = orderDates[i], ShipDate = shipDataDates[i], DeliveryrDate = deliveryDataDates[i] };
            addOrder(order);
        }

        /// OrderItems initializetion
        for (int i = 0; i < OrderInit; i++)
        {
            /// checking whether there are enught items from the spiecific product
            int productInOrder = rnd.Next(ProductInit);
            int amountOfItems = rnd.Next(1, 4);
            if (i % 4 == 0) /// by this way, i insure that there would be at least 40 items. because 5*4 = 20 + 15*1 = 35. however, in the 15 other loops it's between 1 to 4, so it very likely it would be bigger than 20. 
            {
                amountOfItems = 4;
                do
                {
                    if (products[productInOrder].InStock == 0)
                        productInOrder = rnd.Next(ProductInit);
                } while (products[productInOrder].InStock <= amountOfItems || products[productInOrder].InStock == 0);

            }

            else
            {
                do
                {
                    while (products[productInOrder].InStock <= amountOfItems && products[productInOrder].InStock != 0)
                        amountOfItems = rnd.Next(1, 4);
                    if (products[productInOrder].InStock == 0)
                        productInOrder = rnd.Next(ProductInit);
                } while (products[productInOrder].InStock <= amountOfItems || products[productInOrder].InStock == 0);
            }
            /// now i chose the product and the num of items that will be in this order

            OrderItem orderItem = new OrderItem { OrderItemID = Config.getLastIndexOrderItems, ProductID = products[productInOrder].ID, OrderID = orders[i].ID, Price = products[productInOrder].Price, Amount = amountOfItems };
            products[productInOrder].InStock -= amountOfItems;
            addOrdersItem(orderItem);
        }


    }

    internal class Config
    {
        static internal int FirstIndexOrders = 0;
        static internal int FirstIndexProducts = 0;
        static internal int FirstIndexOrderItems = 0;

        private static int LastIndexOrder = 0;
        private static int LastIndexOrderItems = 0;

        public static int getLastIndexOrder { get => LastIndexOrder++; }
        public static int getLastIndexOrderItems { get => LastIndexOrderItems++; }
    }
}
