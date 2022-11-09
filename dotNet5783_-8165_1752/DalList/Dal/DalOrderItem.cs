using DO;
namespace Dal;
/// <summary>
/// public class for implemention of orderItem
/// </summary>
public class DalOrderItem
{
    public DalOrderItem() { }

    public int AddOrderItem(OrderItem newOrderItem) // func that adds an orderItem to the array of orderItems, and return its id
    {
        if (DataSource.Config.firstIndexOrderItems == DataSource.maxOrderItems) // checks if the arr is full
            throw new Exception("array is full");
        bool found = false;
        for (int i = 0; i < DataSource.maxOrders; i++) // looks if  there is such an order
        {
            if (DataSource._orders[i].ID == newOrderItem.OrderID)
            {
                found = true;
                break;
            }
        }
        if (!found)
        {
            throw new Exception("order doesn't exist");
        }
        found = false;
        for (int i = 0; i < DataSource.maxProducts; i++)// looks if  there is such an product
        {
            if (DataSource._products[i].ID == newOrderItem.ProductID)
            {
                found = true;
                break;
            }
        }
        if (!found)
        {
            throw new Exception("product doesn't exist");
        }
        newOrderItem.OrderItemID = DataSource.Config.GetLastIndexOrderItems;
        for (int i = 0; i < DataSource.maxOrderItems; i++)
        {
            if (DataSource._orderItems[i].OrderID == newOrderItem.OrderID && DataSource._orderItems[i].ProductID == newOrderItem.ProductID) // because we can't add a new orderItem to the same product and product id, if there is already one there. 
                throw new Exception("orderItem already exist");
        }
        DataSource._orderItems[DataSource.Config.firstIndexOrderItems] = newOrderItem;
        int newFirstIndexOrderItems = DataSource.maxOrderItems; // updates the new first index orderItems.
        for (int i = DataSource.Config.firstIndexOrderItems; i < DataSource.maxOrderItems; i++)
        {
            if (DataSource._orderItems[i].OrderItemID == 0)
            {
                newFirstIndexOrderItems = i;
                break;
            }
        }
        DataSource.Config.firstIndexOrderItems = newFirstIndexOrderItems;
        return newOrderItem.OrderItemID; // return the id of the orderItem we added
    }
    public OrderItem GetOrderItem(int idOrder, int idProduct) // func that reutrns orderItem by its order and product ids
    {
        for (int i = 0; i < DataSource.maxOrderItems; i++) // returns an orderItem by its product id and its order id
        {
            if (DataSource._orderItems[i].OrderID == idOrder && DataSource._orderItems[i].ProductID == idProduct)
                return DataSource._orderItems[i];
        }
        throw new Exception("order couldn't be found");
    }
    public OrderItem[] GetDataOfOrderItem() // func that returns all of the orderItems
    {
        return DataSource._orderItems;
    } 
    public void DeleteOrderItem(int idOrderItem) // func that deletes orderItem from the array
    {
        bool found = false;
        for (int i = 0; i < DataSource.maxOrderItems; i++) // checks if the order item exists
        {
            if (DataSource._orderItems[i].OrderItemID == idOrderItem)
            {
                found = true;
                DataSource._orderItems[i] = new OrderItem();
                DataSource.Config.firstIndexOrderItems = i;
            }
        }
        if (!found)
        {
            throw new Exception("orderItem couldn't be found");
        }
    }
    public void UpdateOrderItem(OrderItem newOrderItem) // func that updates an orderItem in his array
    {
        bool found = false;
        for (int i = 0; i < DataSource.maxOrders; i++) // checks if the order exists
        {
            if (DataSource._orders[i].ID == newOrderItem.OrderID)
            {
                found = true;
                break;
            }
        }
        if (!found)
        {
            throw new Exception("order doesn't exist");
        }
        found = false;
        for (int i = 0; i < DataSource.maxProducts; i++) // checks if the product exists
        {
            if (DataSource._products[i].ID == newOrderItem.ProductID)
            {
                found = true;
                break;
            }
        }
        if (!found)
        {
            throw new Exception("product doesn't exist");
        }
        found = false;
        for (int i = 0; i < DataSource.maxOrderItems; i++)
        {
            if (DataSource._orderItems[i].OrderItemID == newOrderItem.OrderItemID) // if it has the same id, we do a deep copy
            {
                found = true;
                DataSource._orderItems[i].OrderID = newOrderItem.OrderID;
                DataSource._orderItems[i].ProductID = newOrderItem.ProductID;
                DataSource._orderItems[i].Amount = newOrderItem.Amount;
                DataSource._orderItems[i].Price = newOrderItem.Price;
                break;
            }
        }
        if (!found) // otherwise, the order item couldn't be found.
        {
            throw new Exception("orderItem couldn't be found");
        }
    }

    // The special functions we were asked to add
    public OrderItem GetOrderItem(int id) // func that return orderItem by its id 
    {
        for (int i = 0; i < DataSource.maxOrderItems; i++) // returns an orderItem by its id
        {
            if (DataSource._orderItems[i].OrderItemID == id)
                return DataSource._orderItems[i];
        }
        throw new Exception("orderItem couldn't be found");
    }
    public List<OrderItem> GetDataOfOrderItem(int idOfOrder) // func that returns all the orderItems from the specific order
    {
        List<OrderItem> ret = new List<OrderItem>(); // we use list because we don't know the what is the size of the structre we will need to use.
        for (int i = 0; i < DataSource.maxOrderItems; i++) // returns a list of all of the orderItems from the specific order, whose id was given to us.
        {
            if (DataSource._orderItems[i].OrderID == idOfOrder)
            {
                ret.Add(DataSource._orderItems[i]);
            }
        }
        return ret;
    }
}
