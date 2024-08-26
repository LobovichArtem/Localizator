using System;
using UnityEngine;


public enum LanguageCode
{
    English = 0,
    Russian,
    Polish,
    Lithuanian
}

public class Language : MonoBehaviour
{
    [SerializeField] private LanguageCode _language;

    private void Awake()
    {
        SetLanguage(LoadLanguageCode);
    }

    public void SetLanguage(int code)
    {
        if (IsValidLanguage(code) == false)
            throw new Exception("Invalid language code value.The value is greater than the enum languageCode");

        Localization.SetLanguage((LanguageCode)code);
        _language = (LanguageCode)code;
        SaveLanguageCode(code);
    }

    [ContextMenu("UpdateFiles")]
    public void UpdateFiles()
    {
        Localization.UpdateResources();
    }

    [ContextMenu("SaveCurrentLanguage")]
    public void SaveLanguageCode() => SaveLanguageCode(_language.GetHashCode());

    //private void OnValidate()
    //{
    //    Debug.Log(GetType());
    //    if (_language.GetHashCode() != LoadLanguageCode)
    //    {
    //        int code = _language.GetHashCode();
    //        SaveLanguageCode(code);
    //        SetLanguage(code);
    //    }
    //}

    private int LoadLanguageCode => PlayerPrefs.GetInt("languagecode", 0);

    private void SaveLanguageCode(int language) => PlayerPrefs.SetInt("languagecode", language);

    private bool IsValidLanguage(int code) => code < Enum.GetValues(typeof(LanguageCode)).Length;
}
