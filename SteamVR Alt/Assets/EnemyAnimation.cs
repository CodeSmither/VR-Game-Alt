using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private EnemyMovement enemyMovement;
    private GameObject EnemyAvatar;
    private Animator EnemyAnimator;

    private void Awake()
    {
        EnemyMovement enemyMovement = gameObject.GetComponent<EnemyMovement>();
        EnemyAnimator = gameObject.transform.GetChild(1).GetComponent<Animator>();
        EnemyAvatar = gameObject.transform.GetChild(1).gameObject;
    }

    private void FixedUpdate()
    {
        
    }
}
