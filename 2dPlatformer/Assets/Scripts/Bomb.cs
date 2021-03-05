using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] float timeToExplode;
    [SerializeField] PointEffector2D effector;
    private Animator anim;
    void Start()
    {
        effector.enabled = false;
        anim = GetComponent<Animator>();
        StartCoroutine(WaitToExplode());
    }

    private IEnumerator WaitToExplode()
    {
        yield return new WaitForSeconds(timeToExplode);
        anim.SetTrigger("Explode");
    }

    public void Explosion()
    {
        effector.enabled = true;
        StartCoroutine(TurnOffEffector());
    }

    public void DestroyBombObject()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }

    private IEnumerator TurnOffEffector()
    {
        yield return new WaitForSeconds(0.05f);
        effector.enabled = false;
    }
}
