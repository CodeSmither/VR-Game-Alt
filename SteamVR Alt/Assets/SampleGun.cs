using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SampleGun : MonoBehaviour
{
    public SteamVR_Action_Boolean fireAction;

    private SteamVR_Input_Sources hand;
    private Interactable interactable;
    float timeBetweenShots = 1;
    public float timeTilNextShot = 0;
    [SerializeField]private Transform barrel;
    [SerializeField]private GameObject bulletprefab;


    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        bool actFire;

        if (interactable.attachedToHand)
        {
            //get the hand attached to the interactable
            hand = interactable.attachedToHand.handType;
            //get teh state of the action prescribed for fire above
            actFire = fireAction.GetState(hand);
            Debug.Log("Fire Action" + actFire);
            if (actFire)
            {
                if (Time.time>=timeTilNextShot)
                {
                    GameObject bullet = Instantiate(bulletprefab, barrel.position, Quaternion.Euler(gameObject.transform.forward)); 
                    if (bullet != null)
                    {
                        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 10f);
                        timeTilNextShot = Time.time + timeBetweenShots;
                    }
                }
            }
        }
    }

   
}
