using UnityEngine;

public class Scene6PlayerSword : MonoBehaviour
{
    public GameObject Sword;
    public Transform SwordHandle;
    public RuntimeAnimatorController SwordAnim;
    public Animator SwordAnimator;
    public void GetSword()
    {
        AttackObject(Sword, SwordHandle);
    }

    private void Start()
    {
        SwordAnimator.runtimeAnimatorController = SwordAnim;
    }


    public void AttackObject(GameObject obj, Transform handle)
    {
        obj.transform.SetParent(handle);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
    }
}
