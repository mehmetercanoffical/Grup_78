using System;
using UnityEngine;
namespace AttackSystem
{
    public class Archer : MonoBehaviour
    {
        public float MaxDistance = 1000f;
        public GameObject Arrow;
        public Transform CreatArrowPoint;
        public Transform ArrowOnHand;
        public Transform ArrowFirePoint;
        public float attackSpeed = 25f;
        public LayerMask maskEnemy;

        public GameObject arrow;


        private void Start() => CreatArrow();

        public void CreatArrow()
        {
            arrow = Instantiate(Arrow, CreatArrowPoint.position, CreatArrowPoint.rotation, CreatArrowPoint);
            LocalZero();

        }

        public void Shoot()
        {

            if (arrow == null) return;

            RaycastHit hit;

            Ray ray = ThirdPersonCamera.Instance.mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            if (Physics.Raycast(ray, out hit, MaxDistance, maskEnemy))
            {

                arrow.GetComponent<Arrow>().Shoot((hit.point - ArrowFirePoint.position), attackSpeed);

                arrow.transform.SetParent(null);
            }

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
            Destroy(arrow);
            arrow = null;
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(ArrowFirePoint.position, ArrowFirePoint.forward * MaxDistance);
        }
    }
}
