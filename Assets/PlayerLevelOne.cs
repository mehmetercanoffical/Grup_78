using System.Collections;
using UnityEngine;

public class PlayerLevelOne : MonoBehaviour
{
    public Animator anim;
    public bool stop;
    public Transform bilge;
    public Transform Portal;
    public float rotationSpeed;
    public PathFollowCamera pathFollowCamera;

    private void Start() => anim.SetBool("Walk", true);

    public void RotateToBilge()
    {
        pathFollowCamera.stop = true;
        anim.SetBool("Walk", false);
        StartCoroutine(Rotate(bilge.position));
    }

    public void TurnPortal()
    {
        pathFollowCamera.stop = false;
        anim.SetBool("Walk", true);
        StartCoroutine(Rotate(Portal.position));
    }

    IEnumerator Rotate(Vector3 rot)
    {
        yield return new WaitForSeconds(1.0f);
        while (transform.rotation != Quaternion.Euler(rot))
        {
            Vector3 vector3 = transform.position;
            Vector3 vector3_1 = rot - vector3;
            vector3_1.y = 0.0f;
            Quaternion quaternion = Quaternion.LookRotation(vector3_1);
            transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
