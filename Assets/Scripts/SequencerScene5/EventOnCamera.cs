using System.Collections.Generic;
using UnityEngine;

public class EventOnCamera : MonoBehaviour
{
    public List<SequencerS> sequencersS = new List<SequencerS>();
    public int currentSequencer = 0;
    public int currentAction = 0;

    public void Start()
    {
        sequencersS[currentSequencer].actions[currentAction].Start();
    }

    public void Update()
    {
        sequencersS[currentSequencer].actions[currentAction].Update();
    }

    public void Exit()
    {
        sequencersS[currentSequencer].actions[currentAction].Exit();
    }

    public void UpdateCurrentSequencer()
    {
        currentSequencer++;
    }

    public void UpdateCurrentAction()
    {
        if (currentAction < sequencersS[currentSequencer].actions.Count - 1)
            currentAction++;
        else
        {
            currentAction = 0;
            UpdateCurrentSequencer();
        }
    }
}


[System.Serializable]
public class SequencerS
{
    public List<Sequencer> actions = new List<Sequencer>();
}