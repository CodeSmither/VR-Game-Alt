using System.Collections;
using System;
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
    private NavMeshAgent PlayerNavMeshAgent;
    private GameObject CrouchBox;
    private Sword sword;
    private SampleGun sampleGun;

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
        sword = null;
        sampleGun = null;
        CrouchBox = GameObject.Find("PlayerCrouchingBox");
        MovementBall = GameObject.Find("MovementBall");
        totalScore = GameObject.Find("GameOverUI").GetComponent<TotalScore>();
        health = 100;
        LengthofTime = 1f;
        HeightValue = 1f;
        HealthCountdown();
        PlayerNavMeshAgent = GameObject.Find("Player").GetComponent<NavMeshAgent>();
        Invoke("MovementStopped2", 0.5f);
    }
    private void FixedUpdate()
    {
        if(InGame == false)
        {
            MovementBall.SetActive(false);
        }
        gameObject.transform.position = new Vector3(VRPlayer.transform.position.x, HeightValue , VRPlayer.transform.position.z);
        CrouchBox.transform.position = new Vector3(VRPlayer.transform.position.x, 1f, VRPlayer.transform.position.z);
        if (health < 1)
        {
            GameOver();
        }
        //stores the weapon packages
        GameObject swordpackage = null;
        swordpackage = GameObject.Find("Sword(Clone)");
        GameObject gunpackage = null;
        gunpackage = GameObject.Find("Gun(Clone)");
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
        if (other.gameObject.name == "Bullet" || other.gameObject.name == "Bullet(Clone)")
        {
            health -= 10;

            Destroy(other.gameObject);
            Haptics(1f,10f,1f,SteamVR_Input_Sources.RightHand);
            Haptics(1f, 10f, 1f, SteamVR_Input_Sources.LeftHand);
        }
    }

    //Checks the player isn't touching the walls or they take damage
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Walls")
        {
            health -= 5;
            Haptics(1f, 10f, 1f, SteamVR_Input_Sources.RightHand);
            Haptics(1f, 10f, 1f, SteamVR_Input_Sources.LeftHand);
        }
    }

    //Health Decreases overtime during the game
    private void HealthCountdown()
    {
        LengthofTime = 1 + (totalScore.Points/1000);
        if (health > 100)
        {
            totalScore.Points += health % 100;
            health = 100;
        }
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
        sword = null;
        try
        {
            sword = GameObject.Find("Sword(Clone)").GetComponent<Sword>();
        }
        catch(NullReferenceException)
        {
            sword = null;
        }
        sampleGun = null;
        try
        {
            sampleGun = GameObject.Find("Gun(Clone)").GetComponent<SampleGun>();
        }
        catch
        {
            sampleGun = null;
        }
        

        if(sword != null)
        {
            sword.SwordPickup.SendMessage("TakeBackItem", sword.interactable.attachedToHand);
        }
            
        
        if(sampleGun != null)
        {
            sampleGun.GunPickup.SendMessage("TakeBackItem", sampleGun.interactable.attachedToHand);
        }
 
        
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

    private void MovementStopped2()
    {
        MovementBall.SetActive(PlayerNavMeshAgent.hasPath);
        
            Quaternion direction = Quaternion.LookRotation(new Vector3(PlayerNavMeshAgent.velocity.normalized.x, 0, PlayerNavMeshAgent.velocity.normalized.z));
        
        if (MovementBall.transform.rotation != direction)
        {
            MovementBall.transform.rotation = direction;
        }

        
        Invoke("MovementStopped2", 0.01f);
    }
}
