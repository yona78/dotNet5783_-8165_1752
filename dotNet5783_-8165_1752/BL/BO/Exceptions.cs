using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{ // i should initalize these classes

    public class ExceptionListIsFull : Exception // maybe the list is full, and you're trying to push more objects into it
{
    internal class Exceptions
        public override string Message { get => "ERROR, list is full"; }
    }

    public class ExceptionObjectCouldNotBeFound : Exception // the object you're looking for couldn't be found
    {
        string nameOfObject;
        public ExceptionObjectCouldNotBeFound(string msg) { nameOfObject = msg; }
        public override string Message { get => String.Format("ERROR, {0} couldn't be find", nameOfObject); }

    }

    public class ExceptionObjectAlreadyExist : Exception // the object you're trying to add is already exist
    {
        string nameOfObject;
        public ExceptionObjectAlreadyExist(string msg) { nameOfObject = msg; }
        public override string Message { get => String.Format("ERROR, {0} is already exist", nameOfObject); }
    }

}

