using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Sword : MonoBehaviour
{
    public SteamVR_Action_Boolean shieldAction;
    public SteamVR_Action_Boolean unequip;
    private Interactable interactable;
    private GameObject SwordPickup;

    private SteamVR_Input_Sources hand;

    [SerializeField] private GameObject Shield;

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
                SwordPickup.SendMessage("TakeBackItem", handequipment);
            }
        }
    }

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
