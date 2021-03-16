using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DeathMenu : MonoBehaviour
{
    private RectTransform rect;
    void Awake()
    {
        rect = GetComponent<RectTransform>();
        rect.DOAnchorPos(Vector2.zero, 0.5f).SetEase(Ease.OutBack);
    }
  
}
