using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchTrigger : MonoBehaviour
{
    //checks if the player starts ducking
    private void OnTriggerEnter(Collider other)
    {
        if (other == GameObject.Find("HeadCollider"))
        {
            GameEvents.currentEvent.OnDuckTriggerEnter();
        }
    }
    //checks if the player finishes ducking
    private void OnTriggerExit(Collider other)
    {
        if (other == GameObject.Find("HeadCollider"))
        {
            GameEvents.currentEvent.OnDuckTriggerEnd();
        }
    }
}
