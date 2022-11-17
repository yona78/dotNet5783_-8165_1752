﻿using DO;

namespace BO;
public class Order // object of the manager, on a order a client had asked for
{
    public int ID { set; get; } // id of order
    public string CustomerName { set; get; } // customer name
    public string CustomerEmail { set; get; } // customer email
    public string CustomerAddress { set; get; } // customer address
    public Enums.Status OrderStatus { set; get; } // status of order
    public DateTime PaymentDate { set; get; } // day of payment on this order
    public DateTime ShipDate { set; get; } // when did you send the order
    public DateTime DeliveryrDate { set; get; } // when did the order actually arrived
    public List<OrderItem> Items { set; get; } // items in order
    public double TotelPrice { set; get; } // total price of order

    public override string ToString() => $@"
       ID:{ID}
       CustomerName: {CustomerName}
       CustomerEmail: {CustomerEmail}
       CustomerAddress: {CustomerAddress}
       Status: {OrderStatus}
       PaymentDate: {PaymentDate}
       ShipDate {ShipDate}
       DeliveryrDate: {DeliveryrDate}
       Items: {Items}
       TotelPrice: {TotelPrice}
    "; // to string.

}
