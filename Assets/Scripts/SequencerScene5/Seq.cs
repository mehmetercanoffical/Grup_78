using UnityEngine;

public class Seq : MonoBehaviour
{
    SeqInterface seq;

    public void StartE()
    {
        SeqInterface seq = GetComponent<SeqInterface>();
        GetComponent<SeqInterface>().StartE();
    }

    public void Update()
    {
        GetComponent<SeqInterface>().Update();
    }

    public void Exit()
    {
        GetComponent<SeqInterface>().Exit();
    }
}