using System;
using System.Collections.Generic;

public class ObjectPool<T> where T : IPoolable
{

    public int Count => queue.Count;

    private readonly Queue<T> queue = new Queue<T>();

    public T Get()
    {
        if (queue.Count > 0)
        {
            T instance = queue.Dequeue();
            instance.OnPoolGet();
            return instance;
        }
        else
        {
            throw new Exception("ObjectPool is empty.");
        }
    }

    public T TryGet()
    {
        if (queue.Count > 0)
        {
            T instance = queue.Dequeue();
            instance.OnPoolGet();
            return instance;
        }
        else
        {
            return default;
        }
    }

    public void Put(T instance)
    {
        queue.Enqueue(instance);
        instance.OnPoolPut();
    }

}