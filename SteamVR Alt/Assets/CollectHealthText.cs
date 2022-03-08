using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectHealthText : MonoBehaviour
{
    public Text GloveText;
    private int health;
    

    private void FixedUpdate()
    {
        health = GameObject.Find("PlayerHitBox").GetComponent<PlayerHealth>().Health;
        GloveText.text = "Health: " + health.ToString() + "%";
    }
}
