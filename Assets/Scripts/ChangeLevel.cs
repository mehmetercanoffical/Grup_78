using UnityEngine;

public class ChangeLevel : MonoBehaviour
{
    public void ChangeLevelMethod() => LevelManager.Instance.NextLevel();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") ChangeLevelMethod();
    }
}
