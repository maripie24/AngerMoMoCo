using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class AngerGauge : MonoBehaviour
{
    [SerializeField] private Image angerImage;

    private AngerSwitcher angerSwitcher;

    public float duration = 0.5f;
    public float strength = 20f;
    public int vibrate = 100;

    public float debugAngerRate = 0.2f;
    public float currentRate = 0f;
    public float angerTime = 20f; // �A���K�[��Ԃ̊���

    public bool isFull = false; // �Q�[�W�����^���ɂȂ�����true
    public bool isZero = false; // �Q�[�W���[���ɂȂ�����true
    private bool isPulsing = false; // �p���X���ʂ̃t���O
    private Tween pulseTween; // Tween�̎Q��

    private void Start()
    {
        angerSwitcher = GameObject.Find("Player").GetComponent<AngerSwitcher>();    
        SetGauge(1f); // �Q�[�W�̃[���ɃZ�b�g����
    }

    private void Update()
    {
        Debug.Log($"{currentRate}�ł�"); // �Q�[�W�̐��l�`�F�b�N

        if (currentRate >= 1f) // �Q�[�W���^��
        {
            isFull = true;
            isZero = false;

            SetGauge(1f); // AngerRate��1�ȏ�ɂ��Ȃ��悤�ɌŒ肷��

            // �m�[�}���A�A���K�[��Ԃ̃g���K�[���O��
            angerSwitcher.EnableAngerSwitch();
            angerSwitcher.EnableNormalSwitch();

            StartPulsing(); // �p���X���ʂ��J�n

            // ������Shake�������ێ�
            transform.DOShakePosition(duration * 0.5f, strength, vibrate);
        }
        else if (currentRate > 0f)
        {
            isFull = false;
            isZero = false;
            StopPulsing(); // �p���X���ʂ��~
        }
        else // �Q�[�W�[��
        {
            isFull = false;
            isZero = true;
            StopPulsing(); // �p���X���ʂ��~

            SetGauge(0f); // �Q�[�W���[���ŌŒ�
            // �Q�[�W���[���ɂȂ�����Normal��Ԃɐ؂�ւ���
            angerSwitcher.SwitchToNormal();
        }

        // �f�o�b�O
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AddAnger(debugAngerRate);
        }
    }

    /// <summary>
    /// �p���X���ʂ��J�n���郁�\�b�h
    /// </summary>
    private void StartPulsing()
    {
        isPulsing = true;
        // **�ǉ�: �p���X���ʂ�Tween��ݒ�**
        pulseTween = transform.DOScale(1.1f, 0.5f) // �g��
            .SetLoops(-1, LoopType.Yoyo) // �J��Ԃ�
            .SetEase(Ease.InOutSine);
    }

    /// <summary>
    /// �p���X���ʂ��~���郁�\�b�h
    /// </summary>
    private void StopPulsing()
    {
        isPulsing = false;
        if (pulseTween != null && pulseTween.IsActive())
        {
            pulseTween.Kill(); // Tween���~
        }

        // **�ǉ�: �Q�[�W�̃X�P�[�������ɖ߂�**
        transform.localScale = new Vector3 (0.6f,0.6f,0.6f);
    }

    public void SetGauge(float targetRate)
    {
        angerImage.DOFillAmount(targetRate, duration * 0.5f);

        currentRate = targetRate;
    }

    // �Q�[�W�𑝂₷
    public void AddAnger(float rate)
    {
        SetGauge(currentRate + rate);
    }

    // �Q�[�W�����炷
    public void DecreaseAnger()
    {
        // �Q�[�W���l�����炷
        currentRate -= 0.001f;
        SetGauge(currentRate);
    }
}
