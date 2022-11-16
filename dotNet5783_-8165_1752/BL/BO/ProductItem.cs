using DO;

namespace BO;
public class ProductItem
{
    public int ID { set; get; }
    public string Name { set; get; }
    public double Price { set; get; }
    public Enums.Category Category { set; get; }
    public int Amount { set; get; }
    public bool InStock { set; get; }
    public override string ToString() => $@"
       ID:{ID}
       Name: {Name}
       Price: {Price}
       Category: {Category}
       Amount: {Amount}
       In Stock: {InStock}
    ";

}