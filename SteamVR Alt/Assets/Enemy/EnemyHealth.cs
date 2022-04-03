using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] GameObject healthOrb;
    [SerializeField] TotalScore totalScore;
    [SerializeField] Material EnemyMaterial;
    [SerializeField] Color storedColour;
    private int OrbsSpawned;

    private void Awake()
    {
        // sets health to one hundred and then notifies then gets references to the spawner so it doesn't have to call it again upon death
        health = 100;
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        totalScore = GameObject.Find("GameOverUI").GetComponent<TotalScore>();
        EnemyMaterial.color = Color.red;
    }
    private void OnTriggerEnter(Collider other)
    {
        // Checks if the enemy has been attacked by a bullet or a sword
        if (other.gameObject.name == "Bullet"|| other.gameObject.name == "Bullet(Clone)")
        {
            EnemyMaterial.color = Color.green;
            health -= 20;
            Invoke("BacktoNormal", 1f);
            Destroy(other.gameObject);
            
        }
        if (other.gameObject.CompareTag("Sword"))
        {
            EnemyMaterial.color = Color.green;
            health -= 50;
            Invoke("BacktoNormal", 1f);
        }
       
        
    }

    private void BacktoNormal()
    {
        EnemyMaterial.color = Color.red;
    }

    private void FixedUpdate()
    {
        //  Checks if the object has been killed and if it has spawned an orb for the player to collect
        if (health <= 0)
        {
            if (OrbsSpawned == 0)
            {
                GameObject HealthOrb = Instantiate(healthOrb, new Vector3(gameObject.transform.position.x, 0.3f, gameObject.transform.position.z), Quaternion.Euler(0, 0, 0));
                HealthOrb.name = "HealthOrb";
                OrbsSpawned++;
                enemySpawner.numberOfEnemies--;
                totalScore.Points += 10;
                Destroy(gameObject.transform.parent.gameObject);
            }
            
        }
    }
}
