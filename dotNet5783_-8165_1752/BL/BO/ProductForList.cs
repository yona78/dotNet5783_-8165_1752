using DO;

namespace BO;
public class ProductForList
{
    public int ID { set; get; }
    public string Name { set; get; }
    public double Price { set; get; }
    public Enums.Category Category { set; get; }

    public override string ToString() => $@"
       ID:{ID}
       Name: {Name}
       Price: {Price}
       Category: {Category}
    ";

}