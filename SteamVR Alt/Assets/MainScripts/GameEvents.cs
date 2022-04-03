using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{

    public static GameEvents currentEvent;

    private void Awake()
    {
        currentEvent = this;
    }
    // checks when the event has occured and if it does it activates duck start or duck end
    public event Action onDuckStart;
    public void OnDuckTriggerEnter()
    {
        if(onDuckStart != null)
        {
            onDuckStart();
        }
    }

    public event Action onDuckEnd;

    public void OnDuckTriggerEnd()
    {
        if(onDuckEnd != null)
        {
            onDuckEnd();
        }
    }
}
