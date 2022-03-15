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
        ammo = 24;
        interactable = GetComponent<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        bool actFire;
        bool actDrop;
        timeTilNextShot += Time.deltaTime;
        if (timeTilNextShot > timeBetweenShots)
        {
            timeTilNextShot = timeBetweenShots;
        }

        if (interactable.attachedToHand)
        {
            //get the hand attached to the interactable
            hand = interactable.attachedToHand.handType;
            //get teh state of the action prescribed for fire above
            actFire = fireAction.GetState(hand);
            actDrop = dropAction.GetState(hand);
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
            if (actDrop)
            {
                
            }
        }
       
    }

   
}
