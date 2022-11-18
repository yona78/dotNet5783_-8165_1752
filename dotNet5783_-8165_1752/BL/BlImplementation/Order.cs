using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using Dal;
using DalApi;

namespace BlImplementation;
internal class Order : BlApi.IOrder
{
    private IDal Dal = new DalList();

    public List<OrderForList> GetOrderList()
    {
        throw new NotImplementedException();
    }

    public BO.Order GetOrderManager(int idOrder)
    {
        throw new NotImplementedException();
    }

    public OrderTracking TrackOrder(int idOrder)
    {
        throw new NotImplementedException();
    }

    public void Update(int idOrder)
    {
        throw new NotImplementedException();
    }

    public BO.Order UpdateAmount(int idOrder)
    {
        throw new NotImplementedException();
    }

    public BO.Order UpdateDelivery(int idOrder)
    {
        throw new NotImplementedException();
    }
}

