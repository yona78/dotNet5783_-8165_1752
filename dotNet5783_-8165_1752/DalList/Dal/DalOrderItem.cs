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
        for (int i = 0; i < DataSource._orderItems.Count(); i++) // looks if  there is such an order
        {
            if (DataSource._orders[i].ID == newOrderItem.OrderID)
            {
                found = true;
                break;
            }
        }
        if (!found)
            throw new ExceptionObjectCouldNotBeFound("order");

        found = false;
        for (int i = 0; i < DataSource._orderItems.Count(); i++)// looks if  there is such an product
        {
            if (DataSource._products[i].ID == newOrderItem.ProductID)
            {
                found = true;
                break;
            }
        }
        if (!found)
            throw new ExceptionObjectCouldNotBeFound("product");

        newOrderItem.OrderItemID = DataSource.Config.GetLastIndexOrderItems;
        for (int i = 0; i < DataSource._orderItems.Count(); i++)
        {
            if (DataSource._orderItems[i].OrderID == newOrderItem.OrderID && DataSource._orderItems[i].ProductID == newOrderItem.ProductID) // because we can't add a new orderItem to the same product and product id, if there is already one there. 
                throw new ExceptionObjectAlreadyExist("orderItem");
        }
        DataSource._orderItems.Add(newOrderItem);
        int newFirstIndexOrderItems = DataSource.maxOrderItems; // updates the new first index orderItems.
        return newOrderItem.OrderItemID; // return the id of the orderItem we added
    }
    public OrderItem Get(int id) // func that return orderItem by its id 
    {
        for (int i = 0; i < DataSource._orderItems.Count(); i++) // returns an orderItem by its id
        {
            if (DataSource._orderItems[i].OrderItemID == id)
                return DataSource._orderItems[i];
        }
        throw new ExceptionObjectCouldNotBeFound("orderItem");
    }
    public IEnumerable<OrderItem> GetDataOf() // func that returns all of the orderItems
    {
        return DataSource._orderItems;
    }
    public void Delete(int idOrderItem) // func that deletes orderItem from the array
    {
        bool found = false;
        for (int i = 0; i < DataSource._orderItems.Count(); i++) // checks if the order item exists
        {
            if (DataSource._orderItems[i].OrderItemID == idOrderItem)
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
            if (DataSource._orders[i].ID == newOrderItem.OrderID)
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
            if (DataSource._products[i].ID == newOrderItem.ProductID)
            {
                found = true;
                break;
            }
        }
        if (!found)
            throw new ExceptionObjectCouldNotBeFound("product");

        found = false;
        for (int i = 0; i < DataSource._orderItems.Count(); i++)
        {
            if (DataSource._orderItems[i].OrderItemID == newOrderItem.OrderItemID) // if it has the same id, we do a deep copy
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
        for (int i = 0; i < DataSource._orderItems.Count(); i++) // returns an orderItem by its product id and its order id
        {
            if (DataSource._orderItems[i].OrderID == idOrder && DataSource._orderItems[i].ProductID == idProduct)
                return DataSource._orderItems[i];
        }
        throw new ExceptionObjectCouldNotBeFound("order");
    }
    public IEnumerable<OrderItem> GetDataOfOrderItem(int idOfOrder) // func that returns all the orderItems from the specific order
    {
        List<OrderItem> ret = new List<OrderItem>(); // we use list because we don't know the what is the size of the structre we will need to use.
        for (int i = 0; i < DataSource._orderItems.Count(); i++) // returns a list of all of the orderItems from the specific order, whose id was given to us.
        {
            if (DataSource._orderItems[i].OrderID == idOfOrder)
            {
                ret.Add(DataSource._orderItems[i]);
            }
        }
        return ret;
    }
}