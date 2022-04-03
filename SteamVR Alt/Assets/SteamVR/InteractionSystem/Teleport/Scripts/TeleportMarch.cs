using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using UnityEngine.AI;

namespace TeleportMarching
{
    public static class TeleportMarch
    {
        public static void March(TeleportPoint teleportpoint)
        {
            NavMeshAgent PlayerNaveMeshAgent = GameObject.Find("Player").GetComponent<NavMeshAgent>();
            PlayerNaveMeshAgent.destination = teleportpoint.transform.position;
            HasReached(PlayerNaveMeshAgent, teleportpoint);
        }

        public static void HasReached(NavMeshAgent PlayerNaveMeshAgent, TeleportPoint teleportpoint)
        {
            GameObject MovementBall = GameObject.Find("MovementBall"); 
            if (Vector3.Distance(PlayerNaveMeshAgent.destination, teleportpoint.transform.position) > 1f)
            {
                MovementBall.SetActive(true);
                MovementBall.transform.rotation = Quaternion.LookRotation(new Vector3(PlayerNaveMeshAgent.velocity.normalized.x, 0,PlayerNaveMeshAgent.velocity.normalized.z));
            }
        }
    }
}

