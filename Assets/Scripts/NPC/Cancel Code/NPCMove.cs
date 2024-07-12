using UnityEngine;
using UnityEngine.AI;
namespace NPCSpace
{
    public class NPCMove : MonoBehaviour
    {

        public NavMeshAgent agent;
        public NPCManager manager;
        public bool _dontMove = false;
        public float rotationSpeed;

        void Start()
        {
            agent.autoBraking = false;
            agent.updateRotation = false;
        }


        internal void GoToPos(Transform target)
        {
            RemainingDistance(manager.remainingDistance);

            if (_dontMove) return;
            if (target == null)
            {
                agent.destination = transform.position;   return;
            }

            if (!agent.pathPending)
                agent.destination = target.position;
        }

        internal void RemainingDistance(float dis) => agent.stoppingDistance = dis;

        internal void Stop(bool val) => agent.isStopped = val;

        internal void RotateToPlayer()
        {
            if (_dontMove) return;
            Vector3 direction = (manager._targetPlayer.position - transform.position).normalized;
            direction.y = 0;
            direction.Normalize();
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Ray ray = new(transform.position, new Vector3(0,0, manager.remainingDistance));
            Gizmos.DrawRay(ray);
        }
    }

    
}