using NPCSpace;
using System;
using System.Collections.Generic;
using UnityEngine;

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
public class NPCManager : MonoBehaviour, ITakeDamage
{
    internal NPCAttackBase currentState;
    internal Animator anim;
    private INPCManager _inpcAttack;
    public Transform _targetPlayer;
    internal NPCMove _npcMove;


    [Header("NPC Attack Settings")]
    public float maxDistanceOffset = 10;
    public float remainingDistance = 2;
    public float attackWaitTime = 2;
    public bool isAttacking = false;
    public bool isFirst = false;
    public LayerMask playerLayers;

    float distance;

    public NPCAttackSettings _npcAttackSetting;
    public List<NPCAttackSettings> npcAttackSetting;
    public List<NPCAttackSettings> npcAttackSettingBasic;

    private int _idle = Animator.StringToHash("Idle");
    private int _walk = Animator.StringToHash("Walk");
    private int _takeDamage = Animator.StringToHash("TakeDamage");
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
            distance = Vector3.Distance(transform.position, players[0].transform.position);
            checkDistanceForAttackType(distance);

            _npcMove.RotateToPlayer();

            if (distance > maxDistanceOffset)
            {
                SetPos(null, false);
                isAttacking = false;
            }
            else
            {
                if (distance > remainingDistance)
                {
                    isAttacking = false;
                    SetPos(players[0].transform, true);

                }
                else if (isAttacking)
                {
                    Debug.Log("Attack");    
                    return;
                }
                else
                {
                    isAttacking = true;
                    anim.SetBool(_walk, false);
                    if (!isFirst)
                    {
                        _npcMove.GoToPos(transform);
                    }
                }
            }
        }
    }
    public void checkDistanceForAttackType(float distance)
    {


        for (int i = 0; i < npcAttackSettingBasic.Count; i++)
        {
            int iPlus = i + 1;
            if (iPlus < npcAttackSettingBasic.Count)
            {
                if (npcAttackSettingBasic[i].distance > distance &&
                                        npcAttackSettingBasic[i + 1].distance < distance)
                {
                    _npcAttackSetting = npcAttackSettingBasic[i];
                    remainingDistance = _npcAttackSetting.distance;
                    break;
                }
            }
            else
            {
                _npcAttackSetting = npcAttackSettingBasic[i];
                remainingDistance = _npcAttackSetting.distance;
            }
        }


        //int random = UnityEngine.Random.Range(0, npcAttackSetting.Count);
        //_npcAttackSetting = npcAttackSetting[random];
        //remainingDistance = _npcAttackSetting.distance;

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
    public void IsAttacing()
    {
        isAttacking = !isAttacking;
        isFirst = true;

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, maxDistanceOffset);

        for (int i = 0; i < npcAttackSettingBasic.Count; i++)
        {
            if (npcAttackSettingBasic[i].attackType == ATTACKTYPE.FLYFAR) Gizmos.color = Color.blue;
            else if (npcAttackSettingBasic[i].attackType == ATTACKTYPE.FAR) Gizmos.color = Color.red;
            else if (npcAttackSettingBasic[i].attackType == ATTACKTYPE.NEAR) Gizmos.color = Color.yellow;
            else if (npcAttackSettingBasic[i].attackType == ATTACKTYPE.SONEAR) Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, npcAttackSettingBasic[i].distance);
        }
    }

    public void Attack(Transform target, float damage)
    {
        Health health = _targetPlayer.GetComponent<Health>();
        if (health != null)
        {
            health.health -= ((_npcAttackSetting.attackDamage));
            health.health = Mathf.Max(0, health.health);
            Debug.LogWarning("Attacking to Player " + health.health);
            //UIManager.Instance.UpdateHealthPlayer(health.health);
        }
    }

    internal void TakeDamage() => anim.SetTrigger(_takeDamage);

    public void CollisionControl(bool val)
    {

    }
}
