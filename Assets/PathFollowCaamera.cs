using UnityEngine;

public class PathFollowCaamera : MonoBehaviour
{
    public Transform Target;
    public float speed = 1;
    public float rotationSpeed = 1;
    public Vector3 offset;


    private void Start()
    {
        transform.position = Target.position + offset;
        transform.rotation = Target.rotation;
    }

    private void Update()
    {

        transform.position = Vector3.Lerp(transform.position, Target.position + offset, speed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Target.rotation, rotationSpeed * Time.deltaTime);



    
    }
    }

