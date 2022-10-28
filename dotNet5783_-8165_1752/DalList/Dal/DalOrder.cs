using DO2;
using static Dal.DataSource;

namespace Dal;

public class DalOrder
{
    public DalOrder() { }
    public int addOrder(Order newOrder)
    {
        if (Config.AmountOrders + 1 < maxOrder)
        {
            orders[Config.AmountOrders] = newOrder;
            Config.AmountOrders++;
        }
        return newOrder.ID;
    }    
}
