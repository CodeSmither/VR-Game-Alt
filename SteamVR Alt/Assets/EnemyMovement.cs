using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent EnemyAgent;
    [SerializeField] private GameObject[] MovementPoints;
    [SerializeField] private int objectiveNumber = -1;
    [SerializeField]private bool cooldown;

    public bool Cooldown
    {
        get { return cooldown; }
        set
        {
            cooldown = value;
            StartCoroutine(Cooldown2Norm());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        EnemyAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (cooldown == false)
        {
            if (objectiveNumber == -1)
            {
                objectiveNumber = Random.Range(0, MovementPoints.Length);

            }
            else if (0.6f < Vector3.Distance(gameObject.transform.position, MovementPoints[objectiveNumber].transform.position))
            {
                EnemyAgent.SetDestination(MovementPoints[objectiveNumber].transform.position);
            }
            else if (0.6f >= Vector3.Distance(gameObject.transform.position, MovementPoints[objectiveNumber].transform.position))
            {
                objectiveNumber = -1;
                Cooldown = true;
            }
            if (0 != EnemyAgent.velocity.normalized.magnitude)
            {
                gameObject.transform.rotation = Quaternion.LookRotation(EnemyAgent.velocity.normalized);
            }
        }
    }

    IEnumerator Cooldown2Norm()
    {
        Debug.Log("I have been called");
        yield return new WaitForSeconds(3f);
        cooldown = false;
    }



}
