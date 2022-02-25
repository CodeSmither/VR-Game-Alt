using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

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
    [SerializeField] private GameObject bullet;
<<<<<<< Updated upstream
=======
    [SerializeField] private Transform gunPoint;
    [SerializeField] private GameObject PlayerHitBox; 
    [SerializeField] private int BulletSpead;
>>>>>>> Stashed changes
    private EnemyHealth enemyHealth;
    private bool targetInSight;
    private bool partOfGroup;
    private Transform Player;
    [SerializeField]private LayerMask Raycastable;
    private Vector3 FleeingPoint;
    private bool Firing;
    private bool firingCooldown;

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
        targetInSight = false;
    }


    void FixedUpdate()
    {
        Player = GameObject.Find("Player").transform;
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position + Vector3.up + Vector3.forward,Player.position - transform.position, out hit, Mathf.Infinity, Raycastable))
        {
            Debug.DrawRay(transform.position + Vector3.up, Player.position - transform.position, Color.red, 0, true);
            if (hit.collider.gameObject != null)
            {
<<<<<<< Updated upstream
                
                if (hit.collider.gameObject.tag == "Player")
=======
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.gameObject.tag == "VRPlayer")
>>>>>>> Stashed changes
                {
                    targetInSight = true;
                    Debug.Log("I am Hitting a Player");
                }
<<<<<<< Updated upstream
                else if (hit.collider.gameObject.tag == "Walls")
                {
                    targetInSight = false;
                    
                }
                else
=======
                if (hit.collider.gameObject.tag == "Walls")
>>>>>>> Stashed changes
                {
                    targetInSight = false;
                    
                }
                
            }
        }
        
        

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

            if (partOfGroup == true)
            {
                if (enemyHealth.health < 30)
                {
                    FollowGroupMode();
                }
                else if (enemyHealth.health > 30)
                {
                    BreakOffMode();
                }
            }
            else if (gatheredPoints == false && targetInSight == false && partOfGroup == false)
            {
                Wandering();
            }
            else if(gatheredPoints == true && targetInSight == false && partOfGroup == false)
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
            else if (targetInSight == true)
            {
                AlertMode();
            }
        }
<<<<<<< Updated upstream
        if (0 != EnemyAgent.velocity.normalized.magnitude)
=======
        if (targetInSight == true || Firing == true)
        {
            AlertMode();
            EnemyAgent.speed = 3f;
        }
        
        if (targetInSight == true)
        {
            gameObject.transform.rotation = Quaternion.LookRotation(Player.position - transform.position);
        }
        else if (0 != EnemyAgent.velocity.normalized.magnitude && targetInSight == false)
>>>>>>> Stashed changes
        {
            gameObject.transform.rotation = Quaternion.LookRotation(EnemyAgent.velocity);
        }
        

        if(Firing == true)
        {
            OpenFire();
            
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
        if (enemyHealth.health > 30)
        {
<<<<<<< Updated upstream
            if (enemyHealth.health > 30)
            {
                StartCoroutine(FireandFollow());
            }
            else if (enemyHealth.health < 30)
            {
                FleeingMode();
            }
=======
            StartCoroutine(FireandFollow());

        }
        if (targetInSight == true && enemyHealth.health < 30)
        {
            FleeingMode();
>>>>>>> Stashed changes
        }
        else if (targetInSight == false)
        {
            if (partOfGroup == false && enemyHealth.health > 30)
            {
                FindGroupMode();
            }
             
        }
        
        // chase after target
    }

    IEnumerator FireandFollow()
    {
        
        if (targetInSight == true)
        {
            Firing = true;
        }
        if (targetInSight == false)
        {
            
            yield return new WaitForSeconds(5f);
            Firing = false;
        }
    }

    private void FleeingMode()
    {

        // If health is below a certain amount run away
        MovementPoints = MovementPoints.OrderBy(go => go.GetComponent<WayPoints>().distanceToEnemy(transform.position)).ToArray();
        foreach(GameObject Points in MovementPoints)
        {
            RaycastHit hit;
            if(Physics.Raycast(Points.transform.position + Vector3.up, Player.position - Points.transform.position, out hit, 40f, Raycastable) && hit.collider.gameObject.tag != "Player")
            {
                FleeingPoint = Points.transform.position;
                break;
            }
        }
        EnemyAgent.SetDestination(FleeingPoint);
    }

    private void OpenFire()
    {
        
        if (3f <= Vector3.Distance(gameObject.transform.position,Player.position))
        {
            Vector3 BackPoint = -((new Vector3 (Player.position.x,0,Player.position.z) - new Vector3 (gameObject.transform.position.x,0,gameObject.transform.position.z)).normalized * 5f);
            EnemyAgent.SetDestination(BackPoint);
            
        }
        if (Vector3.Distance(gameObject.transform.position, Player.position) >= 6f)
        {
            MovementPoints = MovementPoints.OrderBy(go => go.GetComponent<WayPoints>().distanceToPlayer(Player.position)).ToArray();
            EnemyAgent.SetDestination(MovementPoints[0].transform.position);
            
        }

        if (firingCooldown == false)
        {
<<<<<<< Updated upstream
            GameObject Bullet = Instantiate(bullet, gameObject.transform.position + Vector3.forward, Quaternion.Euler(gameObject.transform.forward));
            Bullet.GetComponent<Rigidbody>().AddForce(Bullet.transform.forward * 10f);

            firingCooldown = true;
=======
           
            if (Vector3.Dot(transform.forward,(Player.position - transform.position).normalized) > 0.95f && Vector3.Dot(transform.forward, (Player.position - transform.position).normalized) < 1.05f)
            {
                StartCoroutine(Reload());
                for (int x = 0; x < 6; x++)
                {
                    StartCoroutine(BulletShot(0.1f * x));
                    if (x == 3)
                    {
                        firingCooldown = true;
                    }
                }
                StartCoroutine(Reload());
                
            }
>>>>>>> Stashed changes
        }
        // If in alertmode and target is in site open fire
    }

<<<<<<< Updated upstream
=======
    IEnumerator BulletShot(float waitTime)
    {
        float BulletSpreadx = Random.Range(-BulletSpead, BulletSpead);
        float BulletSpready = Random.Range(-BulletSpead, BulletSpead);
        Quaternion InaccuracyModifier = Quaternion.Euler(BulletSpreadx,BulletSpready,0f);
        Quaternion OnTargetbulletRotation = Quaternion.LookRotation(PlayerHitBox.transform.position - transform.position);
        Quaternion bulletRotation = OnTargetbulletRotation * InaccuracyModifier;
        yield return new WaitForSeconds(waitTime);
        GameObject Bullet = Instantiate(bullet,gunPoint.position, bulletRotation);
        Bullet.name = "Bullet";
        Bullet.GetComponent<Rigidbody>().AddForce(Bullet.transform.forward * 1000f);
    }
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(5f);
        firingCooldown = false;
    }

>>>>>>> Stashed changes
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
