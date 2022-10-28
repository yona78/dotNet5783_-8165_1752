using DO;
using static Dal.DataSource;

namespace Dal;

public class DalOrder
{
    public DalOrder() { }
    public int addOrder(Order newOrder)
    {
        
        return newOrder.ID;
    }
    public Order getOrder(int id)
    {
        Order order = null;
        
        return order;
    }
    public string getDataOfOrder()
    {
 
        return "the data of the order";
    }
    public void deleteOrder(int id)
    {

    }
    public void updateOrder(Order newOrder)
    {

    }
    
}
