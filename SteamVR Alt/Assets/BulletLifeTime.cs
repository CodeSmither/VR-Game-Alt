using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLifeTime : MonoBehaviour
{
    void Awake()
    {
        StartCoroutine(EndOfLife());
    }
    IEnumerator EndOfLife()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Walls")
        {
            Destroy(gameObject);
        }
    }
}
