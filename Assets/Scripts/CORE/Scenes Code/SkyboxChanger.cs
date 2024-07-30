using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public Material skyboxes;
    public void ChangeSkybox() => RenderSettings.skybox = skyboxes;
    private void OnEnable()
    {
        ChangeSkybox();
    }
}
