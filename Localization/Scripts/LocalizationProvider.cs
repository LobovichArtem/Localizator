using System.Linq;
using UnityEngine;

public abstract class LocalizationProvider<T> : ScriptableObject, ILocalizationProvider
{
    public abstract LocalizedContainer<T>[] Localization { get; }

    public bool TryGetValue(string key, int index, out T value)
    {
        value = default;
        var container = Localization.FirstOrDefault(x => x.Key.ToLower() == key.ToLower());
        if (container != null)
        {
            value = container.Get(index);
            return true;
        }
        return false;
    }

}
