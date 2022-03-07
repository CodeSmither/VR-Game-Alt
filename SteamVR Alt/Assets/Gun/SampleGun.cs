using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SampleGun : MonoBehaviour
{
    public SteamVR_Action_Boolean fireAction;
    public SteamVR_Action_Boolean dropAction;

    private SteamVR_Input_Sources hand;
    private Interactable interactable;
    float timeBetweenShots = 1;
    public float timeTilNextShot = 0;
    private int ammo;
    [SerializeField]private Transform barrel;
    [SerializeField]private GameObject barrelObj;
    [SerializeField]private GameObject bulletprefab;
    [SerializeField] private AudioSource GunClick;


    // Start is called before the first frame update
    void Awake()
    {
        ammo = 12;
        interactable = GetComponent<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        bool actFire;
        bool actDrop;

        if (interactable.attachedToHand)
        {
            //get the hand attached to the interactable
            hand = interactable.attachedToHand.handType;
            //get teh state of the action prescribed for fire above
            actFire = fireAction.GetState(hand);
            actDrop = dropAction.GetState(hand);
            if (actFire)
            {
                if (Time.time>=timeTilNextShot && ammo > 0)
                {
                    GameObject bullet = Instantiate(bulletprefab, barrel.position, Quaternion.LookRotation(barrelObj.transform.forward));
                    if (bullet != null)
                    {
                        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 1000f);
                        timeTilNextShot = Time.time + timeBetweenShots;
                    }
                }
                else if(ammo == 0)
                {
                    GunClick.Play();
                }
            }
            if (actDrop)
            {
                
            }
        }
       
    }

   
}
