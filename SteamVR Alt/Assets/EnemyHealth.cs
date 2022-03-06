using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] GameObject healthOrb;
    [SerializeField] TotalScore totalScore;

    private void Awake()
    {
        health = 100;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Bullet")
        {
            health -= 10;
            Destroy(other.gameObject);
        }
        if (other.gameObject.name == "Sword")
        {
            health -= 50;
        }
    }
    private void FixedUpdate()
    {
        if (health <= 0)
        {
            GameObject HealthOrb = Instantiate(healthOrb,new Vector3(gameObject.transform.position.x,0.3f,gameObject.transform.position.z),Quaternion.Euler(0,0,0));
            HealthOrb.name = "HealthOrb";
            enemySpawner.numberOfEnemies--;
            totalScore.Points += 10;
            Destroy(gameObject.transform.parent.gameObject);
            
        }
    }
}
