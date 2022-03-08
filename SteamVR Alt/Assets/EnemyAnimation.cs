using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private EnemyMovement enemyMovement;
    [SerializeField] private GameObject EnemyAvatar;
    [SerializeField]private Animator EnemyAnimator;
    private int EnemyHealthLevel;

    private void Awake()
    {
        enemyMovement = gameObject.GetComponent<EnemyMovement>();
        EnemyAnimator = gameObject.transform.GetChild(1).GetComponent<Animator>();
        EnemyAvatar = gameObject.transform.GetChild(1).gameObject;
        EnemyHealthLevel = gameObject.transform.GetComponent<EnemyHealth>().health;
    }

    private void FixedUpdate()
    {

        if (enemyMovement.EnemyAgent.velocity.magnitude >= 0.1f && enemyMovement.targetInSight == false)
        {
            EnemyAnimator.SetBool("Moving", true);
        }
        else if(enemyMovement.EnemyAgent.velocity.magnitude >= 0.1f)
        {
            EnemyAnimator.SetBool("Moving", false);
        }
        if (enemyMovement.firingCooldown == true)
        {
            EnemyAnimator.SetBool("Firing", true);
        }
        else if (enemyMovement.firingCooldown == false)
        {
            EnemyAnimator.SetBool("Firing", false);
        }
        if(enemyMovement.isFiring == true)
        {
            EnemyAnimator.SetBool("Cooldown", false);
        }
        else if(enemyMovement.isFiring == false)
        {
            EnemyAnimator.SetBool("Cooldown", true);
        }
        EnemyAnimator.SetInteger("Health", EnemyHealthLevel);
    }
}
