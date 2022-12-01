using DO;
using DalApi;
namespace Dal;
/// <summary>
/// public class for implemention of orderItem
/// </summary>
internal class DalOrderItem : IOrderItem
{
    public DalOrderItem() { }  
    public int Add(OrderItem newOrderItem) // func that adds an orderItem to the array of orderItems, and return its id
    {
        if (DataSource._orderItems.Count == DataSource.maxOrderItems) // checks if the arr is full
            throw new ExceptionListIsFull();
        bool found = false;
        for (int i = 0; i < DataSource._orders.Count(); i++) // looks if  there is such an order
        {
            if ((DataSource._orders[i]).ID == newOrderItem.OrderID)
            {
                found = true;
                break;
            }
        }
        if (!found)
            throw new ExceptionObjectCouldNotBeFound("order");
        double price = 0;
        found = false;
        for (int i = 0; i < DataSource._products.Count(); i++)// looks if  there is such an product
        {
            if ((DataSource._products[i]).ID == newOrderItem.ProductID)
            {
                price = (DataSource._products[i]).Price;
                found = true;
                break;
            }
        }
        if (!found)
            throw new ExceptionObjectCouldNotBeFound("product");
        newOrderItem.Price = price;
        newOrderItem.OrderItemID = DataSource.Config.GetLastIndexOrderItems;
        for (int i = 0; i < DataSource._orderItems.Count(); i++)
        {
            if ((DataSource._orderItems[i]).OrderID == newOrderItem.OrderID && (DataSource._orderItems[i]).ProductID == newOrderItem.ProductID) // because we can't add a new orderItem to the same product and product id, if there is already one there. 
                throw new ExceptionObjectAlreadyExist("orderItem");
        }
        DataSource._orderItems.Add(newOrderItem);
        return newOrderItem.OrderItemID; // return the id of the orderItem we added
    }
    public OrderItem Get(int id) // func that return orderItem by its id 
    {
        return Get(orderItem => (orderItem ?? new OrderItem()).OrderItemID == id);

        /*for (int i = 0; i < DataSource._orderItems.Count(); i++) // returns an orderItem by its id
        {
            if ((DataSource._orderItems[i] ?? new OrderItem()).OrderItemID == id)
                return (DataSource._orderItems[i] ?? new OrderItem());
        }
        throw new ExceptionObjectCouldNotBeFound("orderItem");*/
    }
    public IEnumerable<OrderItem> GetDataOf(Func<OrderItem?, bool>? predict = null) // func that returns all of the orderItems
    {
        if (predict == null)
            return DataSource._orderItems;
        IEnumerable<OrderItem> data = DataSource._orderItems.Where(x => predict(x));
        return data;
    }
    public void Delete(int idOrderItem) // func that deletes orderItem from the array
    {
        bool found = false;
        for (int i = 0; i < DataSource._orderItems.Count(); i++) // checks if the order item exists
        {
            if ((DataSource._orderItems[i]).OrderItemID == idOrderItem)
            {
                found = true;
                DataSource._orderItems.RemoveAt(i);
            }
        }
        if (!found)
            throw new ExceptionObjectCouldNotBeFound("orderItem");
    }
    public void Update(OrderItem newOrderItem) // func that updates an orderItem in his array
    {
        bool found = false;
        for (int i = 0; i < DataSource._orderItems.Count(); i++) // checks if the order exists
        {
            if ((DataSource._orders[i] ?? new Order()).ID == newOrderItem.OrderID)
            {
                found = true;
                break;
            }
        }
        if (!found)
            throw new ExceptionObjectCouldNotBeFound("order");

        found = false;
        for (int i = 0; i < DataSource._orderItems.Count(); i++) // checks if the product exists
        {
            if ((DataSource._products[i] ?? new Product()).ID == newOrderItem.ProductID)
            {
                found = true;
                break;
            }
        }
        if (!found)
            throw new ExceptionObjectCouldNotBeFound("product");

        found = false;
        for(int i =0; i< DataSource._products.Count();i++)
        {
            if (newOrderItem.ProductID == (DataSource._products[i] ?? new Product()).ID)
            {
                newOrderItem.Price = (DataSource._products[i] ?? new Product()).Price;
            }
        }
        for (int i = 0; i < DataSource._orderItems.Count(); i++)
        {
            if ((DataSource._orderItems[i] ?? new OrderItem()).OrderItemID == newOrderItem.OrderItemID) // if it has the same id, we do a deep copy
            {
                found = true;
                DataSource._orderItems.RemoveAt(i);
                DataSource._orderItems.Insert(i, newOrderItem);
                break;
            }
        }
        if (!found) // otherwise, the order itemcouldn't be found.
            throw new ExceptionObjectCouldNotBeFound("orderItem");
    }

    // The special functions we were asked to add
    public OrderItem GetOrderItem(int idOrder, int idProduct) // func that reutrns orderItem by its order and product ids
    {
        return Get(orderItem => ((orderItem ?? new OrderItem()).OrderID == idOrder)&&((orderItem ?? new OrderItem()).ProductID == idProduct));

        /*for (int i = 0; i < DataSource._orderItems.Count(); i++) // returns an orderItem by its product id and its order id
        {
            if ((DataSource._orderItems[i] ?? new OrderItem()).OrderID == idOrder && (DataSource._orderItems[i] ?? new OrderItem()).ProductID == idProduct)
                return (DataSource._orderItems[i] ?? new OrderItem());
        }
        throw new ExceptionObjectCouldNotBeFound("order");*/
    }
    public IEnumerable<OrderItem?> GetDataOfOrderItem(int idOfOrder) // func that returns all the orderItems from the specific order
    {
        List<OrderItem?> ret = new List<OrderItem?>(); // we use list because we don't know the what is the size of the structre we will need to use.
        for (int i = 0; i < DataSource._orderItems.Count(); i++) // returns a list of all of the orderItems from the specific order, whose id was given to us.
        {
            if ((DataSource._orderItems[i] ?? new OrderItem()).OrderID == idOfOrder)
            {
                ret.Add((DataSource._orderItems[i] ?? new OrderItem()));
            }
        }
        return ret;
    }

    public OrderItem Get(Func<OrderItem?, bool>? func) // func that returns an orderItem by a term it gets.
    {
        foreach (var item in DataSource._orderItems)
        {
            if (func(item))
                return (item ?? new OrderItem()); // if item is null, i will return a default value
        }
        throw new ExceptionObjectCouldNotBeFound("orderItem"); // else, if i couldn't have found this orderItem, i will throw an exception
    }
}