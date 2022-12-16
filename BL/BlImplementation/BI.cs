using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BlApi;
namespace BlImplementation;
/// <summary>
/// Class to use in the menu for get accses to all the functions of the products
/// </summary>
sealed internal class Bl : IBl
{
    public IProduct Product { get; } = new BlImplementation.Product();

    public IOrder Order { get; } = new BlImplementation.Order();

    public ICart Cart { get; } = new BlImplementation.Cart();
}

