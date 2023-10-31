using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
//��� ȹ�� �ִϸ��̼�
public class ItemFx : MonoBehaviour
{
    public void Explosion(Vector2 from, Vector2 to, float explo_range)
    {
        transform.position = from;
        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOMove(from + Random.insideUnitCircle * explo_range, 0.25f).SetEase(Ease.OutCubic));
        sequence.Append(transform.DOMove(to, 0.5f).SetEase(Ease.InCubic));

        sequence.AppendCallback(() =>
        {
            Destroy(gameObject);
        });
    }
}
