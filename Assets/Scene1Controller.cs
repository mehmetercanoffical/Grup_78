using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1Controller : MonoBehaviour
{
   public void NextLevel()
    {
        LevelManager.Instance.NextLevel();
    }

    public void QuitGame()
    {
       Application.Quit();
    }


}
