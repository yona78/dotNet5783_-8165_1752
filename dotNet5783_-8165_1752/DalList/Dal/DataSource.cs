using DO;
namespace Dal;
/// <summary>
/// static class for having the real dataSource we are going to work with 
/// </summary>
internal static class DataSource
{

    static readonly Random _rnd = new Random(); // neccesery initlizetion for the initilizetion of the data structures. 
    static internal List<Order>? _orders = new List<Order>();
    public static int maxOrders = 100; // we will use it in the functions of each class
    static internal List<Product>? _products = new List<Product>();
    public static int maxProducts = 50; // we will use it in the functions of each class
    static internal List<OrderItem>? _orderItems = new List<OrderItem>();
    public static int maxOrderItems = 200; // we will use it in the functions of each class



    static DataSource() // the constructor calls this func.
    {
        s_Initialize();
    }

    private static void addOrder(Order newOrder) // func that gets new order and add it to the array of orders
    {
        _orders.Add(newOrder);  // this func promotes the first index of empty place in the array of the orders by one, and adds the new order 
    }

    private static void addProduct(Product newProduct) // func that gets new product and add it to the array of products
    {
        _products.Add(newProduct);  // this func promotes the first index of empty place in the array of the products by one, and adds the new product
    }

    private static void addOrdersItem(OrderItem newOrderItem) // func that gets new orderItem and add it to the array of orderItems
    {
        _orderItems.Add(newOrderItem);  // this func promotes the first index of empty place in the array of the orderItems by one, and adds the new orderItem
    }

    static void s_Initialize() // func that initialize the first data
    {
        /// constant for loop limit - right programing rules 
        const int productInit = 10;
        const int orderInit = 20;

        /// Products initializetion
        int[] arrayOfRandomNumbersForProducts = new int[productInit];
        /// the next code will check whether there are duplicate IDs, it also generates random valuse to each product id
        bool checkForDuplicateId = false;
        do
        {
            checkForDuplicateId = false;
            for (int i = 0; i < productInit; i++) // generates random values to each product
            {
                arrayOfRandomNumbersForProducts[i] = _rnd.Next(100000, 999999);
            }
            for (int i = 0; i < productInit; i++) // for each product in the array
            {
                int currentId = arrayOfRandomNumbersForProducts[i]; // saving the curID
                for (int j = i + 1; j < productInit; j++) // for each product we didn't check yet
                {
                    if (arrayOfRandomNumbersForProducts[j] == currentId) // it means there is different product with the same id.
                    {
                        checkForDuplicateId = true;
                    }
                }
            }
        } while (checkForDuplicateId);
        /// there must be one product with empty amount (InStock=0)
        Product product;
        // initilzetion with random data
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
        string[] streets = new string[10] { "Arlozoroff", "Balfour", "Begin", "Ben Gurion", "Ben Yehuda", "Bialik", "Herzl", "Ibn Gabirol", "Jabotinsky", "HaTe'ena" };

        DateTime[] orderDates = new DateTime[orderInit];
        DateTime[] shipDateDates = new DateTime[orderInit];
        DateTime[] deliveryDateDates = new DateTime[orderInit];
        for (int i = 0; i < orderInit; i++)
        {
            TimeSpan duration = new TimeSpan(-_rnd.Next(50, 200), _rnd.Next(24), _rnd.Next(60), _rnd.Next(60)); // taking random duration between 50 to 200 days from now
            orderDates[i] = DateTime.Now.Add(duration); // the day of the add of the order, it's from now to 50-200 day ago
            duration = new TimeSpan(_rnd.Next(20, 100), _rnd.Next(24), _rnd.Next(60), _rnd.Next(60)); // taking random duration between 20 to 45 days from now
            shipDateDates[i] = orderDates[i].Add(duration); // the day of the ship of the order, it's from now to 20-45 day ago
            if (i % 5 == 0 || i % 4 == 0 || i % 3 == 0) // thats equal to 60% from all of the orders
            {
                duration = new TimeSpan(_rnd.Next(4, 25), _rnd.Next(24), _rnd.Next(60), _rnd.Next(60)); // taking random duration between 2 to 18 days from now
                deliveryDateDates[i] = shipDateDates[i].Add(duration); // the day of the delivery of the order, it's from now to 2-18 day ago
            }
        }
        for (int i = 0; i < orderInit; i++)
        {
            Order order = new Order { ID = Config.GetLastIndexOrder, CustomerName = customerNames[_rnd.Next(10)], CustomerEmail = customerNames[_rnd.Next(10)] + _rnd.Next(1000) + "@gmail.com", CustomerAdrress = _rnd.Next(100) + " " + streets[_rnd.Next(10)] + " Street \t" + _rnd.Next(1000000, 9999999) + " " + cities[_rnd.Next(5)] + "\t Israel", OrderDate = orderDates[i], ShipDate = shipDateDates[i], DeliveryDate = deliveryDateDates[i] }; // we use a clean format
            addOrder(order);
        }

        /// OrderItems initializetion
        for (int i = 0; i < orderInit; i++)
        {
            /// checking whether there are enught items from the spiecific product
            int productInOrder = _rnd.Next(productInit);
            int amountOfItems = _rnd.Next(1, 4);
            if (i % 4 == 0) /// by this way, i insure that there would be at least 40 items. because 5*4 = 20 + 15*1 = 35. however, in the 15 other loops it's between 1 to 4, so it very likely it would be bigger than 20. 
            {
                amountOfItems = 4;
                do
                {
                    if ((_products[productInOrder]).InStock == 0)
                        productInOrder = _rnd.Next(productInit);
                } while ((_products[productInOrder]).InStock <= amountOfItems || (_products[productInOrder]).InStock == 0); // i can't add a number of amount itmes if there is no such num of items in the store...

            }

            else // i can choose random product, and random amount of orders, as long it is possible
            {
                do
                {
                    while ((_products[productInOrder]).InStock <= amountOfItems && (_products[productInOrder]).InStock != 0)
                        amountOfItems = _rnd.Next(1, 4);
                    if ((_products[productInOrder]).InStock == 0)
                        productInOrder = _rnd.Next(productInit);
                } while ((_products[productInOrder]).InStock <= amountOfItems || (_products[productInOrder]).InStock == 0);
            }
            /// now i chose the product and the num of items that will be in this order

            OrderItem orderItem = new OrderItem { OrderItemID = Config.GetLastIndexOrderItems, ProductID = (_products[productInOrder]).ID, OrderID = (_orders[i]??new Order()).ID, Price = (_products[productInOrder]).Price, Amount = amountOfItems };
            //int sum = _products[productInOrder].InStock - amountOfItems; // if someone orders 4 items from specific product, i will save this items for him. 
            addOrdersItem(orderItem);
        }


    }

    internal class Config // internal class for configuration staff
    {
        private static int lastIndexOrder = 0; // the last index in the array of orders who isn't empty
        private static int lastIndexOrderItems = 0;// the last index in the array of orderItems who isn't empty
        public static int GetLastIndexOrder { get => lastIndexOrder++; } // simple get attribute to lastIndexOrder
        public static int GetLastIndexOrderItems { get => lastIndexOrderItems++; } // simple get attribute to lastIndexOrderItems
    }
}