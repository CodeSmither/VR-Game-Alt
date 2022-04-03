using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //sets up two events 
        GameEvents.currentEvent.onDuckStart += OnDuckEnter;
        GameEvents.currentEvent.onDuckEnd += OnDuckExit;
    }
    // activates the effects of the two events 
    private void OnDuckEnter()
    {
        GameObject PlayerHitbox = GameObject.Find("PlayerHitBox");
        PlayerHealth playerhealth = PlayerHitbox.GetComponent<PlayerHealth>();
        playerhealth.HeightValue = 0f;
    }

    private void OnDuckExit()
    {
        GameObject PlayerHitbox = GameObject.Find("PlayerHitBox");
        PlayerHealth playerhealth = PlayerHitbox.GetComponent<PlayerHealth>();
        playerhealth.HeightValue = 1f;
    }
}
