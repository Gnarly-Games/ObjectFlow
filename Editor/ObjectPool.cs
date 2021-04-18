using System.Collections.Generic;

/// <summary>
/// ObjectPool helps to reduce the object creation overhead 
/// by allowing to use the previously created objects of the same type if available.
/// </summary>
/// <typeparam name="T"></typeparam>
public class ObjectPool<T>
{
    /// <summary>
    /// InstantiateEvent defines the object creation logic.
    /// </summary>
    /// <returns>Returns a new object by using the given method.</returns>
    public delegate T InstantiateEvent();
    private InstantiateEvent _instantiateEvent;

    // Pool of the previously created objects.
    private Queue<T> _pool;

    /// <summary>
    /// Create the object pool with the object creation logic. 
    /// </summary>
    /// <param name="instantiateEvent"></param>
    public ObjectPool(InstantiateEvent instantiateEvent)
    {
        _pool = new Queue<T>();
        _instantiateEvent = instantiateEvent;
    }

    /// <summary>
    /// Get an object from the pool.
    /// </summary>
    /// <returns>Returns an existing object if available in the pool or creates a new one.</returns>
    public T Get()
    {
        var selected = _pool.Count > 0 ? _pool.Dequeue() : _instantiateEvent();
        return selected;
    }

    /// <summary>
    /// Put the object back into the pool to be used later.
    /// </summary>
    /// <param name="selected"></param>
    public void Return(T selected)
    {
        _pool.Enqueue(selected);
    }
}