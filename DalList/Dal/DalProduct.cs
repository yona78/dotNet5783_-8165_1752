﻿using DO;
using DalApi;
namespace Dal;
/// <summary>
/// public class for implemention of product 
/// </summary>
internal class DalProduct : IProduct
{
    public DalProduct() { }
    public int Add(Product newProduct) // func that adds an product to the array of products, and return its id
    {
        if (DataSource._products.Count() == DataSource.maxProducts)
            throw new ExceptionListIsFull();
        for (int i = 0; i < DataSource._products.Count(); i++) // checks if the product is already exist
        {
            if ((DataSource._products[i]??new Product()).ID == newProduct.ID)
                throw new ExceptionObjectAlreadyExist("product");
        }
        DataSource._products.Add(newProduct);
        return newProduct.ID;
    }
    public Product Get(int id) // func that reutrns product by its id
    {
        return Get(product => (product ?? new Product()).ID == id);
    }
    public IEnumerable<Product?> GetDataOf(Func<Product?, bool>? predict = null) // func that returns all of the products
    {
        if (predict == null)
            return DataSource._products;
        IEnumerable<Product?> data = DataSource._products.Where(x => predict(x));
        return data;
    }
    public void Delete(int id) // func that deletes product from the array
    {
        bool found = false;
        for (int i = 0; i < DataSource._products.Count(); i++) // looks for the product with the specific id
        {
            if ((DataSource._products[i]??new Product()).ID == id)
            {
                DataSource._products.RemoveAt(i);
                found = true;
            }
        }
        if (!found) // if the product isn't exist throw an exception
            throw new ExceptionObjectCouldNotBeFound("product");
    }
    public void Update(Product newProduct) // func that updates product in his array
    {
        bool found = false;
        for (int i = 0; i < DataSource._products.Count(); i++) // if the specific product is found, it does a deep copy
        {
            if ((DataSource._products[i]?? new Product()).ID == newProduct.ID)
            {
                DataSource._products.RemoveAt(i);
                DataSource._products.Insert(i, newProduct);
                found = true;
                break;
            }
        }
        if (!found) // if the product isn't exist throw an exception
            throw new ExceptionObjectCouldNotBeFound("product");  
    }

    public Product Get(Func<Product?, bool>? func) // func that returns an proudct by a term it gets.
    {
        foreach (var item in DataSource._products)
        {
            if ((func ?? (x => false))(item))
                return (item ?? new Product()); // if item is null, i will return a default value
        }
        throw new ExceptionObjectCouldNotBeFound("product"); // else, if i couldn't have found this product, i will throw an exception
    }
}