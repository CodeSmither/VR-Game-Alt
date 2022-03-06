using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] MovementPoints;
    [SerializeField] private GameObject Enemy;
    public int numberOfEnemies;
    [SerializeField] private LayerMask layerMask;

    private void FixedUpdate()
    {
        if (numberOfEnemies < 3)
        {
            int RandomNumber= Random.Range(0, MovementPoints.Length);
            Collider[] SpaceInvaders = Physics.OverlapBox(MovementPoints[RandomNumber].transform.position, new Vector3(3f, 3f, 3f), MovementPoints[RandomNumber].transform.rotation, layerMask);
            if (SpaceInvaders.Length == 0)
            {
                GameObject NewEnemy = Instantiate(Enemy, MovementPoints[RandomNumber].transform.position, MovementPoints[RandomNumber].transform.rotation);
                numberOfEnemies++;
            }
        }
    }
}
