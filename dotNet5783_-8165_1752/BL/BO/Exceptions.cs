using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
// i should initalize these classes

/*
public class ExceptionListIsFull : Exception // maybe the list is full, and you're trying to push more objects into it
{
    public override string Message { get => "ERROR, list is full"; }
}
*/
public class ExceptionLogicObjectCouldNotBeFound : Exception // the dataBase object you're looking for couldn't be found
{
    string nameOfObject;
    public ExceptionLogicObjectCouldNotBeFound(string msg, ExceptionObjectCouldNotBeFound inner) : base(msg, inner) { nameOfObject = msg; }
    public ExceptionLogicObjectCouldNotBeFound(string msg) { nameOfObject = msg; }
    public override string Message { get => String.Format("ERROR, {0} couldn't be find", nameOfObject); }

}

    public class ExceptionNotEnoughInDataBase : Exception // the data you gave me is invalid
{
    string nameOfObject;
    public ExceptionNotEnoughInDataBase(string msg) { nameOfObject = msg; }
    public override string Message { get => String.Format("ERROR, {0} couldn't be find", nameOfObject); }

}
public class ExceptionObjectIsNotAviliable : Exception // the data you gave me is invalid
{
    string nameOfObject;
    public ExceptionObjectIsNotAviliable(string msg) { nameOfObject = msg; }
    public override string Message { get => String.Format("ERROR, {0} couldn't be find", nameOfObject); }

}

public class ExceptionDataInvalid : Exception // the data you gave me is invalid
{
    string nameOfObject;
    public ExceptionDataInvalid(string msg) { nameOfObject = msg; }
    public override string Message { get => String.Format("ERROR, {0} couldn't be find", nameOfObject); }

}
public class ExceptionLogicObjectIsFoundInOrders : Exception // the object you're trying to add is already exist
{
    string nameOfObject;
    public ExceptionLogicObjectIsFoundInOrders(string msg) { nameOfObject = msg; }
    public override string Message { get => String.Format("ERROR, {0} is already exist", nameOfObject); }
}
public class ExceptionLogicObjectAlreadyExist : Exception // the object you're trying to add is already exist
{
    string nameOfObject;
    public ExceptionLogicObjectAlreadyExist(string msg) { nameOfObject = msg; }
    public override string Message { get => String.Format("ERROR, {0} is already exist", nameOfObject); }
}



