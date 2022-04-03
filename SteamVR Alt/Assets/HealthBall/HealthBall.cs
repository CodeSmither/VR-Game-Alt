using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HealthBall : MonoBehaviour
{
    [SerializeField]PlayerHealth playerHealth;
    [SerializeField] private MovementPointStore movementPointStore;
    [SerializeField] GameObject[] allWayPoints;
    [SerializeField]WayPoints wayPoint;

    private void Awake()
    {
        // estabilishes all the points and orders them based on distance
        playerHealth = GameObject.Find("PlayerHitBox").GetComponent<PlayerHealth>();
        movementPointStore = GameObject.Find("TeleportPoints").GetComponent<MovementPointStore>();
        for (int x = 0; x < 31 + 1; x++)
        {
            allWayPoints[x] = movementPointStore.MovementPoints[x];
        }
        allWayPoints = allWayPoints.OrderBy(go => go.GetComponent<WayPoints>().distanceToHealthBall(gameObject.transform.position)).ToArray();

        Invoke("AutoDestruct",5f);
    }

    private void FixedUpdate()
    {
        // moves the healthball to the closest point
        float speed = 1f;
        transform.position = Vector3.Lerp(transform.position, allWayPoints[0].transform.position, Time.deltaTime * speed);
        
    }
    private void AutoDestruct()
    {
        // destroys the object but gives the player a little health after some time
        playerHealth.Health += 10;
        Destroy(gameObject);
    }

    // checks if the player has stood on the health ball and if they have destroys it and gives them some health
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6 && other.gameObject.tag == "VRPlayer")
        {
            playerHealth.Health += 40;
            Destroy(gameObject);
        }
        //if (other.gameObject != null)
        //{
        //    Debug.Log(other.gameObject.name);
        // }
    }
}
