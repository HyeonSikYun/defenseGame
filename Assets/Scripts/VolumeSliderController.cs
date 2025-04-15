using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderController : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        // 현재 설정된 볼륨으로 슬라이더 초기화
        musicSlider.value = SoundManager.Instance.GetVolume("MusicVolume");
        sfxSlider.value = SoundManager.Instance.GetVolume("SFXVolume");

        // 슬라이더 이벤트 연결
        musicSlider.onValueChanged.AddListener(delegate { OnMusicSliderChanged(); });
        sfxSlider.onValueChanged.AddListener(delegate { OnSFXSliderChanged(); });
    }

    void OnMusicSliderChanged()
    {
        SoundManager.Instance.SetVolume("MusicVolume", musicSlider.value);
    }

    void OnSFXSliderChanged()
    {
        SoundManager.Instance.SetVolume("SFXVolume", sfxSlider.value);
    }
}