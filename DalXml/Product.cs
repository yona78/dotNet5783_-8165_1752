using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DalApi;
using DO;

namespace Dal
{
    internal class Product : IProduct
    {
        XElement ProductRoot;
        string FPath = "@Product.xml";
        public Product()
        {
            if (!File.Exists(FPath))
                CreateFiles();
            else
                LoadData();
        }
        private void CreateFiles()
        {
            ProductRoot = new XElement("Products");
            ProductRoot.Save(FPath);
        }

        public int Add(DO.Product toAdd)
        {
            XElement productElement;
            try
            {
                productElement = (from p in ProductRoot.Elements()
                                  where Convert.ToInt32(p.Element("id").Value) == toAdd.ID
                                  select p).FirstOrDefault();
                throw new ExceptionObjectAlreadyExist("product");
            }
            catch
            {
                XElement id = new XElement("id", toAdd.ID);
                XElement name = new XElement("name", toAdd.Name);
                XElement price = new XElement("price", toAdd.Price);
                XElement inStock = new XElement("inStock", toAdd.InStock);
                XElement category = new XElement("catgeory", toAdd.Category);
                XElement product = new XElement("product", id, name, price, inStock, category);
                ProductRoot.Add(product);
                ProductRoot.Save(FPath);
                return toAdd.ID;
            }
        }
        private void LoadData()
        {
            try
            {
                ProductRoot = XElement.Load(FPath);
            }
            catch
            {
                Console.WriteLine("File upload problem");
            }
        }

        public void Delete(int id)
        {
            XElement productElement;
            try
            {
                productElement = (from p in ProductRoot.Elements()
                                  where Convert.ToInt32(p.Element("id").Value) == id
                                  select p).FirstOrDefault();
                productElement.Remove();
                ProductRoot.Save(FPath);
            }
            catch
            {
                throw new ExceptionObjectCouldNotBeFound("product");
            }
        }

        public DO.Product Get(Func<DO.Product?, bool>? func)
        {
            IEnumerable<DO.Product?> products = GetDataOf();
            foreach(var p in products)
            {
                if (func(p))
                    return (DO.Product)p;
            }
            throw new ExceptionObjectCouldNotBeFound("product");
        }

        public DO.Product Get(int id)
        {
            DO.Product prdct;
            try
            {
                prdct = (from p in ProductRoot.Elements()
                           where Convert.ToInt32(p.Element("id").Value) == id
                           select new DO.Product()
                           {
                               ID = Convert.ToInt32(p.Element("id").Value),
                               Name = p.Element("name").Value,
                               Price = Convert.ToInt32(p.Element("price").Value),
                               InStock = Convert.ToInt32(p.Element("inStock").Value),
                               Category = (Enums.Category)Enum.Parse(typeof(Enums.Category), p.Element("category").Value, true)
                           }).FirstOrDefault();
            }
            catch
            {
                throw new ExceptionObjectCouldNotBeFound("product");
            }
            return prdct;

        }

        public IEnumerable<DO.Product?> GetDataOf(Func<DO.Product?, bool>? predict = null)
        {
            IEnumerable<DO.Product?> products;
            try
            {
                products = ((IEnumerable<DO.Product?>)(from p in ProductRoot.Elements()
                            select new DO.Product()
                            {
                                ID = Convert.ToInt32(p.Element("id").Value),
                                Name = p.Element("name").Value,
                                Price = Convert.ToInt32(p.Element("price").Value),
                                InStock = Convert.ToInt32(p.Element("inStock").Value),
                                Category = (Enums.Category)Enum.Parse(typeof(Enums.Category),p.Element("category").Value,true)

                            }));
            }
            catch
            {
                products = null;
            }
            IEnumerable<DO.Product?> data = products.Where(x => predict(x));
            return data;

        }

        public void Update(DO.Product toUpdate)
        {
            try
            {
                XElement productElement = (from p in ProductRoot.Elements()
                                           where Convert.ToInt32(p.Element("id").Value) == toUpdate.ID
                                           select p).FirstOrDefault();
                productElement.Element("name").Value = toUpdate.Name;
                productElement.Element("price").Value = toUpdate.Price.ToString();
                productElement.Element("inStock").Value = toUpdate.InStock.ToString();
                productElement.Element("category").Value = toUpdate.Category.ToString();
                ProductRoot.Save(FPath);
            }
            catch
            {
                throw new ExceptionObjectCouldNotBeFound("product");
            }
        }
    }
}
