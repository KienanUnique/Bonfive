using System.Collections.Generic;

public class ObjectRegistrator<T>{
    protected readonly List<T> _objectsList = new List<T>();
    public int Count => _objectsList.Count;
    public delegate void OnCountChanged();
    public event OnCountChanged ObjectAdd;
    public event OnCountChanged ObjectRemove;

    public virtual void Add(T objectToAdd){
        _objectsList.Add(objectToAdd);
        ObjectAdd?.Invoke();
    }

    public virtual void Remove(T objectToRemove){
        _objectsList.Remove(objectToRemove);
        ObjectRemove?.Invoke();
    }
}