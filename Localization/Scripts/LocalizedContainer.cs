using System;

[Serializable]
public abstract class LocalizedContainer<T>
{
    public string Key;
    
    public abstract T Get(int index);

}
