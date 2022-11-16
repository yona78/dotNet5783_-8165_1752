﻿using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Order
    {
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime ShipDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public OrderItem Items { get; set; }
        public double TotalPrice { get; set; }
        public override string ToString() => $@"
       Order ID: {ID}
       Customer name: {CustomerName}
       Customer email: {CustomerEmail}
       Customer address: {CustomerAddress}
       Order date: {OrderDate}
       Order status: {Status}
       Payment date: {PaymentDate}
       Ship date: {ShipDate}
       Delivery date: {DeliveryDate}
       Items: {Items}
       Total price: {TotalPrice}
    ";
    }
}
