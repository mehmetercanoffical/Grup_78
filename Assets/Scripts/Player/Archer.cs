using UnityEngine;

public class Archer : MonoBehaviour
{
    public GameObject Arrow;
    public Transform CreatArrowPoint;
    public Transform ArrowOnHand;
    public Transform ArrowFirePoint;

    private GameObject arrow;
    public float attackSpeed = 25f;


    private void Start() => CreatArrow();

    public void CreatArrow()
    {
        arrow = Instantiate(Arrow, CreatArrowPoint.position, CreatArrowPoint.rotation, CreatArrowPoint);
        LocalZero();

    }

    public void Shoot()
    {

        if (arrow == null) return;

        arrow.GetComponent<Arrow>().Shoot(transform,Vector3.forward, attackSpeed);
        arrow.transform.SetParent(null);

    }

    public void ChangeArrowOnHand()
    {
        if (arrow == null)
            CreatArrow();

        arrow.transform.SetParent(ArrowOnHand); 

        LocalZero();

    }

    void LocalZero()
    {
        arrow.transform.localPosition = Vector3.zero;
        arrow.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }
}
