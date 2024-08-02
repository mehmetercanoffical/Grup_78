using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum WhoIsObject
{
    Player,
    Enemy
}

public class GameManager : Singleton<GameManager>
{
    public GameObject Player;
    public float PlayerHealth = 100;

    private void OnEnable()
    {
        LevelManager.OnLevelLoaded += OnLevelLoaded;

    }

    private void OnDisable()
    {
        LevelManager.OnLevelLoaded -= OnLevelLoaded;
    }

    private void OnLevelLoaded(bool val)
    {
        if (val)
            Player = GameObject.FindGameObjectWithTag("Player");

    }

    public void SetActiveDeactivePlayer(bool val)
    {
        //if (Player == null)
        //{
        //    Player = GameObject.FindGameObjectWithTag("Player");
        //    Debug.Log("Player is null");
        //}
        //if (Player != null)
        //{
        //    Debug.Log("Player is active");
        //    Player.SetActive(val);
        //    Player.GetComponent<Animator>().SetTrigger("JumpInPortal");
        //}
    }
}
