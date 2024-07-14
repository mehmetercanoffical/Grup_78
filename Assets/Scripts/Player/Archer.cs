using System;
using UnityEngine;
namespace AttackSystem
{
    public class Archer : MonoBehaviour
    {
        public float attackSpeed = 25f;
        public float MaxDistance = 1000f;
        public GameObject Arrow;
        public Transform CreatArrowPoint;
        public Transform ArrowOnHand;
        public Transform ArrowFirePoint;
        public LayerMask maskEnemy;

        internal GameObject arrow;


        public void CreatArrow()
        {
            arrow = Instantiate(Arrow, CreatArrowPoint.position, CreatArrowPoint.rotation, CreatArrowPoint);
            LocalZero();

        }

        public void Shoot()
        {

            if (arrow == null) return;

            RaycastHit hit;

            Vector3 rayPos = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            Ray ray = ThirdPersonCamera.Instance.mainCamera.ScreenPointToRay(rayPos);

            if (Physics.Raycast(ray, out hit, MaxDistance, maskEnemy))
            {
                Debug.Log("Ray " + hit.transform.gameObject.name);
                Vector3 hitAttackPoint = (hit.point - transform.position);
                hitAttackPoint.Normalize();

                Debug.DrawRay(transform.position, hitAttackPoint * MaxDistance, Color.blue, 10f);
                arrow.GetComponent<Arrow>().Shoot(hitAttackPoint, hitAttackPoint.magnitude * attackSpeed);
                arrow.transform.SetParent(null);
            }
            else
                DestroyArrow();

        }

        public void ChangeArrowOnHand()
        {
            if (arrow == null)
            {
                Debug.Log("Arrow is null On Hand");
                CreatArrow();
            }

            arrow.transform.SetParent(ArrowOnHand);

            LocalZero();

        }

        void LocalZero()
        {
            arrow.transform.localPosition = Vector3.zero;
            arrow.transform.localRotation = Quaternion.Euler(Vector3.zero);
        }

        internal void DestroyArrow()
        {
            if (arrow == null) return;
            Destroy(arrow);
            arrow = null;
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(ArrowFirePoint.position, ArrowFirePoint.forward * MaxDistance);
        }
    }
}
