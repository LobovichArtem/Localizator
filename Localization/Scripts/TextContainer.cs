using System;
using UnityEngine;

[Serializable]
public class TextContainer : LocalizedContainer<string>
{
    [SerializeField] private string[] _value;

    public TextContainer(string key, string[] value)
    {
        Key = key;
        _value = value;
    }

    public override string Get(int index)
    {
        if (index >= _value.Length || _value[index] == null || _value[index] == string.Empty)
            return _value[0];
        
        return _value[index];
    }
}
