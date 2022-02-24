using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    public float distanceToEnemy(Vector3 enemyposition)
    {
        float enemyDistance = Vector3.Distance(enemyposition, transform.position);
        return enemyDistance;
    }
    public float distanceToPlayer(Vector3 playerposition)
    {
        float playerDistance = Vector3.Distance(playerposition, transform.position);
        return playerDistance;
    }
}
