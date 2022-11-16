using DO;

namespace BO;
public class OrderTracking
{
    public int ID { set; get; }
    public Enums.Status OrderStatus { set; get; }

    public List<(DateTime, Enums.Status)> status { set; get; }

    public override string ToString() => $@"
       Order ID: {ID}
       Status: {OrderStatus}";
     

}