using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

[ExecuteAlways]
public class Localization
{
    private static LanguageCode _language;

    public static event Action ChangeLanguage;

    private static ILocalizationProvider[] _allLocalization;

    private static bool _isLoad;
    
    public static void UpdateResources()
    {
        _isLoad = false;
        Load();
    }

    public static void SetLanguage(LanguageCode language)
    {
        if (_isLoad == false)
            Load();
        Debug.Log($"Language set {language}");
        _language = language;
        ChangeLanguage?.Invoke();
    }
    public static int GetLanguage => _language.GetHashCode();

    public static void ChangeValue(Component component, string key)
    {
        if (_isLoad == false)
            Load();
        switch (component)
        {
            case Text text:
                text.text = GetString(key);
                break;
            case TextMeshProUGUI textMesh:
                textMesh.text = GetString(key);
                break;
            case AudioSource audioSource:
               audioSource.clip = GetAudioClip(key);
                break;
        }
    }
    
    public static string GetStringToKey(string key) => GetString(key);

    public static AudioClip GetAudioClipToKey(string key) => GetAudioClip(key);
    
    private static void Load()
    {
        var s = Resources.LoadAll("", typeof(ILocalizationProvider));
        _allLocalization = new ILocalizationProvider[s.Length];
        s.CopyTo(_allLocalization, 0);

        if (_allLocalization.Length == 0)
            throw new IndexOutOfRangeException("Localized files not found");
        _isLoad = true;
    }

    private static string GetString(string key)
    {
        foreach (var localization in _allLocalization)
        {
            if (IsSupportedType(typeof(string), localization) == false)
                continue;
            
            var provider = localization as LocalizationProvider<string>;
            if (provider.TryGetValue(key, GetLanguage, out string value))
                return value;
        }
        return ("NULL");
    }

    private static AudioClip GetAudioClip(string key)
    {
        foreach (var localization in _allLocalization)
        {
            if (IsSupportedType(typeof(AudioClip), localization) == false)
                continue;

            var provider = localization as LocalizationProvider<AudioClip>;
            if (provider.TryGetValue(key, GetLanguage, out AudioClip value))
                return value;
        }

        return null;
    }

    private static bool IsSupportedType(Type type, ILocalizationProvider component) 
    {
        return (component.GetType().BaseType.GetGenericArguments().Length != 0 && component.GetType().BaseType.GetGenericArguments()[0] == type);
    }

    ////[Button("Import")]
    //private void ReadCSVFile()
    //{
    //    if (localizationAsset == null)
    //        return;
    //    string[] allLines = localizationAsset.text.Split(new char[] { '\n' });
    //    localizations = new LocalizationData[allLines.Length];
    //    for (int i = 0; i < allLines.Length; i++)
    //    {
    //        string[] data = allLines[i].Split(new char[] { ',' });
    //        LocalizationData newLocData = new LocalizationData(data[0], data[1]);
    //        localizations[i] = newLocData;
    //    }
    //}

}
