using UnityEngine;

public class camera_follow : MonoBehaviour
{
    public GameObject OtherCameraActiveSignal;

    public void StartOtherOnTimelineSignal()
    {
        OtherCameraActiveSignal.SetActive(true);
        gameObject.SetActive(false);
    }
}

