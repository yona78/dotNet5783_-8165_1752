
using System.Security.Principal;

namespace BlApi
{
    internal class IBl
    {
        public IProduct Product { get; }
        public IOrder Order { get; }
        public ICart Cart { get; }
    }
}
