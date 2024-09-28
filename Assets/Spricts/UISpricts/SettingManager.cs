using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingManager : MonoBehaviour
{
    [Header("UI要素")]
    public GameObject settingPanel;
    public Slider volumeSlider;

    // [Header("Audio")]
    // public AudioMixer auidioMixier;

    private void Start()
    {

    }

    // Settingパネルを表示する
    public void AwakeSetting()
    {
        settingPanel.SetActive(true);
    }

    // Settingパネルを閉じる
    public void CloseSetting()
    {
        settingPanel.SetActive(false);
    }

    // 音量スライダーの値が変更されたときに呼ばれる
    public void SetVolume(float volume)
    {
        // AudioMixerを使用する場合（ログスケール）
        // audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);

        // AudioListenerを直接使用する場合
        // AudioListener.volume = volume;
    }
}
