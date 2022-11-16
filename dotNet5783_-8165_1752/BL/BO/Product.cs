using DO;

namespace BO;
public class Product
{
    public int ID { set; get; }
    public string Name { set; get; }
    public double Price { set; get; }
    public Enums.Category Category { set; get; }
    public int InStock { set; get; }

    public override string ToString() => $@"
       ID:{ID}
       Name: {Name}
       Price: {Price}
       Category: {Category}
       In Stock: {InStock}
    ";

}