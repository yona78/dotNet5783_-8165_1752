using DO;
namespace Dal;
internal static class DataSource
{


    static readonly Random rnd = new Random();
    static internal DalOrder[] orders = new DalOrder[100];
    static int maxOrder = 100;
    static internal DalProduct[] products = new DalProduct[50];
    static int maxProducts = 50;
    static internal DalOrderItem[] OrderItems = new DalOrderItem[200];
    static int maxOrderItems = 200;

    static DataSource()
    {
        s_Initialize();
    }

    private static void addOrder(DalOrder newOrder)
    {
        if(Config.AmountOrders+ 1< maxOrder)
        {
            orders[Config.AmountOrders] = newOrder;
            Config.AmountOrders++;
        }
    }

    private static void addProduct(DalProduct newProduct)
    {
        if (Config.AmountProducts + 1 < maxProducts)
        {
            products[Config.AmountProducts] = newProduct;
            Config.AmountProducts++;
        }
    }

    private static void addOrdersItem(DalOrderItem newOrderItem)
    {
        if (Config.AmountOrderItems + 1 < maxOrderItems)
        {
            OrderItems[Config.AmountOrderItems] = newOrderItem;
            Config.AmountOrderItems++;
        }
    }

    static void s_Initialize()
    {
        /// constant for loop limit - right programing rules 
        const int ProductInit = 10;
        const int OrderInit = 20;
        const int OrderItemslInit = 40;
        for (int i = 0; i < ProductInit; i++)
            DalProduct X = new DalProduct;
            {
                Id = RandomGen.Next(100000, 999999),
            };
    }

    internal class Config
    {

        static internal int AmountOrders = 0;
        static internal int AmountOrderItems = 0;
        static internal int AmountProducts = 0;
        static internal int IdCreations = 0;

        
        s_Initialize();
    }
}
