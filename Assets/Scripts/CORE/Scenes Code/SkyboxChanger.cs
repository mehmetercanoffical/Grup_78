using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public Material skyboxes;
    public bool isPlayerHaveAction = false;
    public void ChangeSkybox() => RenderSettings.skybox = skyboxes;
    private void OnEnable()
    {
        ChangeSkybox();
        UIManager.Instance.ShowPlayer(isPlayerHaveAction);
        UIManager.Instance.ArrowCursoure(false);
    }
}
