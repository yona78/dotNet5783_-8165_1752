using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using Dal;
using DalApi;

namespace BlImplementation;
internal class Cart : ICart
{
    private IDal Dal = new DalList();

    public BO.Cart AddProduct(BO.Cart cart, int idProduct)
    {
        bool productExistInCart = false;
        foreach(var item in cart.Items)
        {
            if (item.ID == idProduct)
            {
                productExistInCart = true;
                break;
            }
        }
        try
        {
            DO.Product productToAdd = Dal.Product.Get(idProduct);
        }
        catch (ExceptionObjectCouldNotBeFound)
        {

            throw;
        }

        throw new NotImplementedException();
    }

    public void MakeOrder(BO.Cart cart, string name, string address, string email)
    {
        throw new NotImplementedException();
    }

    public BO.Cart UpdateAmount(BO.Cart cart, int idProduct, int amount)
    {
        throw new NotImplementedException();
    }
}

