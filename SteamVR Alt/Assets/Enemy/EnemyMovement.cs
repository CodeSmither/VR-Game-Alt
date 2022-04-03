using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class EnemyMovement : MonoBehaviour
{
    protected internal NavMeshAgent EnemyAgent;
    [SerializeField] private NavMeshPath EnemyPathing;
    public GameObject[] MovementPoints;
    [SerializeField] private int objectiveNumber = -1;
    [SerializeField] private bool cooldown;
    [SerializeField] private bool alertMode;
    // Stores the 3 points a enemy walks to before walking to the next point
    [SerializeField] private Vector3 wanderpoint1;
    [SerializeField] private Vector3 wanderpoint2;
    [SerializeField] private Vector3 wanderpoint3;
    // Checks if the player has reached the 3 points
    [SerializeField] private bool Reached1;
    [SerializeField] private bool Reached2;
    [SerializeField] private bool Reached3;
    // checks if all the movement points have been gathered
    [SerializeField] private bool gatheredPoints;
    // stores the bullet needed for shooting
    [SerializeField] private GameObject bullet;
    // stores the Transform for the barrel of the gun
    [SerializeField] private Transform gunPoint;
    // stores the target for the enemies to hit when seen
    [SerializeField] private GameObject PlayerHitBox; 
    // stores the randomness of bullet firing
    [SerializeField] private int BulletSpead;
    [SerializeField] private MovementPointStore movementPointStore;
    private EnemyHealth enemyHealth;
    // checks if the player is site
    [SerializeField]protected internal bool targetInSight;
    // checks if the enemy is part of a group
    private bool partOfGroup;
    private Transform Player;
    //stores the raycasting layer
    [SerializeField] private LayerMask Raycastable;
    private Vector3 FleeingPoint;
    [SerializeField] protected internal bool Firing;
    [SerializeField]protected internal bool firingCooldown;

    public bool Cooldown
    {
        // collects the cooldown value and if it is turned on turns it off after a few seconds using a property
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
        movementPointStore = GameObject.Find("TeleportPoints").GetComponent<MovementPointStore>();
        EnemyAgent = gameObject.GetComponent<NavMeshAgent>();
        enemyHealth = gameObject.GetComponent<EnemyHealth>();
        Reached1 = false;
        Reached2 = false;
        Reached3 = false;
        targetInSight = false;
        BulletSpead = 12;
        PlayerHitBox = GameObject.Find("PlayerHitBox");
        // sets all of the movement points in the array to a value based on the corisponding point in the movementpointstore
        for (int x = 0; x < 31 + 1; x++)
        {
            MovementPoints[x] = movementPointStore.MovementPoints[x];
        }
    }


    void FixedUpdate()
    {
        Player = GameObject.Find("Player").transform;
        RaycastHit hit;
        // searches for the player
        if (Physics.Raycast(transform.position + (transform.forward/2), Player.position - transform.position, out hit, Mathf.Infinity, Raycastable))
        {
            Debug.DrawRay(transform.position +  (transform.forward/2), Player.position - transform.position, Color.red, 0, true);
            if (hit.collider.gameObject != null)
            {
                //Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.gameObject.tag == "VRPlayer")
                {
                    targetInSight = true;
                }

                if (hit.collider.gameObject.tag == "Walls")
                {
                    targetInSight = false;
                    
                }
                
            }
            
        }
        // moves the enemy into alert mode which is faster and attacking when it is both firing and the target is in sight
        if (targetInSight == true || Firing == true)
        {
            AlertMode();
            EnemyAgent.speed = 3f;
        }
        // sets the enemy to look at the player when it has spotted them
        if (targetInSight == true)
        {
            gameObject.transform.rotation = Quaternion.LookRotation(Player.position - transform.position);
        }
        else if (0 != EnemyAgent.velocity.normalized.magnitude && targetInSight == false) { Quaternion.LookRotation(new Vector3(EnemyAgent.velocity.normalized.x, 0, EnemyAgent.velocity.normalized.z)); }
        // allows the enemy to fire after target un sught has been enabled
        if (Firing == true)
        {

            OpenFire();

        }
        // Sets Enemy Moving speed to faster when moving normally and also decides the enemies objective
        if (cooldown == false && targetInSight == false)
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
        if (cooldown == true && targetInSight == false)
        {
            //Set Enemy to walking speed to slower when fleeing
            EnemyAgent.speed = 1.1f;
            //Grouping Feature
            if (partOfGroup == true)
            {
                if (enemyHealth.health < 30)
                {
                    //FollowGroupMode();
                }
                else if (enemyHealth.health > 30)
                {
                    //BreakOffMode();
                } 
        } 
            // allows the enemy to wander when the player isn't near it
            if (gatheredPoints == false && targetInSight == false && partOfGroup == false)
            {
                Wandering();
            }
            // checks movement between the movement points and the random points which a created from them
            else if (gatheredPoints == true && targetInSight == false && partOfGroup == false)
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
        
    }

    IEnumerator Cooldown2Norm()
    {
        //prevents overlapping commands in the firing commands
        yield return new WaitForSeconds(5f);
        cooldown = false;
    }

    private void Wandering()
    {
        // creates 3 points for the enemy to walk about in once reaching a set movement point
        gatheredPoints = true;
        wanderpoint1 = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z + 1);
        wanderpoint2 = new Vector3(gameObject.transform.position.x - 1, 0, gameObject.transform.position.z - 1);
        wanderpoint3 = new Vector3(gameObject.transform.position.x + 1, 0, gameObject.transform.position.z - 1);
    }

    private void AlertMode()
    {

        // causes enemy to flee if damaged or charge towards the player if not hurt
        if (enemyHealth.health > 30)
        {
            if (enemyHealth.health > 30)
            {
                StartCoroutine(FireandFollow(2.0f));
            }
            else if (enemyHealth.health < 30)
            {
                FleeingMode();
            }
        }
        if (targetInSight == true && enemyHealth.health < 30)
        {
            FleeingMode();
        }
        /*else if (targetInSight == false)
        {
            if (partOfGroup == false && enemyHealth.health > 30)
            {
                FindGroupMode();
            }
        }*/
        // chase after target
    }

    IEnumerator FireandFollow(float delayTime)
    {
        //Allow the enemy to fire after a few seconds of the player being is sight
        yield return new WaitForSeconds(delayTime);
        if (targetInSight == true)
        {
            Firing = true;
        }
        if (targetInSight == false)
        {
            yield return new WaitForSeconds(5f);
            if (targetInSight == false)
            {
                Firing = false;
            }
            
        }
    }

    private void FleeingMode()
    {

        // If enemy is damaged it will flee out of sight of the player by selecting a point which is just out of sight giving the player a chance to persue the enemy
        MovementPoints = MovementPoints.OrderBy(go => go.GetComponent<WayPoints>().distanceToEnemy(transform.position)).ToArray();
        foreach (GameObject Points in MovementPoints)
        {
            RaycastHit hit;
            if (Physics.Raycast(Points.transform.position + Vector3.up, Player.position - Points.transform.position, out hit, 40f, Raycastable) && hit.collider.gameObject.tag != "Player")
            {
                FleeingPoint = Points.transform.position;
                break;
            }
        }
        EnemyAgent.SetDestination(FleeingPoint);
    }

    private void OpenFire()
    {
        // checks if the player is in sight of the enemy and if they are moves them towards the player if they are too far and away if they are too close making melee combat more challanging

        //Debug.Log(Vector3.Distance(gameObject.transform.position, Player.position));
        if (3f >= Vector3.Distance(gameObject.transform.position, Player.position))
        {
            EnemyAgent.ResetPath();
            Vector3 BackPoint = -((new Vector3(Player.position.x, 0, Player.position.z) - new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z)).normalized * 5f);
            EnemyAgent.SetDestination(BackPoint);
            //Debug.Log("FallBack");
        }
        if (Vector3.Distance(gameObject.transform.position, Player.position) >= 6f)
        {
            EnemyAgent.ResetPath();
            MovementPoints = MovementPoints.OrderBy(go => go.GetComponent<WayPoints>().distanceToPlayer(Player.position)).ToArray();
            //Debug.Log(MovementPoints[0].transform.position);
            EnemyAgent.SetDestination(MovementPoints[0].transform.position);
            
        }

        if (firingCooldown == false)
        {
            // checks if the player is in the sight of the enemy and then fires
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

        }
        /*
        else if (firingCooldown == true)
        {
            
        }
        */
        // If in alertmode and target is in site open fire
    }

    //fires bullets by instantiating them and creating random bullet spread to choose which direction to fire them in
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

    /*
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
    */
}
