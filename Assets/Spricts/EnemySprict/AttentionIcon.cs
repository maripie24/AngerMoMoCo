using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttentionIcon : MonoBehaviour
{
    private float moveDistance = 7f;　  // 上方向への移動距離
    private float moveDuration = 1f;    // 移動にかかる時間
    private float fadeDuration = 1.2f;    // フェードアウトにかかる時間

    private SpriteRenderer spriteRenderer;
    private Vector3 initialPosition;
    private Color initialColor;
    private Color targetColor;

    private void Awake()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();

        initialPosition = this.transform.position;
        initialColor = spriteRenderer.color;
    }

    private void Start()
    {
        transform.DOLocalMoveY(moveDistance, moveDuration).SetEase(Ease.OutQuad);

        spriteRenderer.DOColor(targetColor, fadeDuration)
            .SetEase(Ease.InQuad)
            .OnComplete(Delete);
    }

    private void Delete()
    {
        GameObject.Destroy(this.gameObject);
    }

}
    