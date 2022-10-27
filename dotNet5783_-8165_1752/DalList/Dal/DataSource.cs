
namespace Dal;

internal static class DataSource
{
    static Random rnd = new Random();
    static internal DalOrder[] orders = new DalOrder[100];
    int maxOrder = 100;
    static internal DalProduct[] products = new DalProduct[50];
    int maxProducts = 50;
    static internal DalOrderItem[] OrderItems = new DalOrderItem[200];
    int maxOrderItems = 200;

    static DataSource()
    {
        s_Initialize();
    }

    static void addOrder(DalOrder newOrder)
    {
        if(Config.AmountOrders+ 1< maxOrder)
        {
            orders[Config.AmountOrder] = newOrder;
            Config.AmountOrders++;
        }
    }

    static void addProduct(DalProduct newProduct)
    {
        if (Config.AmountProduct + 1 < maxProducts)
        {
            orders[Config.AmountProducts] = newProduct;
            Config.AmountProducts++;
        }
    }

    static void addOrdersItem(DalOrdersItem newOrdersItem)
    {
        if (Config.AmountOrderItem + 1 < maxOrdersItems)
        {
            orders[Config.AmountOrderItem] = newOrdersItem;
            Config.AmountOrderItem++;
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

        static internal int AmountOrder = 0;
        static internal int AmountOrderItem = 0;
        static internal int AmountProduct = 0;
        static internal int IdCreation = 0;

        s_Initialize();
    }
}
