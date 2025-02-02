﻿
/// not a special thing, regular abstract class.

namespace DO;
/// <summary>
/// public struct for abstract product class
/// </summary>
public struct Product
{
    public int ID { set; get; }
    public string? Name { set; get; }
    public double Price { set; get; }
    public Enums.Category? Category { set; get; }
    public int InStock { set; get; }
    public override string ToString() => $@"
       Product ID: {ID}
       Name: {Name}
       category: {Category}
       Price: {Price}
       Amount in stock: {InStock}
    ";
}