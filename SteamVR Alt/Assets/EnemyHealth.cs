using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] GameObject healthOrb;
    [SerializeField] TotalScore totalScore;
    private int OrbsSpawned;

    private void Awake()
    {
        health = 100;
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        totalScore = GameObject.Find("GameOverUI").GetComponent<TotalScore>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Bullet"|| other.gameObject.name == "Bullet(Clone)")
        {
            health -= 20;
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Sword"))
        {
            health -= 50;
        }
       // if(other != null)
       // {
       //     Debug.Log(other.gameObject);
       //  }
        
    }
    private void FixedUpdate()
    {
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
