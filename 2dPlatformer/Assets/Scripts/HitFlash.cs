using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFlash : MonoBehaviour
{
    [HideInInspector] public bool isFlashing;
    [SerializeField] SpriteRenderer sprRend;
    private float index;
    private float amplitude;
    private float interval;

    void Start()
    {
        index = 0;
        amplitude = 0.8f;
        interval = 12f;
        isFlashing = false;
    }

    void Update()
    {
        if (isFlashing)
        {
            index += Time.deltaTime;
            float flashAmount = Mathf.Abs(amplitude * Mathf.Sin(interval * index));
            sprRend.material.SetFloat("_FlashAmount", flashAmount);
        }
    }
    public void MakeItFlash(float time)
    {
        StartCoroutine(TimeToFlash(time));
    }

    private IEnumerator TimeToFlash(float time)
    {
        isFlashing = true;
        yield return new WaitForSeconds(time);
        isFlashing = false;
        sprRend.material.SetFloat("_FlashAmount", 0f);
    }

}
