

namespace DO;

public struct Product
{
    public int ID { set; get; }
    public string Name { set; get; }
    public double price { set; get; }
    public enum Category
    public Category category { set; get; }
    public int InStock { set; get; }
    public override string ToString() => $@"
       Product ID={ID}: {Name}, 
       category - {Category}
    	Price: {Price}
    	Amount in stock: {InStock}
    ";
}
