using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class JukeBoxAudio : MonoBehaviour
{
    [SerializeField]AudioSource Jukebox;
    [SerializeField] AudioClip[] Audioclips;
    [SerializeField] private static float volume = 0.25f;
    PlayerHealth playerHealth;

    private void Awake()
    {
        //stores a reference of the playerhealth script to check if the player is in game or not
        playerHealth = GameObject.Find("PlayerHitBox").GetComponent<PlayerHealth>();
    }
    // stores the current volume of objects as a static so it can be used with all other audio sources
    public static float Volume
    {
        get
        {
            return volume;
        }
        set
        {
            volume = value;
        }
        
    }
    // changes the current track by randomly selecting one from the set list
    public void SwapTrack()
    {
            int RandomNumber = Random.Range(0, Audioclips.Length);
            AudioClip Thisclip = Audioclips[RandomNumber];
            Jukebox.clip = Thisclip;
            Jukebox.Play();
    }
    // checks if the player is in game and doesn't play sound unless they have
    public void FixedUpdate()
    {
        if (Jukebox.isPlaying == false && playerHealth.InGame == true)
        {
            SwapTrack();
        }
        else if (playerHealth.InGame == false)
        {
            Jukebox.Stop();
        }
        Jukebox.volume = Volume;
    }
    // ends the previous track and starts the new desired track
    public void SetTrack(int DesiredTrack)
    {
        AudioClip Thisclip = Audioclips[DesiredTrack];
        Jukebox.Stop();
        Jukebox.clip = Thisclip;
        Jukebox.Play();
    }
}
