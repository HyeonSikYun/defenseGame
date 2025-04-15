using UnityEngine;

[CreateAssetMenu(fileName = "AudioSettings", menuName = "Audio/AudioSettings")]
public class AudioSettings : ScriptableObject
{
    public float musicVolume = 0.3f;
    public float sfxVolume = 0.3f;
}