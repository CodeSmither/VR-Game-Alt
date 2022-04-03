using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using UnityEngine.AI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject VRPlayer;
    [SerializeField] private int health;
    public SteamVR_Action_Vibration[] damagefeedbackArray;
    private SteamVR_Action_Vibration damagefeedback;
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
        if (health < 1)
        {
            GameOver();
        }

        GameObject swordpackage = GameObject.Find("Sword");
        GameObject gunpackage = GameObject.Find("Gun");
        if (swordpackage != null && gunpackage == null)
        {
            damagefeedback = damagefeedbackArray[0];
        }
        else if (swordpackage == null && gunpackage != null)
        {
            damagefeedback = damagefeedbackArray[1];
        }
        else if (swordpackage == null && gunpackage == null)
        {
            damagefeedback = damagefeedbackArray[2];
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Bullet")
        {
            health -= 10;

            Destroy(other.gameObject);
            Haptics(1f,90f,0.7f,SteamVR_Input_Sources.RightHand);
            Haptics(1f, 90f, 0.7f, SteamVR_Input_Sources.LeftHand);
        }
    }

    //Checks the player isn't touching the walls or they take damage
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Walls")
        {
            health -= 5;
        }
    }

    //Health Decreases overtime during the game
    private void HealthCountdown()
    {
        if (InGame == true)
        {
            health -= 1;
        }

        Invoke("HealthCountdown", 2f);
    }
    
    // sends the player to the gameover area when they run out of health
    private void GameOver()
    {
        
        VRPlayer.GetComponent<NavMeshAgent>().enabled = false;
        VRPlayer.transform.position = new Vector3(0, 0, 50);
        InGame = false;
    }

    //resets the players health and returns them to the menu
    public void Restart()
    {
        health = 100;
        VRPlayer.transform.position = new Vector3(0,20,0);
    }
    // helps apply haptic feedback when called
    private void Haptics(float duration,float frequency, float amps, SteamVR_Input_Sources HapticSource)
    {
        damagefeedback.Execute(0,duration, frequency, amps, HapticSource);
    }
}
