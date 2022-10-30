﻿

using System.Data;

namespace DO;

public struct Order
{
    public int ID { set; get; }
    public string CustomerName { set; get; }
    public string CustomerEmail { set; get; }
    public string CustomerAdrress { set; get; }
    public DateTime OrderDate { set; get; }
    public DateTime ShipDate { set; get; }
    public DateTime DeliveryrDate { set; get; }
    public override string ToString() => $@"
       Order ID: {ID}
       CustomerName: {CustomerName}
       CustomerEmail: {CustomerEmail}
       CustomerAdress: {CustomerAdrress}
       OrderDate: {OrderDate}
       ShipData: {ShipDate}
       DeliveryrData: {DeliveryrDate}
    ";

}