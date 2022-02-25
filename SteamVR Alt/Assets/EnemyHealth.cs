using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : EnemyMovement
{
<<<<<<< Updated upstream
    protected internal int health;
=======
    public int health;
    [SerializeField] EnemySpawner enemySpawner;

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
        if(other.gameObject.name == "Sword")
        {
            health -= 50;
        }
    }
    private void FixedUpdate()
    {
        if(health <= 0)
        {
            Destroy(gameObject.transform.parent.gameObject);
            enemySpawner.numberOfEnemies--;
        }
    }
>>>>>>> Stashed changes
}
