﻿using BlApi;
using BO;
using Dal;
using DalApi;
using DO;
using System.IO.Pipes;

namespace BlImplementation;
internal class Cart : ICart // cart of customer
{
    private IDal Dal = new DalList(); // a way to communicate with dBase level

    public BO.Cart AddProduct(BO.Cart cart, int idProduct) // add a product to the cart of the customer. The customer will use this func
    {
        bool productExistInCart = false;
        BO.OrderItem itemToChange = new BO.OrderItem();
        DO.Product product = new DO.Product();

        foreach (var item in cart.Items)
        {
            if (item.ProductID == idProduct)
            {
                productExistInCart = true;
                itemToChange = item;
                break;
            }
        }
        if (!productExistInCart) // the product isn't found in the cart
        {
            try
            {
                product = Dal.Product.Get(idProduct);
            }
            catch (ExceptionObjectCouldNotBeFound inner) // the product isn't exist in the dBase
            {
                throw new ExceptionLogicObjectCouldNotBeFound("product", inner);
            }
            if (product.InStock <= 0) // there isn't enugh in stock
                throw new ExceptionObjectIsNotAviliable("product");

            BO.OrderItem item = new BO.OrderItem();
            item.Name = product.Name;
            item.Price = product.Price;
            item.TotalPrice = product.Price;
            cart.TotelPrice += product.Price;
            cart.Items.Add(item);


        }
        else // the product is already found in the cart
        {
            if (product.InStock <= 0) // there isn't enough in stock
                throw new ExceptionObjectIsNotAviliable("product");
            itemToChange.Amount += 1;
            itemToChange.TotalPrice += product.Price;
            cart.TotelPrice += product.Price;
        }

        return cart;
    }

    public void MakeOrder(BO.Cart cart, string name, string address, string email) // func that take the customer cart and make it into a real order in the dBase
    {
        // checks if all the data in the cart is valid
        DO.Product product = new DO.Product();
        foreach (var item in cart.Items) // checks if all the items are realy exist, if the amounts are positive
        {
            try
            {
                product = Dal.Product.Get(item.ProductID);
            }
            catch (ExceptionObjectCouldNotBeFound inner) // the product isn't exist in the dBase
            {
                throw new ExceptionLogicObjectCouldNotBeFound("product", inner);
            }
            if (item.Amount < 0) // amount negative
                throw new ExceptionDataIsInvalid("orderItem");
            if (product.InStock < item.Amount) // not enough in dBase
                throw new ExceptionNotEnoughInDataBase("orderItem");
        }
        if (name == "" || address == "" || email == "") // checks if the string are valids. ### TO ADD - that email and address will be in a specific format.
            throw new ExceptionDataIsInvalid("cart");
        DO.Order order = new DO.Order();
        order.OrderDate = DateTime.Now; // initalize the orderDate to be now.
        int id;
        try
        {
            id = Dal.Order.Add(order);
        }
        catch (ExceptionObjectAlreadyExist inner)
        {
            throw new ExceptionLogicObjectAlreadyExist("orderItem", inner);
        }
        DO.OrderItem orderItem = new DO.OrderItem();
        foreach (var item in cart.Items)
        {
            orderItem.OrderID = id;
            orderItem.Price = item.Price;
            orderItem.ProductID = item.ProductID;
            orderItem.Amount = item.Amount;
            try
            {
                Dal.OrderItem.Add(orderItem);
            }
            catch (ExceptionObjectCouldNotBeFound inner)
            {
                throw new ExceptionLogicObjectCouldNotBeFound("orderItem", inner);
            }
            catch (ExceptionObjectAlreadyExist inner)
            {
                throw new ExceptionLogicObjectAlreadyExist("orderItem", inner);
            }
            try
            {
                product = Dal.Product.Get(orderItem.ProductID);
            }
            catch (ExceptionObjectCouldNotBeFound inner)
            {
                throw new ExceptionLogicObjectCouldNotBeFound("product", inner);
            }
            product.InStock -= orderItem.Amount;
            try
            {
                Dal.Product.Update(product);
            }
            catch (ExceptionObjectCouldNotBeFound inner)
            {
                throw new ExceptionLogicObjectCouldNotBeFound("product", inner);
            }

        }
    }

    public BO.Cart UpdateAmount(BO.Cart cart, int idProduct, int amount) // func that updates the amount of a product in the cart
    {
        bool productExistInCart = false;
        BO.OrderItem itemToChange = new BO.OrderItem();
        DO.Product product = new DO.Product();

        foreach (var item in cart.Items)
        {
            if (item.ProductID == idProduct) // checks if the specific product is found in the cart
            {
                productExistInCart = true;
                itemToChange = item;
                break;
            }
        }
        if (!productExistInCart) // the product isn't exist in the cart
        {
            throw new ExceptionLogicObjectCouldNotBeFound("product");

        }
        else // the product is exist in the cart
        {
            if (product.InStock <= 0) // there isn't enough in stock
                throw new ExceptionObjectIsNotAviliable("product");
            if (amount == 0)
            {
                cart.TotelPrice -= (product.Price) * (itemToChange.Amount); // becuase if the last amount was 3, and now the new amount is 0, it means we add remove three items.
                cart.Items.Remove(itemToChange);
            }
            else if (itemToChange.Amount < amount) // we want to enlarge the amount
            {
                cart.TotelPrice += (product.Price) * (amount - itemToChange.Amount); // becuase if the last amount was 3, and now the new amount is 5, it means we add only two items.
                itemToChange.Amount = amount;
                itemToChange.TotalPrice = (product.Price) * amount;
            }
            else if (itemToChange.Amount > amount) // we want to small the amount
            {
                cart.TotelPrice -= (product.Price) * (itemToChange.Amount - amount); // becuase if the last amount was 5, and now the new amount is 3, it means we remove only two items.
                itemToChange.Amount = amount;
                itemToChange.TotalPrice = (product.Price) * amount;
            }
        }

        return cart;
    }
}

