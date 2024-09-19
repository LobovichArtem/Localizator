using System;
using UnityEngine;
using UnityEngine.UI;

public class Language : MonoBehaviour
{
    [SerializeField] private LanguageCode _language;
    [SerializeField] private Text _currentLanguageCode; 

    private void Awake()
    {
        SetLanguage(LoadLanguageCode);
    }

    public void SwitchLanguage()
    {
        var code = Localization.GetLanguage;
        code++;
        if (code >= Enum.GetValues(typeof(LanguageCode)).Length)
            code = 0;
        SetLanguage(code);
    }

    public void SetLanguage(int code)
    {
        if (IsValidLanguage(code) == false)
            throw new Exception("Invalid language code value.The value is greater than the enum languageCode");

        Localization.SetLanguage((LanguageCode)code);
        _language = (LanguageCode)code;
        SaveLanguageCode(code);
        _currentLanguageCode.text = Localization.Language.ToString();
    }

    [ContextMenu("UpdateFiles")]
    public void UpdateFiles()
    {
        Localization.UpdateResources();
    }

    [ContextMenu("SaveCurrentLanguage")]
    public void SaveLanguageCode() => SaveLanguageCode(_language.GetHashCode());

    private int LoadLanguageCode => PlayerPrefs.GetInt("languagecode", 0);

    private void SaveLanguageCode(int language) => PlayerPrefs.SetInt("languagecode", language);

    private bool IsValidLanguage(int code) => code < Enum.GetValues(typeof(LanguageCode)).Length;

}
