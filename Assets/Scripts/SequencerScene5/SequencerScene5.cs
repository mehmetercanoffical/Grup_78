using System.Collections.Generic;
using UnityEngine;

public class SequencerScene5 : Singleton<SequencerScene5>
{

    public List<SeqList> seqList = new List<SeqList>();
    public int currentSeqList;
    public int currentSeq;
    public bool stop = false;

    public void Start()
    {
        seqList[currentSeqList].seqList[currentSeq].StartE();
    }

    private void Update()
    {
        if (stop) return;
        seqList[currentSeqList].seqList[currentSeq].Update();

    }

    public void NextSeq()
    {
        if (currentSeq < seqList[currentSeqList].seqList.Count - 1)
        {
            stop = true;
            seqList[currentSeqList].seqList[currentSeq].Exit();
            currentSeq++;
            seqList[currentSeqList].seqList[currentSeq].StartE();
            stop = false;
        }
    }

}

[System.Serializable]
public class SeqList
{
    public List<Seq> seqList = new List<Seq>();
}


public interface SeqInterface
{
    void StartE();
    void Update();
    void Exit();
}
