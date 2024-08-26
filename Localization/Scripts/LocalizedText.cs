using UnityEngine;


[CreateAssetMenu(fileName = "LocalizedText", menuName = "LOCALIZATION/LocalizedText")]
public class LocalizedText : LocalizationProvider<string>
{
    [SerializeField] private TextAsset _localizationFile;
    private const char LANGUAGE_SEPARATOR = '\t';
    [Space(20)]
    [SerializeField] private TextContainer[] _localization;

    public override LocalizedContainer<string>[] Localization => _localization;

    [ContextMenu("ReplaceFile")]
    [ExecuteInEditMode]
    public void Replace()
    {
        if (TryReadFile(out string[] lines))
        {
            _localization = GetTextContainer(lines);
        }
    }

    private bool TryReadFile(out string[] lines)
    {
        lines = default;
        if (_localizationFile == null)
            return false;

        lines = _localizationFile.text.Split(new char[] { '\n' });
        return true;

    }

    private TextContainer [] GetTextContainer(string[] lines)
    {
        var container = new TextContainer[lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            string key = lines[i].Substring(0, lines[i].IndexOf(LANGUAGE_SEPARATOR));
            var newLine = lines[i].Remove(0, key.Length);//, string.Empty, System.StringComparison.OrdinalIgnoreCase);
            string[] data = newLine.Split(new char[] { LANGUAGE_SEPARATOR }, System.StringSplitOptions.RemoveEmptyEntries);            

            container[i] = new TextContainer(key, data);
        }
        return container;
    }
}

