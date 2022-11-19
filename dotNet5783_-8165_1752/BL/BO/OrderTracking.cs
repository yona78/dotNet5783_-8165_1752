using DO;

namespace BO;
public class OrderTracking // logic object for the manager to see the progras of orders.
{
    public int ID { set; get; } // id of the order
    public Enums.Status OrderStatus { set; get; } // status of order
    public List<(DateTime, Enums.Status)> status { set; get; } // what is the date now and  what is his current status.

    public override string ToString() => $@"
       Order ID: {ID}
       Status: {OrderStatus}"; // to string.


}