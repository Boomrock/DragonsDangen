using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T>
{
    private Queue<T> _objectsQueue;

    private Func<T> _preloadFunc;
    private Action<T> _getAction;
    private Action<T> _returnAction;

    public ObjectPool(
        int objectsStartCount, 
        Func<T> preloadFunc, 
        Action<T> getAction, 
        Action<T> returnAction)
    {
        _objectsQueue = new Queue<T>();

        _preloadFunc = preloadFunc;
        _getAction = getAction;
        _returnAction = returnAction;

        for (int i = 0; i < objectsStartCount; i++)
        {
            var item = _preloadFunc();

            Return(item);
        }
    }

    public T Get()
    {
        T item = _objectsQueue.Count > 0 ? _objectsQueue.Dequeue() : _preloadFunc();

        _getAction(item);

        return item;
    }

    public void Return(T item) 
    {
        _returnAction(item);
        _objectsQueue.Enqueue(item);
    }
}
