using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
/// <summary>
/// Exception to trhow if the user enter object that dont exist 
/// </summary>

public class ExceptionLogicObjectCouldNotBeFound : Exception // the dataBase object you're looking for couldn't be found
{
    string nameOfObject;
    public ExceptionLogicObjectCouldNotBeFound(string msg, ExceptionObjectCouldNotBeFound inner) : base(msg, inner) { nameOfObject = msg; }
    public ExceptionLogicObjectCouldNotBeFound(string msg) { nameOfObject = msg; }
    public override string Message { get => String.Format("ERROR, {0} couldn't be find", nameOfObject); }

}
/// <summary>
/// Exception to trhow if the user enter object even when the lists are full 
/// </summary>
public class ExceptionNotEnoughInDataBase : Exception // there isn't enough from the required thing in the dBase
{
    string nameOfObject;
    public ExceptionNotEnoughInDataBase(string msg) { nameOfObject = msg; }
    public override string Message { get => String.Format("ERROR, {0}, there isn't enough in the dBase", nameOfObject); }

}
/// <summary>
/// Exception to trhow if the user tries to get unreal object 
/// </summary>
public class ExceptionObjectIsNotAviliable : Exception // the data you gave me is not aviliable
{
    string nameOfObject;
    public ExceptionObjectIsNotAviliable(string msg) { nameOfObject = msg; }
    public override string Message { get => String.Format("ERROR, {0} isn't aviliable", nameOfObject); }

}
/// <summary>
/// Exception to trhow if the user enter data that is not valid
/// </summary>
public class ExceptionDataIsInvalid : Exception // the data you gave me is invalid
{
    string nameOfObject;
    public ExceptionDataIsInvalid(string msg) { nameOfObject = msg; }
    public ExceptionDataIsInvalid(string msg, ExceptionObjectAlreadyExist inner) : base(msg, inner) { nameOfObject = msg; }
    public override string Message { get => String.Format("ERROR, {0}, data is invalid", nameOfObject); }

}
/// <summary>
/// Exception to trhow if the user enter object that already exist 
/// </summary>
public class ExceptionLogicObjectAlreadyExist : Exception // the object you're trying to add is already exist
{
    string nameOfObject;
    public ExceptionLogicObjectAlreadyExist(string msg) { nameOfObject = msg; }
    public ExceptionLogicObjectAlreadyExist(string msg, ExceptionObjectAlreadyExist inner) : base(msg, inner) { nameOfObject = msg; }
    public override string Message { get => String.Format("ERROR, {0} is already exist", nameOfObject); }
}




