
using System.Security.Principal;

namespace BlApi
{
    /// <summary>
    /// The main interface to use all the interfaces of the objects 
    /// </summary>
    public interface IBl
    {
        public IProduct Product { get; }
        public IOrder Order { get; }
        public ICart Cart { get; }
    }
}
