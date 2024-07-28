using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathRoll : MonoBehaviour
{

    public Transform[] paths; 

    public Vector3 GetPointAt(float t)
    {
        int pointIndex = Mathf.FloorToInt(t * (paths.Length - 1));
        float lerpFactor = t * (paths.Length - 1) - pointIndex;

        if (pointIndex >= paths.Length - 1)
        {
            return paths[paths.Length - 1].position;
        }

        return Vector3.Lerp(paths[pointIndex].position, paths[pointIndex + 1].position, lerpFactor);
    }


}


//public List<Transform> paths = new List<Transform>();
//public float speed = 1;
//public float rotationSpeed = 1;
//public float reachDistance = 1.0f;
//public int currentPath = 0;


//private void Start()
//{
//    transform.position = paths[currentPath].position;
//    transform.rotation = paths[currentPath].rotation;
//}

//private void Update()
//{
//    float distance = Vector3.Distance(paths[currentPath].position, transform.position);

//    transform.position = Vector3.MoveTowards(transform.position, paths[currentPath].position, Time.deltaTime * speed);

//    transform.rotation = Quaternion.Slerp(transform.rotation, paths[currentPath].rotation, Time.deltaTime * rotationSpeed);

//    if (distance <= reachDistance)
//    {
//        currentPath++;
//        if (currentPath >= paths.Count)
//        {
//            currentPath = paths.Count - 1;
//        }
//    }
//}

//private void OnDrawGizmos()
//{
//    for (int i = 0; i < paths.Count; i++)
//    {
//        if (i + 1 < paths.Count)
//        {
//            Gizmos.color = Color.red;
//            Gizmos.DrawLine(paths[i].position, paths[i + 1].position);
//        }
//    }
//}