using DO;
using DalApi;

namespace Dal;
sealed internal class DalList : IDal
{
    private DalList() { }
    public static IDal Instance { get; } = new DalList();
    public IOrder Order { get; } = new Dal.DalOrder();
    public IProduct Product { get; } = new Dal.DalProduct();
    public IOrderItem OrderItem { get; } = new Dal.DalOrderItem();

}
