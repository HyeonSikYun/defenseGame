using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioMixer audioMixer;
    public AudioSource musicSource;
    public List<AudioSource> sfxSources;
    public int sfxSourcesCount = 15;

    public AudioSettings audioSettings;

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
    }

    public List<Sound> sounds;

    private const float MinVolume = -40f;
    private const float MaxVolume = 0f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeSFXSources();
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // 저장된 설정 로드
        LoadSettings();
    }

    void Start()
    {
        // 초기 볼륨 설정 적용
        SetVolume("MusicVolume", audioSettings.musicVolume);
        SetVolume("SFXVolume", audioSettings.sfxVolume);
    }

    private void InitializeSFXSources()
    {
        sfxSources = new List<AudioSource>();
        for (int i = 0; i < sfxSourcesCount; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.outputAudioMixerGroup = audioMixer.FindMatchingGroups("SFX")[0];
            sfxSources.Add(source);
        }
    }

    public void SetVolume(string parameterName, float sliderValue)
    {
        float volume = (sliderValue <= 0.01f) ? -80f : Mathf.Lerp(MinVolume, MaxVolume, sliderValue);
        audioMixer.SetFloat(parameterName, volume);

        // 설정 업데이트 및 저장
        if (parameterName == "MusicVolume")
            audioSettings.musicVolume = sliderValue;
        else if (parameterName == "SFXVolume")
            audioSettings.sfxVolume = sliderValue;

        SaveSettings();
    }

    public float GetVolume(string parameterName)
    {
        return parameterName == "MusicVolume" ? audioSettings.musicVolume : audioSettings.sfxVolume;
    }

    public void PlaySound(string soundName)
    {
        Sound sound = sounds.Find(s => s.name == soundName);
        if (sound != null)
        {
            AudioSource availableSource = sfxSources.Find(source => !source.isPlaying);
            if (availableSource == null)
            {
                Debug.LogWarning("모든 AudioSource가 사용 중입니다. 소리를 재생할 수 없습니다.");
                return;
            }
            availableSource.clip = sound.clip;
            availableSource.Play();
        }
        else
        {
            Debug.LogWarning($"Sound {soundName} not found!");
        }
    }

    private void LoadSettings()
    {
        if (audioSettings == null)
        {
            Debug.LogError("AudioSettings asset is not assigned to SoundManager!");
            return;
        }
    }

    private void SaveSettings()
    {
        // ScriptableObject의 변경사항을 저장
        #if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(audioSettings);
        UnityEditor.AssetDatabase.SaveAssets();
        #endif
    }
}