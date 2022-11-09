using System;

public interface Idal <T>
{
    void Add<T>(T toAdd);
    void Update<T>(T toUpdate);
    void Delete<T>(T id);
    T Get<T>(int id);
}
