

namespace DO;

public struct Product
{
    public int ID { set; get; }
    public string Name { set; get; }
    public double Price { set; get; }
    public enum Category /// נראה לי ש - Enums צריכים להיות במחלקה של Enums
    public Category category { set; get; }
    public int InStock { set; get; }
    public override string ToString() => $@"
       Product ID={ID}: {Name}, 
       category - {Category} /// נראה לי ש - Enums צריכים להיות במחלקה של Enums
    	Price: {Price}
    	Amount in stock: {InStock}
    ";
}
