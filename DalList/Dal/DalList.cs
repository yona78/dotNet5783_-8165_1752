using DalApi;

namespace Dal;
sealed internal class DalList : IDal
{
    private static readonly Lazy<IDal> lazy = new Lazy<IDal>(() => new DalList()); // the bonus
    public static IDal Instance { get { return lazy.Value; } } // the bonus

    private DalList() { }
    // public static IDal Instance { get; } = new DalList();
    public IOrder Order { get; } = new Dal.DalOrder();
    public IProduct Product { get; } = new Dal.DalProduct();
    public IOrderItem OrderItem { get; } = new Dal.DalOrderItem();

}
/*
    In a singleton design pattern, the goal is to ensure that there is only one instance of a 
    class and to provide a global access point to that instance. The "lazy initialization" 
    aspect of the singleton means that the instance is only created when it is first needed, 
    rather than being created at the time the singleton class is loaded. This can be useful 
    in situations where the instance is resource-intensive to create and may not be needed 
    in all cases.

    The "thread-safe" aspect of the singleton means that it is safe to use the singleton 
    instance from multiple threads concurrently. Without thread safety, it is possible for 
    two or more threads to try to create the singleton instance simultaneously, which 
    could result in multiple instances being created.

    In the code I provided, the Lazy<T> class is used to implement the lazy initialization 
    and thread safety of the singleton. The Lazy<T> class provides thread-safe, lazy 
    initialization of an instance by using a double-checked locking pattern to ensure that 
    only one instance is created, even if accessed by multiple threads concurrently.


    To use the singleton, you can access the Instance property, which will return the 
    singleton instance. The first time the Instance property is accessed, the Lazy<T> 
    class will create the DalList instance and store it in a static field. Subsequent 
    accesses to the Instance property will return the same instance, without the need for 
    synchronization. This ensures that the singleton instance is created only once, and that 
    it is safe to use from multiple threads concurrently.


    לבונוס - ניתן לעשות סינגלטון שיהיה Thread Safe ועם Lazy Initialization מירבי - אבל רק אם אתם יודעים להסביר איך עשיתם ולמה זה Thread Safe ו-Lazy Initialization

 */