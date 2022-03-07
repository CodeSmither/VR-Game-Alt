using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Sword : MonoBehaviour
{
    public SteamVR_Action_Boolean shieldAction;

    private Interactable interactable;

    private SteamVR_Input_Sources hand;

    [SerializeField] private GameObject Shield;

    private bool cooldown;

    private void Awake()
    {
        Shield.SetActive(false);
        interactable = GetComponent<Interactable>();
    }

    private void Update()
    {
        bool ShieldAct;

        if (interactable.attachedToHand)
        {
            hand = interactable.attachedToHand.handType;
            ShieldAct = shieldAction.GetState(hand);

            if (ShieldAct && cooldown == false)
            {
                StartCoroutine(ShieldActivation());
            }
        }
    }

    IEnumerator ShieldActivation()
    {
        Shield.SetActive(true);
        cooldown = true;
        yield return new WaitForSeconds(0.5f);
        Shield.SetActive(false);
        yield return new WaitForSeconds(5f);
        cooldown = true;
    }
}
