using System;
using UnityEngine;

[Serializable]
public class AudioClipContainer : LocalizedContainer<AudioClip>
{
    [SerializeField] private AudioClip[] _value;

    public override AudioClip Get(int index)
    {
        if (index >= _value.Length || _value[index] == null)
            return _value[0];
        return _value[index];
    }
}
