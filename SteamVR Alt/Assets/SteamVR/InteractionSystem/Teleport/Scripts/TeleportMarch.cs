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
        }
    }
}

