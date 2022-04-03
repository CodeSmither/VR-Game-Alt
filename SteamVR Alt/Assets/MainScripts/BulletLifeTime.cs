using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLifeTime : MonoBehaviour
{
    AudioSource LazerNoise;
    // Stores bullet sound
    void Awake()
    {
        
        LazerNoise = gameObject.GetComponent<AudioSource>();
        LazerNoise.volume = JukeBoxAudio.Volume;
        LazerNoise.Play();
        StartCoroutine(EndOfLife());
        // activates the bullet sound as soon as it is created which has the same effect as shooting when clicking the fire button
    }
    IEnumerator EndOfLife()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
        // destroys a bullet after 5 seconds if it stays in the air too long before hitting a wall 
        // this stops the player being stuck in iniscable traps as often
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Walls")
        {
            Destroy(gameObject);
        }
        // destroys bullet preaturely if it hits a wall
    }
}
