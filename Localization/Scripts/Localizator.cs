using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteAlways]
public class Localizator : MonoBehaviour
{
    public string Key { get { return _key; } set { _key = value; ChangeValue(); } }
    [SerializeField] private string _key;
    private string _previousKey;

    private Component _component;

    private void Awake()
    {
        Localization.ChangeLanguage += ChangeValue;
        if (TryGetComponent(out Text text))
        {
            _component = text;
            ChangeValue();
            return;
        }

        if (TryGetComponent(out TextMeshProUGUI textMesh))
        {
            _component = textMesh;
            ChangeValue();
            return;
        }

        if (TryGetComponent(out AudioSource audioSource))
        {
            _component = audioSource;
            ChangeValue();
            return;
        }
        Localization.ChangeLanguage -= ChangeValue;
        DestroyImmediate(this);
        throw new Exception("This component that can be used for translation was not found (needed AudioSource, Text)");        
    }

    private void OnDestroy() => Localization.ChangeLanguage -= ChangeValue;

    private void OnValidate()
    {
        if (_key != _previousKey)
            ChangeValue();
    }

    public void ChangeValue()
    {
        Localization.ChangeValue(_component, _key);
        _previousKey = _key;
    }

    
}


//[CreateAssetMenu(fileName = "Localizator", menuName = "ScriptableObjects/Localizator")]
//public class Localizator : ScriptableObject
//{
//    #region PUBLIC

//    public string GetLocalizedText(string key)
//    {
//        foreach(Localization localization in allLocalizations)
//        {
//            if (localization.language == language)
//                return localization.GetLocalizedText(key);
//        }
//        return "Error, no key or language" + key;
//    }

//    public Language GetCurrentlanguage()
//    {
//        return language;
//    }
//    #endregion

//    #region PRIVATE
//    [SerializeField] private Language language;
//    [SerializeField] private Localization[] allLocalizations = new Localization[0];

//    //private void Awake()
//    //{
//    //    if (_instance != null && _instance != this)
//    //    {
//    //        DestroyImmediate(this.gameObject);
//    //    }
//    //    else
//    //    {
//    //        _instance = this;
//    //        DontDestroyOnLoad(gameObject);
//    //    }

//    //}
//    #endregion

