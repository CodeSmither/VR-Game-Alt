using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchTrigger : MonoBehaviour
{
    //checks if the player starts ducking
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "HeadCollider")
        {
            GameEvents.currentEvent.OnDuckTriggerEnter();
        }
    }
    //checks if the player finishes ducking
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "HeadCollider")
        {
            GameEvents.currentEvent.OnDuckTriggerEnd();
        }
    }
}
