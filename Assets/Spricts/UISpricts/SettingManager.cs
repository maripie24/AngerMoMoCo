using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingManager : MonoBehaviour
{
    [Header("UI�v�f")]
    public GameObject settingPanel;
    public Slider volumeSlider;

    // [Header("Audio")]
    // public AudioMixer auidioMixier;

    private void Start()
    {

    }

    // Setting�p�l����\������
    public void AwakeSetting()
    {
        settingPanel.SetActive(true);
    }

    // Setting�p�l�������
    public void CloseSetting()
    {
        settingPanel.SetActive(false);
    }

    // ���ʃX���C�_�[�̒l���ύX���ꂽ�Ƃ��ɌĂ΂��
    public void SetVolume(float volume)
    {
        // AudioMixer���g�p����ꍇ�i���O�X�P�[���j
        // audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);

        // AudioListener�𒼐ڎg�p����ꍇ
        // AudioListener.volume = volume;
    }
}
