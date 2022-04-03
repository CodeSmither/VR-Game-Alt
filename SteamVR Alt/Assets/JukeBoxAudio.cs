using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class JukeBoxAudio : MonoBehaviour
{
    [SerializeField]AudioSource Jukebox;
    [SerializeField] AudioClip[] Audioclips;
    [SerializeField] private static float volume = 0.25f;

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
    public void SwapTrack()
    {
            int RandomNumber = Random.Range(0, Audioclips.Length);
            AudioClip Thisclip = Audioclips[RandomNumber];
            Jukebox.clip = Thisclip;
            Jukebox.Play();
            NowPlaying();
    }

    public void FixedUpdate()
    {
        if (Jukebox.isPlaying == false)
        {
            SwapTrack();
        }
        Jukebox.volume = Volume;
    }
    private void NowPlaying()
    {

    }

    public void SetTrack(int DesiredTrack)
    {
        AudioClip Thisclip = Audioclips[DesiredTrack];
        Jukebox.Stop();
        Jukebox.clip = Thisclip;
        Jukebox.Play();
    }
}
