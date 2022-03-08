using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject VRPlayer;
    [SerializeField] private int health;
    public bool InGame;
    

    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }

    private void Awake()
    {
        health = 100;
        HealthCountdown();
    }
    private void FixedUpdate()
    {
        gameObject.transform.position = new Vector3(VRPlayer.transform.position.x, 1, VRPlayer.transform.position.z);
        if(health < 1)
        {
            GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Bullet")
        {
            health -= 10;
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Walls")
        {
            health -= 5;
        }
    }

    private void HealthCountdown()
    {
        if (InGame == true)
        {
            health -= 1;
        }

        Invoke("HealthCountdown", 2f);
    }

    private void GameOver()
    {
        VRPlayer.transform.position = new Vector3(0, 0, 50);
        InGame = false;
    }

    public void Restart()
    {
        health = 100;
        VRPlayer.transform.position = new Vector3(0,20,0);
    }
}
