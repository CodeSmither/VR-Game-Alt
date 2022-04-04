using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.UI;

public class SampleGun : MonoBehaviour
{
    public SteamVR_Action_Boolean fireAction;
    public SteamVR_Action_Boolean dropAction;
    // Actions which occur when a bind is pressed in VR
    private SteamVR_Input_Sources hand;
    // The Hand which the object is attached to
    public Interactable interactable;
    // The Interactable form of the gun
    float timeBetweenShots = 0.2f;
    // The Gun has a time between shots to prevent spamming of shots causing bullets to bounce of each other
    public float timeTilNextShot = 0;
    // float used to check if the time needed for the next shot has occured
    private int ammo;
    public Text ammoremaining;
    // ammo stores the remaining ammo as an interger for the gun
    [SerializeField]private Transform barrel;
    // stores where the barrel of the gun is to fire out of for bullet instantiation
    [SerializeField]private GameObject barrelObj;
    // stores the barrel as a game object
    [SerializeField]private GameObject bulletprefab;
    // stores the bullet prefab for instatiation
    [SerializeField] private AudioSource GunClick;
    // stores the sound for the gun 
    public GameObject GunPickup;
    // stores the Gun Object it's self

    
    void Awake()
    {
        ammo = 24;
        // Sets the Ammo to 24 the reason is because the gun is inherintly stronger than the sword despite doing less damage
        // By Forcing the player to need ammo it leaves them far more vunrable to attacks.
        interactable = GetComponent<Interactable>();
        GunPickup = GameObject.Find("GunPickup");
        
    }

    // Update is called once per frame
    void Update()
    {
        bool actFire;
        bool actDrop;
        // stores if the player is doing either of the actions by pressing the buttons
        timeTilNextShot += Time.deltaTime;
        if (timeTilNextShot > timeBetweenShots)
        {
            timeTilNextShot = timeBetweenShots;
        }
        // Checks if the player has shot the gun yet

        if (interactable.attachedToHand)
        {
            //get the hand attached to the interactable
            hand = interactable.attachedToHand.handType;
            Hand handequipment = interactable.attachedToHand;
            //get teh state of the action prescribed for fire above
            actFire = fireAction.GetState(hand);
            actDrop = dropAction.GetState(hand);
            // checks if the gun is being fired and for which hand it is being fired on so it functions on both hands
            if (actFire)
            {
                if (timeTilNextShot == timeBetweenShots && ammo > 0)
                {
                    GameObject bullet = Instantiate(bulletprefab, barrel.position, Quaternion.LookRotation(barrelObj.transform.forward));
                    ammo--;
                    if (bullet != null)
                    {
                        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 1000f);
                        timeTilNextShot = 0;
                    }
                }
                else if(ammo == 0)
                {
                    GunClick.Play();
                }
            }
            // This checks if a player has ammo and is able to fire and then applies the fire sound
            if (actDrop)
            {
                    GunPickup.SendMessage("TakeBackItem", handequipment);
            }
        }


        ammoremaining.text = ammo.ToString() + " Bullets remaining";
    }

   
}
