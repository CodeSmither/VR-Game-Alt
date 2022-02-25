using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject VRPlayer;
    [SerializeField] private int Health;

    private void Awake()
    {
        Health = 100;
    }
    private void FixedUpdate()
    {
        gameObject.transform.position = new Vector3(VRPlayer.transform.position.x, 1, VRPlayer.transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Bullet")
        {
            Health -= 10;
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Walls")
        {
            Health -= 5;
        }
    }
}
