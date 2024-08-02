using System.Collections.Generic;
using UnityEngine;

public class AreaStart : MonoBehaviour
{
    public bool animStart;
    public float range;
    public Color color;



    private void Update()
    {
        if (!animStart) return;
        Collider[] soliderController = Physics.OverlapSphere(transform.position, range);
        foreach (var item in soliderController)
        {
            if (item.CompareTag("Enemy") && animStart)
            {
                Debug.Log("Attack");
                item.GetComponent<SoliderController>().AttackStart();
                animStart = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            Debug.Log("Start");
            animStart = true;
        }
           
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, Vector3.one * range);
    }
}
