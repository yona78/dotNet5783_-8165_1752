using DalApi;
using DL;
using DO;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using static DO.Enums;

namespace Dal;
internal class Product : IProduct
{
    
    string FPath = "Product.xml";

    public int Add(DO.Product toAdd)
    {
        XElement ProductRoot = XMLTools.LoadListFromXMLElement(FPath);
        XElement productElement;
            productElement = (from p in ProductRoot.Elements()
                              where Convert.ToInt32(p.Element("ID").Value) == toAdd.ID
                              select p).FirstOrDefault();
        if(productElement != null)
        {
                throw new ExceptionObjectAlreadyExist("product");
        }
            
        XElement ID = new XElement("ID", toAdd.ID.ToString());
        XElement Name = new XElement("Name", toAdd.Name);
        XElement Price = new XElement("Price", toAdd.Price.ToString());
        XElement Category = new XElement("Category", toAdd.Category.ToString());
        XElement InStock = new XElement("InStock", toAdd.InStock.ToString());
        XElement product = new XElement("Product", ID, Name, Price,  Category,InStock);
        ProductRoot.Add(product);
        XMLTools.SaveListToXMLElement(ProductRoot, FPath);
        //ProductRoot.Save(FPath);
        return toAdd.ID;
    }

    public void Delete(int id)
    {
        XElement ProductRoot = XMLTools.LoadListFromXMLElement(FPath);
        XElement productElement= (from p in ProductRoot.Elements()
                                  where Convert.ToInt32(p.Element("ID").Value) == id
                                  select p).FirstOrDefault();
        if(productElement!=null)
        {
            productElement.Remove();
            XMLTools.SaveListToXMLElement(ProductRoot, FPath);
        }
        else
        {
            throw new ExceptionObjectCouldNotBeFound("product");
        }
    }

    public DO.Product Get(Func<DO.Product?, bool>? func)
    {
        IEnumerable<DO.Product?> lst = GetDataOf();
        foreach (var item in lst)
        {
            if ((func ?? (x => false))(item))
                return (item ?? new DO.Product()); // if item is null, i will return a default value
        }
        throw new ExceptionObjectCouldNotBeFound("Product"); // else, if i couldn't have found this order, i will throw an exception
    }

    public DO.Product Get(int id)
    {
        return Get(product => (product ?? new DO.Product()).ID == id);

    }

    public IEnumerable<DO.Product?> GetDataOf(Func<DO.Product?, bool>? predict = null)
    {
        XElement ProductRoot = XMLTools.LoadListFromXMLElement(FPath);
        return (from p in ProductRoot.Elements()
                select (DO.Product?)new DO.Product()
                {
                    ID = Convert.ToInt32(p.Element("ID").Value),
                    Name = p.Element("Name").Value,
                    Price = Convert.ToDouble(p.Element("Price").Value),
                    InStock = Convert.ToInt32(p.Element("InStock").Value),
                    Category = (Enums.Category)Enum.Parse(typeof(Enums.Category), p.Element("Category").Value, true)

                }).Where(product => predict is null ? true : predict(product));

    }

    public void Update(DO.Product toUpdate)
    {
        XElement ProductRoot = XMLTools.LoadListFromXMLElement(FPath);
        XElement productElement = (from p in ProductRoot.Elements()
                                   where Convert.ToInt32(p.Element("ID").Value) == toUpdate.ID
                                   select p).FirstOrDefault();
        if (productElement != null)
        {
            productElement.Element("ID").Value = toUpdate.ID.ToString();
            productElement.Element("Name").Value = toUpdate.Name;
            productElement.Element("Price").Value = toUpdate.Price.ToString();
            productElement.Element("InStock").Value = toUpdate.InStock.ToString();
            productElement.Element("Category").Value = toUpdate.Category.ToString();
            XMLTools.SaveListToXMLElement(ProductRoot, FPath);
        }
        else
        {
            throw new ExceptionObjectCouldNotBeFound("product");
        }
    }
}

