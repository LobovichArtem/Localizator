using UnityEngine;

[CreateAssetMenu(fileName = "LocalizedAudioClip", menuName = "LOCALIZATION/LocalizedAudioClip")]
public class LocalizedAudioClip : LocalizationProvider<AudioClip>
{
    [SerializeField] private AudioClipContainer[] _localization;

    public override LocalizedContainer<AudioClip>[] Localization => _localization;
    
}
