using System.Collections;
using UnityEngine;

public class PlayerLevelOne : MonoBehaviour
{
    public Animator anim;
    public bool stop;
    public Transform bilge;
    public Transform Portal;
    public float rotationSpeedToBilge;
    public float rotationSpeedPortal;
    public PathFollowCamera pathFollowCamera;

    private void Start() => anim.SetBool("Walk", true);

    public void RotateToBilge()
    {
        pathFollowCamera.stop = true;
        anim.SetBool("Walk", false);
        StartCoroutine(Rotate(bilge.position, false, rotationSpeedToBilge));
    }

    public void TurnPortal()
    {
        StopAllCoroutines();
        StartCoroutine(Rotate(Portal.position, true, rotationSpeedPortal));


    }


    IEnumerator Rotate(Vector3 rot, bool val, float _rotateSpeed)
    {
        while (transform.rotation != Quaternion.Euler(rot))
        {
            {
                Vector3 vector3 = transform.position;
                Vector3 vector3_1 = rot - vector3;
                vector3_1.y = 0.0f;
                Quaternion quaternion = Quaternion.LookRotation(vector3_1);
                transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, _rotateSpeed * Time.deltaTime);
                yield return null;
            }
            Debug.Log("Rotate");
            pathFollowCamera.stop = !val;
            anim.SetBool("Walk", val);

        }
    }
}
