using NPCSpace;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum ATTACKTYPE
{
    FAR,
    NEAR,
    SONEAR,
}
public class NPCManager : MonoBehaviour
{
    internal NPCAttackBase currentState;
    internal Animator anim;
    private INPCManager _inpcAttack;
    public Transform _targetPlayer;
    internal NPCMove _npcMove;

    [Header("NPC Attack Settings")]
    public float maxDistanceOffset = 10;
    public float remainingDistance = 2;
    public bool isAttacking = false;
    public LayerMask playerLayers;
    public ATTACKTYPE attackType;

    public List<NPCAttackSettings> npcAttackSetting;


    private int _idle = Animator.StringToHash("Idle");
    private int _walk = Animator.StringToHash("Walk");
    private int _run = Animator.StringToHash("Run");

    private void Awake()
    {
        anim = GetComponent<Animator>();
        _inpcAttack = GetComponent<INPCManager>();
        SetState(_inpcAttack.nPCAttackBase);
        _npcMove = GetComponent<NPCMove>();
    }

    void Update()
    {
        Collider[] players = Physics.OverlapSphere(transform.position, maxDistanceOffset, playerLayers);

        if (players.Length > 0)
        {
            float distance = Vector3.Distance(transform.position, players[0].transform.position);

            checkDistanceForAttackType(distance);

            if (distance > maxDistanceOffset)
            {
                SetPos(null, false);
                isAttacking = false;
            }
            else
            {
                if (isAttacking == false)
                {
                    isAttacking = false;
                    SetPos(players[0].transform, true);
                }
                else
                {

                    for (int i = 0; i < npcAttackSetting.Count; i++)
                    {
                        if (npcAttackSetting[i].attackType == attackType)
                        {
                            remainingDistance = npcAttackSetting[i].distance;
                            break;
                        }
                    }

                    anim.SetBool(_walk, false);
                }
                _npcMove.RotateToPlayer();
            }
        }
        currentState.Update(this);
    }

    private void checkDistanceForAttackType(float distance)
    {
        for (int i = 0; i < npcAttackSetting.Count; i++)
        {
            int iPlus = i + 1;
            if (iPlus < npcAttackSetting.Count)
            {
                if (npcAttackSetting[i].distance > distance && npcAttackSetting[i + 1].distance < distance)
                {
                    attackType = npcAttackSetting[i].attackType;
                    break;
                }
            }
            else
            {
                attackType = npcAttackSetting[i].attackType;
                Debug.Log("Last " + attackType);
            }
        }

    }

    internal void SetState(NPCAttackBase state)
    {

        currentState?.Exit(this);
        currentState = state;

        gameObject.name = "Enemy - " + state.GetType().Name;
        currentState?.Start(this);
    }


    private void SetPos(Transform TargetPos, bool val)
    {
        _targetPlayer = TargetPos;
        _inpcAttack.Target = _targetPlayer;
        anim.SetBool(_walk, val);
        _npcMove.GoToPos(_targetPlayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, maxDistanceOffset);

        for (int i = 0; i < npcAttackSetting.Count; i++)
        {
            if (npcAttackSetting[i].attackType == ATTACKTYPE.FAR)
                Gizmos.color = Color.red;
            else if (npcAttackSetting[i].attackType == ATTACKTYPE.NEAR)
                Gizmos.color = Color.yellow;
            else if (npcAttackSetting[i].attackType == ATTACKTYPE.SONEAR)
                Gizmos.color = Color.green;

            Gizmos.DrawWireSphere(transform.position, npcAttackSetting[i].distance);
        }
    }

    public void IsAttacing() => isAttacking = !isAttacking;

}

// Far,10, 11

[Serializable]
public class NPCAttackSettings
{
    public ATTACKTYPE attackType;
    public string attackName;
    public float attackDamage;
    public float distance;
}