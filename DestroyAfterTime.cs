using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float seconds;

    void Start()
    {
        StartCoroutine(WaitForDestroy(seconds));
    }


    IEnumerator WaitForDestroy(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
