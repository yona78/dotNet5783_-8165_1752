﻿using DalApi;
using DL;
using DO;
using System;
using System.Xml.Linq;

namespace Dal
{
    internal class Order : IOrder
    {
        private const string ordersFileName = "orders.xml";
        string FPath_n = @"..\xml\config.xml";
        public int Add(DO.Order toAdd)
        {
            List<DO.Order> orders = XMLTools.LoadListFromXMLSerializer<DO.Order>(ordersFileName);
            XElement root = XElement.Load(FPath_n);
            toAdd.ID = int.Parse(root.Element("lastIndexOrder").Value);
            orders.Add(toAdd);
            XMLTools.SaveListToXMLSerializer<DO.Order>(orders, ordersFileName);
            root.Element("lastIndexOrder").Value = (toAdd.ID+1).ToString();
            root.Save(FPath_n);
            return toAdd.ID;
        }

        public void Delete(int id)
        {
            List<DO.Order> orders = XMLTools.LoadListFromXMLSerializer<DO.Order>(ordersFileName);
            int index = orders.FindIndex(o => o.ID == id);
            if (index != -1)
            {
                orders.RemoveAt(index);
            }
            else
            {
                throw new ExceptionObjectCouldNotBeFound("order");
            }
            XMLTools.SaveListToXMLSerializer<DO.Order>(orders, ordersFileName);
        }

        public DO.Order Get(Func<DO.Order?, bool>? func)
        {
            List<DO.Order?> orders = XMLTools.LoadListFromXMLSerializer<DO.Order?>(ordersFileName);
            foreach (var item in orders)
            {
                if ((func ?? (x => false))(item))
                    return (item ?? new DO.Order()); // if item is null, i will return a default value
            }
            throw new ExceptionObjectCouldNotBeFound("order"); // else, if i couldn't have found this order, i will throw an exception

        }

        public DO.Order Get(int id)
        {
            return Get(order => (order ?? new DO.Order()).ID == id);
        }

        public IEnumerable<DO.Order?> GetDataOf(Func<DO.Order?, bool>? predict = null)
        {
           List<DO.Order?> orders = XMLTools.LoadListFromXMLSerializer<DO.Order?>(ordersFileName);
           if(predict==null)
            {
                return orders;
            }
            IEnumerable<DO.Order?> data = (IEnumerable<DO.Order?>)orders.Where(x => predict(x));
            return data;
        }

        public void Update(DO.Order toUpdate)
        {
            List<DO.Order> orders = XMLTools.LoadListFromXMLSerializer<DO.Order>(ordersFileName);
            int index = orders.FindIndex(o=>o.ID== toUpdate.ID);
            if(index!=-1)
            {
                orders.RemoveAt(index);
                orders.Insert(index, toUpdate);
            }
            else
            {
                throw new ExceptionObjectCouldNotBeFound("order");
            }
            XMLTools.SaveListToXMLSerializer<DO.Order>(orders, ordersFileName);
        }
    }
}
