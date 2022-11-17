
namespace BO
{
    public class ProductItem // product in the cart of the client
    {
        public int ID { set; get; } // id of product
        public string Name { set; get; } // name of product
        public double Price { set; get; } // price of product
        public Enums.Category Category { set; get; } // category of product
        public bool InStock { set; get; } // is the product has enugh in the Dbase
        public int Amount { set; get; } // amount of items from this product in the cart
        public override string ToString() => $@"
       ID:{ID}
       Product ID: {ID}
       Name: {Name}
       category: {Category}
       Price: {Price}
       Category: {Category}
       Amount: {Amount}
       InStock: {(InStock ? "yes" : "no")}
    "; // to string.
    }
}

