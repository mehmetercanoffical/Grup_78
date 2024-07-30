using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowCamera : MonoBehaviour
{
    public List<Transform> paths = new();
    public float speed = 1.0f;
    public float rotationSpeed = 1.0f;
    public float reachDistance = 0.1f;
    public int currentPath = 0;
    public GameObject Portal;
    public GameObject Player;
    public bool isPlayerActive = false;
    public bool stop = false;
    public bool isLevelChange = false;


    private void Start()
    {
        transform.position = paths[0].position;
        transform.rotation = paths[0].rotation;
    }

    private void Update()
    {
        if (stop) return;
        float distance = Vector3.Distance(paths[currentPath].position, transform.position);
        transform.position = Vector3.MoveTowards(transform.position, paths[currentPath].position, Time.deltaTime * speed);
        transform.rotation = Quaternion.Slerp(transform.rotation, paths[currentPath].rotation, Time.deltaTime * rotationSpeed);

        if (distance <= reachDistance)
        {
            currentPath++;
            if (currentPath >= paths.Count)
            {
                currentPath = paths.Count - 1;
                if (Portal != null) Portal.SetActive(true);
                if (isPlayerActive) StartCoroutine(Active());
              if(isLevelChange)
                LevelChange();
            } 
        }
    }

    private void LevelChange() => LevelManager.Instance.NextLevel();

    IEnumerator Active()
    {
        yield return new WaitForSeconds(1.0f);
        Player.SetActive(true);
        Player.GetComponent<Animator>().SetTrigger("JumpInPortal");
        isPlayerActive = false;
        yield return new WaitForSeconds(1.0f);
        while (Portal.transform.localScale.x > 0)
        {
            Portal.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(1.0f);
        GetComponent<ThirdPersonCamera>().enabled = true;
        this.enabled = false;

     

        Portal.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < paths.Count; i++)
        {
            if (i == 0) Gizmos.color = Color.red;
            else Gizmos.color = Color.green;

            Gizmos.DrawWireSphere(paths[i].position, 0.3f);

            if (i + 1 < paths.Count)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(paths[i].position, paths[i + 1].position);

            }
        }
    }
}
