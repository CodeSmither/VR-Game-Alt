using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using UnityEngine.AI;

namespace TeleportMarching
{
    public static class TeleportMarch
    {
        // Moves the player to their required destination based on which teleport point they teleport to
        public static void March(TeleportPoint teleportpoint)
        {
            NavMeshAgent PlayerNaveMeshAgent = GameObject.Find("Player").GetComponent<NavMeshAgent>();
            PlayerNaveMeshAgent.destination = teleportpoint.transform.position;
            
        }

    }
}

