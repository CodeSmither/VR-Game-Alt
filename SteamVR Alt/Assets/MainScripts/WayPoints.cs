using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    // checks a waypoints distance from the enemy,player or a health ball to be called on by other scripts
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
    public float distanceToHealthBall(Vector3 ballposition)
    {
        float ballDistance = Vector3.Distance(ballposition, transform.position);
        return ballDistance;
    }
}
