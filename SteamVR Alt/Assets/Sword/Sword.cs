using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Sword : MonoBehaviour
{
    // stores reference to the binds
    public SteamVR_Action_Boolean shieldAction;
    public SteamVR_Action_Boolean unequip;
    // stores the interactable object as a reference
    private Interactable interactable;
    private GameObject SwordPickup;
    // stores the hand the object is held in
    private SteamVR_Input_Sources hand;
    // stores the shield object for the trigger event
    [SerializeField] private GameObject Shield;
    // stores the cooldown time on the shield
    private bool cooldown;

    private void Awake()
    {
        Shield.SetActive(false);
        interactable = GetComponent<Interactable>();
        SwordPickup = GameObject.Find("SwordPickup");
    }

    private void Update()
    {
        bool ShieldAct;
        bool UnequipAct;
        // checks for the player pressing the unequip or shield actions then activates the respective events for each one
        if (interactable.attachedToHand)
        {
            hand = interactable.attachedToHand.handType;
            Hand handequipment = interactable.attachedToHand;
            ShieldAct = shieldAction.GetState(hand);
            UnequipAct = unequip.GetState(hand);

            if (ShieldAct && cooldown == false)
            {
                StartCoroutine(ShieldActivation());
            }
            if (UnequipAct)
            {
                // this sends a message to the itempackage script to remove the item from the players hand.
                SwordPickup.SendMessage("TakeBackItem", handequipment);
            }
        }
    }
    // this activates the shield for a short time to allow the player to block bullets
    IEnumerator ShieldActivation()
    {
        Shield.SetActive(true);
        cooldown = true;
        yield return new WaitForSeconds(0.5f);
        Shield.SetActive(false);
        yield return new WaitForSeconds(1f);
        cooldown = false;
    }
}
