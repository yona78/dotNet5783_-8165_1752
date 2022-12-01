using DO;

namespace BO;
/// <summary>
/// help class of object to use for getting all the data about order status
/// </summary>
public class OrderTracking // logic object for the manager to see the progras of orders.
{
    public int ID { set; get; } // id of the order
    public Enums.Status? OrderStatus { set; get; } // status of order
    public List<(DateTime?, Enums.Status)?>? status { set; get; } // what is the date now and  what is his current status.
    /// <summary>
    /// override to string function to orderTracking
    /// </summary>
    /// <returns>
    /// string with all the information about the orderTracking
    /// </returns>
    public override string ToString()
    {
        string sum = "";
        foreach ((DateTime, Enums.Status) tmp in status)
        {
            sum += tmp;
            sum += "\n";
        }
        return $@"
       Order ID: {ID}
       Status: {OrderStatus}
       Status : {sum}
        "; // to string.
    }
}