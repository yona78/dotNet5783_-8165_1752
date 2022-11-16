using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using Dal;
using DalApi;
using DO;

namespace BlImplementation;
internal class Product : BlApi.IProduct // otherwise there is ambigiouty, because he doesn't know whether it's BlApi.IProduct or DalApi.IProduct
{
    private IDal dal = new DalList();
    public void Add(BO.Product product)
    {

        throw new NotImplementedException();
    }

    public void Delete(int idProduct)
    {
        throw new NotImplementedException();
    }

    public ProductItem GetForCustomer(int idCustomer)
    {
        DO.Product product = new DO.Product();
        if (idCustomer >= 0)
        {
            try
            {
                product = dal.Product.Get(idCustomer);
            }
            catch (ExceptionObjectCouldNotBeFound)
            {
                throw new ExceptionLogicObjectCouldNotBeFound("productItem");
            }
        }
        BO.ProductItem item = new BO.ProductItem();
        item.ID = idCustomer;
        item.Price = product.Price;
        item.Name = product.Name;
        item.Category = (BO.Enums.Category)product.Category;
        item.InStock = (product.InStock > 0);
        item.Amount = ;
        return item;
    }

    public BO.Product GetForManager(int idProduct)
    {
        DO.Product product = new DO.Product();
        if(idProduct >= 0)
        {
            try
            {
               product= dal.Product.Get(idProduct);
            }
            catch (ExceptionObjectCouldNotBeFound)
            {
                throw new ExceptionLogicObjectCouldNotBeFound("product");
            }
        }
        BO.Product item = new BO.Product();
        item.Price = product.Price;
        item.Name = product.Name;
        item.Category = (BO.Enums.Category)product.Category;
        return item;
    }

    public List<ProductForList> GetList()
    {
        IEnumerable<DO.Product> listOfProducts = dal.Product.GetDataOf();
        List<ProductForList> list = new List<ProductForList>();
        foreach (DO.Product product in listOfProducts)
        {
            list.Add(new ProductForList(product.ID, product.Name, product.Price, (BO.Enums.Category)product.Category));    
        }
        return list;
    }

    public void Update(BO.Product product)
    {
        throw new NotImplementedException();
    }
}

