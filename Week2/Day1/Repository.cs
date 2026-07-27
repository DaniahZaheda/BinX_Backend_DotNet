using System;
using System.Collections.Generic;

public class Repository<T> where T : class
{
    private List<T> items = new List<T>();

    public void Add(T item)
    {
        items.Add(item);
    }

    public IReadOnlyList<T> GetAll()
    {
        return items.AsReadOnly();
    }

    public T Find(Predicate<T> predicate)
    {
        return items.Find(predicate);
    }
}