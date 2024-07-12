using NPCSpace;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[Serializable]
public class NPCAttackSettings
{
    public ATTACKTYPE attackType;
    public string attackName;
    public float attackDamage;
    public float distance;
}


[Serializable]
public enum ATTACKTYPE
{
    FLYFAR,
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

    public NPCAttackSettings _npcAttackSetting;
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
        SearchAndWalk();
        currentState.Update(this);
    }

    private void SearchAndWalk()
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
                    remainingDistance = _npcAttackSetting.distance;
                    anim.SetBool(_walk, false);
                }
                _npcMove.RotateToPlayer();
            }
        }
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
                    _npcAttackSetting = npcAttackSetting[i];
                    break;
                }
            }
            else
                _npcAttackSetting = npcAttackSetting[i];
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
    public void IsAttacing() => isAttacking = !isAttacking;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, maxDistanceOffset);

        for (int i = 0; i < npcAttackSetting.Count; i++)
        {
            if (npcAttackSetting[i].attackType == ATTACKTYPE.FLYFAR)
                Gizmos.color = Color.blue;
            else if (npcAttackSetting[i].attackType == ATTACKTYPE.FAR)
                Gizmos.color = Color.red;
            else if (npcAttackSetting[i].attackType == ATTACKTYPE.NEAR)
                Gizmos.color = Color.yellow;
            else if (npcAttackSetting[i].attackType == ATTACKTYPE.SONEAR)
                Gizmos.color = Color.black;

            Gizmos.DrawWireSphere(transform.position, npcAttackSetting[i].distance);
        }
    }

    public void TakeDamage()
    {
        Health health = _targetPlayer.GetComponent<Health>();
        if (health != null)
        {
            health.health -= ((_npcAttackSetting.attackDamage) / 100f);
            health.health = Mathf.Max(0, Mathf.Min(1, health.health));
            UIManager.Instance.UpdateHealthPlayer(health.health);
        }
    }
}
