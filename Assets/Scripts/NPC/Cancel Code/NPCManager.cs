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
[Serializable]

public enum NPCS
{
    DRAGON,
    SOLIDER,
    TROLL,
    DRAGON_BOSS,
}


public class NPCManager : MonoBehaviour, ITakeDamage
{
    internal NPCAttackBase currentState;
    internal Animator anim;
    private INPCManager _inpcAttack;
    public Transform _targetPlayer;
    internal NPCMove _npcMove;
    public NPCS nPCS;

    [Header("NPC Attack Settings")]
    public float maxDistanceOffset = 10;
    public float remainingDistance = 2;
    public float attackWaitTime = 2;
    public float ChangeAttackByTime = 3;
    private float _changeAttackByTime = 3;
    public bool isAttacking = false;
    public bool isFirst = false;
    public bool isDead = false;
    private float _distance;
    public LayerMask playerLayers; //Target Layer

    public NPCAttackSettings _npcAttackSetting;
    public List<NPCAttackSettings> npcAttackSetting;
    public ParticleSystem Fire;

    [Header("NPC Animation")]
    private int _idle = Animator.StringToHash("Idle");
    private int _walk = Animator.StringToHash("Walk");
    private int _takeDamage = Animator.StringToHash("TakeDamage");
    private int _run = Animator.StringToHash("Run");
    private int _die = Animator.StringToHash("Die");


    [Header("NPC SOLIDER")]
    public GameObject DeactiveSwordTakeDamage;


    public void FireStart()
    {
        Fire.Play();
        Debug.Log("Fire Start");
    }

    public void FireStop()
    {
        Fire.Stop();
        IsFirstDeactive();
        Debug.Log("Fire Stop");
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        _inpcAttack = GetComponent<INPCManager>();
        SetState(_inpcAttack.nPCAttackBase);
        _npcMove = GetComponent<NPCMove>();

        _npcAttackSetting = npcAttackSetting[0];
        remainingDistance = _npcAttackSetting.distance;
    }
    void Update()
    {
        if (isDead) return;
        SearchAndWalk();
        currentState.Update(this);
    }


    private void SearchAndWalk()
    {
        Collider[] players = Physics.OverlapSphere(transform.position, maxDistanceOffset, playerLayers);

        if (players.Length > 0)
        {
            _distance = Vector3.Distance(transform.position, players[0].transform.position);
            checkDistanceForAttackType(_distance);

            _npcMove.RotateToPlayer();

            if (_distance > maxDistanceOffset)
            {
                SetPos(null, false);
                isAttacking = false;
            }
            else
            {
                if (_distance > remainingDistance)
                {
                    isAttacking = false;
                    SetPos(players[0].transform, true);

                }
                else if (isAttacking)
                    return;
                else
                {
                    isAttacking = true;
                    anim.SetBool(_walk, false);
                    _npcMove.GoToPos(transform);
                }


            }
        }
    }
    public void checkDistanceForAttackType(float distance)
    {

        if (isFirst)
        {
            _npcAttackSetting = npcAttackSetting[0];
            remainingDistance = _npcAttackSetting.distance;
            _changeAttackByTime = ChangeAttackByTime;
        }
        else
        {
            if (_changeAttackByTime < 0 && !isAttacking)
            {
                int random = UnityEngine.Random.Range(1, npcAttackSetting.Count);
                _npcAttackSetting = npcAttackSetting[random];
                remainingDistance = _npcAttackSetting.distance;
                _changeAttackByTime = ChangeAttackByTime;
            }
            _changeAttackByTime -= Time.deltaTime;
        }

        if (_npcAttackSetting.attackName == "Attack" && nPCS == NPCS.SOLIDER)
            DeactiveSwordTakeDamage.SetActive(false);
        else if (nPCS == NPCS.SOLIDER)
            DeactiveSwordTakeDamage.SetActive(true);

        // With Distance 
        //for (int i = 0; i < npcAttackSetting.Count; i++)
        //    int iPlus = i + 1;
        //    if (iPlus < npcAttackSetting.Count)
        //    {
        //        if (npcAttackSetting[i].distance > distance &&
        //                                npcAttackSetting[i + 1].distance < distance)
        //        {
        //            _npcAttackSetting = npcAttackSetting[i];
        //            remainingDistance = _npcAttackSetting.distance;
        //            break;
        //        }
        //    }
        //    else
        //    {
        //        _npcAttackSetting = npcAttackSetting[i];
        //        remainingDistance = _npcAttackSetting.distance;
        //    }
        //}
    }

    public void IsFirstDeactive()
    {
        isFirst = false;
    }
    internal void SetState(NPCAttackBase state)
    {

        currentState?.Exit(this);
        currentState = state;
        gameObject.name = gameObject.name + " Enemy - " + state.GetType().Name;
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
            if (npcAttackSetting[i].attackType == ATTACKTYPE.FLYFAR) Gizmos.color = Color.blue;
            else if (npcAttackSetting[i].attackType == ATTACKTYPE.FAR) Gizmos.color = Color.red;
            else if (npcAttackSetting[i].attackType == ATTACKTYPE.NEAR) Gizmos.color = Color.yellow;
            else if (npcAttackSetting[i].attackType == ATTACKTYPE.SONEAR) Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, npcAttackSetting[i].distance);
        }
    }

    public void AttackComingPlayer(Transform target, float damage)
    {
        Health health = target.GetComponent<Health>();
        if (health != null)
        {
            health.health -= ((_npcAttackSetting.attackDamage) / 100);
            health.health = Mathf.Max(0, health.health);
            Debug.LogWarning("Attacking to Player " + health.health);
            UIManager.Instance.UpdateHealthPlayer(health.health / 100);
        }
    }

    internal void TakeDamage()
    {
        Debug.Log("Take Damage coming Player", gameObject);
        anim.SetTrigger(_takeDamage);
    }

    public void CollisionControl(bool val)
    {

    }

    internal void Die()
    {
        currentState.Die(this);
        isDead = true;
        _targetPlayer = null;
        _npcMove.agent.isStopped = true;
        _npcMove.agent.enabled = false;
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject, 10f);
    }
}