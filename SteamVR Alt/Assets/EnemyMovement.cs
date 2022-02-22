using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent EnemyAgent;
    [SerializeField] private NavMeshPath EnemyPathing;
    [SerializeField] private GameObject[] MovementPoints;
    [SerializeField] private int objectiveNumber = -1;
    [SerializeField] private bool cooldown;
    [SerializeField] private bool alertMode;
    [SerializeField] private Vector3 wanderpoint1;
    [SerializeField] private Vector3 wanderpoint2;
    [SerializeField] private Vector3 wanderpoint3;
    [SerializeField] private bool Reached1;
    [SerializeField] private bool Reached2;
    [SerializeField] private bool Reached3;
    [SerializeField] private bool gatheredPoints;
    private EnemyHealth enemyHealth;

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
    void Awake()
    {
        EnemyAgent = gameObject.GetComponent<NavMeshAgent>();
        enemyHealth = gameObject.GetComponent<EnemyHealth>();
        Reached1 = false;
        Reached2 = false;
        Reached3 = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (cooldown == false)
        {
            EnemyAgent.speed = 2f;
            if (objectiveNumber == -1)
            {
                objectiveNumber = Random.Range(0, MovementPoints.Length);

            }
            else if (1.1f < Vector3.Distance(gameObject.transform.position, MovementPoints[objectiveNumber].transform.position))
            {
                EnemyAgent.SetDestination(MovementPoints[objectiveNumber].transform.position);
            }
            else if (1.1f >= Vector3.Distance(gameObject.transform.position, MovementPoints[objectiveNumber].transform.position))
            {
                objectiveNumber = -1;
                Cooldown = true;
                gatheredPoints = false;
            }
            
        }
        if (cooldown == true)
        {
            EnemyAgent.speed = 1.1f;
            if (gatheredPoints == false)
            {
                Wandering();
            }
            else if(gatheredPoints == true)
            {
                if (Reached1 == false)
                {
                    if (1.1f < Vector3.Distance(gameObject.transform.position, wanderpoint1))
                    {
                        EnemyAgent.SetDestination(wanderpoint1);
                    }
                    else if (1.1f > Vector3.Distance(gameObject.transform.position, wanderpoint1))
                    {
                        Reached1 = true;
                    }
                }
                if (Reached2 == false && Reached1 == true)
                {
                    if (1.1f < Vector3.Distance(gameObject.transform.position, wanderpoint2))
                    {
                        EnemyAgent.SetDestination(wanderpoint2);
                    }
                    else if (1.1f > Vector3.Distance(gameObject.transform.position, wanderpoint2))
                    {
                        Reached2 = true;
                    }
                }
                if (Reached3 == false && Reached1 == true && Reached2 == true)
                {
                    if (1.1f < Vector3.Distance(gameObject.transform.position, wanderpoint3))
                    {
                        EnemyAgent.SetDestination(wanderpoint3);
                    }
                    else if (1.1f > Vector3.Distance(gameObject.transform.position, wanderpoint3))
                    {
                        Reached3 = true;
                    }
                }
                else if (Reached1 == true && Reached2 == true && Reached3 == true)
                {
                    Reached1 = false;
                    Reached2 = false;
                    Reached3 = false;
                }
                
            }
        }
        if (0 != EnemyAgent.velocity.normalized.magnitude)
        {
            gameObject.transform.rotation = Quaternion.LookRotation(EnemyAgent.velocity);
        }
    }

    IEnumerator Cooldown2Norm()
    {
        yield return new WaitForSeconds(5f);
        cooldown = false;
    }

    private void Wandering()
    {
        gatheredPoints = true;
        wanderpoint1 = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z + 1);
        wanderpoint2 = new Vector3(gameObject.transform.position.x - 1, 0, gameObject.transform.position.z - 1);
        wanderpoint3 = new Vector3(gameObject.transform.position.x + 1, 0, gameObject.transform.position.z - 1);
    }


    private void SearchingState()
    {
        // 2 second delay between walking and being found in alert mode
        if (true)
        {
            alertMode = true;
        }
        
    }

    private void AlertMode()
    {

        // chase after target
    }

    private void FleeingMode()
    {
        if(enemyHealth.health < 30)
        {

        }
        // If health is below a certain amount run away
    }

    private void OpenFire()
    {
        // If in alertmode and target is in site open fire
    }

    private void FindGroupMode()
    {
        // If health is below a certain amount and has fled from target find an enemy to team up with
    }

    private void FollowGroupMode()
    {
        // If health is still below a certain amount stay with group
    }

    private void BreakOffMode()
    {
        // If health has recovered a certain amount stay with group
    }

}
