using DO;
using System;

namespace DalApi;
public interface ICrud <T>
{
    public int Add(T toAdd);// func that adds an T to the list of T's, and return its id 
    public T Get(int id); // func that reutrns T by its id
    public IEnumerable<T?> GetDataOf(Func<T?, bool>? predict=null); // func that returns all of the T's
    public void Delete(int id);// func that deletes T from it's list
    public void Update(T toUpdate);// func that updates an T in his list
}
