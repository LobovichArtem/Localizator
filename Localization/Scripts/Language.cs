using System;
using UnityEngine;

public class Language : MonoBehaviour
{
    [SerializeField] private Text _currentLanguageCode; 
    
    private void Awake()
    {
        SetLanguage(LoadLanguageCode);
    }

    public void SetLanguage(int code)
    {
        if (IsValidLanguage(code) == false)
            throw new Exception("Invalid language code value.The value is greater than the enum languageCode");

        Localization.SetLanguage((LanguageCode)code);
        SaveLanguageCode(code);
        
        DisplayCurrentLanguage();    
    }

    public void SwitchLanguage()
    {
        var code = Localization.GetLanguage;
        code++;
        if (code >= Enum.GetValues(typeof(LanguageCode)).Length)
            code = 0;
        SetLanguage(code);
    }

    [ContextMenu("UpdateFiles")]
    public void UpdateFiles()
    {
        Localization.UpdateResources();
    }

    public void DisplayCurrentLanguage()
    {
        if(_currentLanguageCode != null) 
            _currentLanguageCode.text = Localization.Language.ToString();    
    }

    [ContextMenu("SaveCurrentLanguage")]
    public void SaveLanguageCode() => SaveLanguageCode(_language.GetHashCode());

    private int LoadLanguageCode => PlayerPrefs.GetInt("languagecode", 0);

    private void SaveLanguageCode(int language) => PlayerPrefs.SetInt("languagecode", language);

    private bool IsValidLanguage(int code) => code < Enum.GetValues(typeof(LanguageCode)).Length;
}
