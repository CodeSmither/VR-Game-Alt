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
    private float LengthofTime;
    private TotalScore totalScore;
    private float heightValue;
    [SerializeField]private GameObject MovementBall;

    //gets and sets the height of the players hitbox based on their standing on crouching position
    public float HeightValue
    {
        get
        {
            return heightValue;
        }

        set
        {
            heightValue = value;
        }
    }
    //gets and sets the players health
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
        MovementBall = GameObject.Find("MovementBall");
        totalScore = GameObject.Find("GameOverUI").GetComponent<TotalScore>();
        health = 100;
        LengthofTime = 1f;
        HeightValue = 1f;
        HealthCountdown();
        Invoke("MovementStopped", 1f);
    }
    private void FixedUpdate()
    {
        gameObject.transform.position = new Vector3(VRPlayer.transform.position.x, HeightValue , VRPlayer.transform.position.z);
        if (health < 1)
        {
            GameOver();
        }
        //stores the weapon packages
        GameObject swordpackage = GameObject.Find("Sword");
        GameObject gunpackage = GameObject.Find("Gun");
        // chooses what sources to apply haptic feedback to based on item sets
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
    // triggers haptics when the player is shot
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
            Haptics(1f, 90f, 0.7f, SteamVR_Input_Sources.RightHand);
            Haptics(1f, 90f, 0.7f, SteamVR_Input_Sources.LeftHand);
        }
    }

    //Health Decreases overtime during the game
    private void HealthCountdown()
    {
        LengthofTime = 1 + (totalScore.Points/1000);
        if (InGame == true)
        {
            health -= 1;
        }

        Invoke("HealthCountdown", 2f / LengthofTime);
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
    // checks if the player is not moving Via AI then disables the movement ball if they are
    private void MovementStopped()
    {
        NavMeshAgent PlayerNaveMeshAgent = GameObject.Find("Player").GetComponent<NavMeshAgent>();
        Vector3 teleportpoint = PlayerNaveMeshAgent.destination;

        if (Vector3.Distance(PlayerNaveMeshAgent.destination, teleportpoint) < 1f)
        {
            MovementBall.SetActive(false);
        }
        Invoke("MovementStopped", 1f);
    }
}
