using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class AngerGauge : MonoBehaviour
{
    [SerializeField] private Image angerImage;
    [SerializeField] private Image maxAngerImage;

    private AngerSwitcher angerSwitcher;

    public float duration = 0.5f;

    public float debugAngerRate = 0.2f;
    public float currentRate = 0f;
    public float angerTime = 20f; // �A���K�[��Ԃ̊���

    public bool isFull = false; // �Q�[�W�����^���ɂȂ�����true
    public bool isZero = false; // �Q�[�W���[���ɂȂ�����true
    private Tween pulseTween; // Tween�̎Q��

    private void Start()
    {
        angerSwitcher = GameObject.Find("Player").GetComponent<AngerSwitcher>();    
        SetGauge(0.8f); // �Q�[�W�̃[���ɃZ�b�g����

        maxAngerImage.gameObject.SetActive(false);// ������Ԃ�maxAngerImage���A�N�e�B�u�ɂ���
    }

    private void Update()
    {
        Debug.Log($"{currentRate}�ł�"); // �Q�[�W�̐��l�`�F�b�N

        if (currentRate >= 1f) // �Q�[�W���^��
        {
            isFull = true;
            isZero = false;

            SetGauge(1f); // AngerRate��1�ȏ�ɂ��Ȃ��悤�ɌŒ肷��
            StartPulse();

            // ���^������angerImage���A�N�e�B�u�AmaxAngerImage���A�N�e�B�u�ɂ���
            angerImage.gameObject.SetActive(false);
            maxAngerImage.gameObject.SetActive(true);

            // �m�[�}���A�A���K�[��Ԃ̃g���K�[���O��
            angerSwitcher.EnableAngerSwitch();
            angerSwitcher.EnableNormalSwitch();
        }
        else if (currentRate > 0f)
        {
            isFull = false;
            isZero = false;
            StopPulse();
        }
        else // �Q�[�W�[��
        {
            isFull = false;
            isZero = true;
            StopPulse();

            SetGauge(0f); // �Q�[�W���[���ŌŒ�

            // �Q�[�W���[����angerImage���A�N�e�B�u�AmaxAngerImage���A�N�e�B�u�ɂ���
            angerImage.gameObject.SetActive(true);
            maxAngerImage.gameObject.SetActive(false);

            // �Q�[�W���[���ɂȂ�����Normal��Ԃɐ؂�ւ���
            angerSwitcher.SwitchToNormal();
        }

        // �f�o�b�O
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AddAnger(debugAngerRate);
        }
    }

    private void StartPulse()
    {
        if (pulseTween != null && pulseTween.IsActive()) return;

        // �X�P�[����1.2�{�Ɋg�債�A���ɖ߂����[�v�A�j���[�V����
        pulseTween = maxAngerImage.transform.DOScale(1.2f, 0.5f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    private void StopPulse()
    {
        if (pulseTween != null && pulseTween.IsActive())
        {
            pulseTween.Kill();
            maxAngerImage.transform.localScale = Vector3.one; // �X�P�[�������ɖ߂�
        }
    }

    public void SetGauge(float targetRate)
    {
        angerImage.DOFillAmount(targetRate, duration * 0.5f);
        maxAngerImage.DOFillAmount(targetRate, duration * 0.5f);

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
        currentRate -= 0.0006f;
        SetGauge(currentRate);
    }
}
