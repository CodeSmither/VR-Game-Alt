using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectHealthText : MonoBehaviour
{
    public Text GloveText;
    private int health;
    private AudioSource alarm;

    private void Awake()
    {
        // sets the alarm sound of the wrist
        alarm = gameObject.GetComponent<AudioSource>();
        alarm.volume = JukeBoxAudio.Volume;
    }
    //checks if the alarm is playing and keeps playing it if the player is on lower helth
    private void Onlowhealth()
    {
        if (alarm.isPlaying == false)
        {
            alarm.Play();
        }

        if (health < 30)
        {
            Invoke("Onlowhealth", 1f);
        }
    }
    // checks if the player is on low health and also stores the players current health on the wrist UI
    private void FixedUpdate()
    {
        
        health = GameObject.Find("PlayerHitBox").GetComponent<PlayerHealth>().Health;
        if (health < 30)
        {
            Onlowhealth();
        }
        GloveText.text = "Health: " + health.ToString() + "%";
    }

}
