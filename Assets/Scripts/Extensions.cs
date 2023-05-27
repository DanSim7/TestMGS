using UnityEngine;
using DG.Tweening;

public static class Extensions
{
    public static void PlayPulseAnim(this Transform animatingObject)
    {
        DOTween.Sequence()
            .Append(animatingObject.DOScale(new Vector2(1.2f, 1.2f), 0.3f))
            .Append(animatingObject.DOScale(Vector2.one, 0.3f))
            .SetLink(animatingObject.gameObject);
    }
}
