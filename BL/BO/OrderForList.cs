namespace BO;
/// <summary>
/// help class to use in our project to print data about orders
/// </summary>
public class OrderForList // logic object for dealing with the orders. we doens't care about the items, we only want to know about the order
{
    public int ID { set; get; } // id of order
    public string? CustomerName { set; get; } // customer name
    public Enums.Status? OrderStatus { set; get; } // status of order
    public int AmountOfItems { set; get; } // amount of items in the order
    public double TotelPrice { set; get; } // total price of order

    /// <summary>
    /// override to string function to orderforlist
    /// </summary>
    /// <returns>
    /// string with all the information about the orderfor list
    /// </returns>
    public override string ToString() => $@"
       ID:{ID}
       CustomerName: {CustomerName}
       Status: {OrderStatus}
       Amount of Items: {AmountOfItems}
       TotalPrice: {TotelPrice}
    "; // to string.

}
