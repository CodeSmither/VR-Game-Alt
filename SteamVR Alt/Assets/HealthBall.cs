using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBall : MonoBehaviour
{
   PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth = GameObject.Find("PlayerHitBox").GetComponent<PlayerHealth>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer != 6 && other.gameObject.tag == "RayCastable")
        {
            playerHealth.Health += 40;
            Destroy(gameObject);
        }
    }
}
