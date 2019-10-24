using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AIState
{
    IDLE,
    WALK,
    ENTERRUN,
    EXITRUN,
    ENTERATTACK,
    EXITATTACK,
    DEATH
}

public class AI : MonoBehaviour {
    private Transform m_Transform;
    private NavMeshAgent m_NavMeshAgent;
    private Animator m_Animator;
    private Transform player_Transform;
    private PlayerController player_Controller;
    private GameObject prefab_Effect;

    private Vector3 dir;
    private List<Vector3> posList;
    private AIState m_AIState;
    private int attack;
    private int life;
    private AIType m_AIType = AIType.NULL;

    public Vector3 Dir { get { return dir; } set { dir = value; } }
    public List<Vector3> PosList { get { return posList; } set { posList = value; } }
    public AIState M_AIState { get { return m_AIState; } set { m_AIState = value; } }

    public int Attack
    { get { return attack; } set { attack = value; } }

    public int Life
    {
        get
        {
            return life;
        }

        set
        {
            life = value;
            if (life <= 0)
            {
                ToggleState(AIState.DEATH);
            }
        }
    }

    public AIType AIType { get { return m_AIType; } set { m_AIType = value; } }

    private void Awake()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_NavMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        m_Animator = gameObject.GetComponent<Animator>();
        player_Transform = GameObject.Find("FPSController").GetComponent<Transform>();
        player_Controller = player_Transform.GetComponent<PlayerController>();
        prefab_Effect = Resources.Load<GameObject>("Effects/Gun/Bullet Impact FX_Flesh");
        m_NavMeshAgent.SetDestination(dir);
        m_AIState = AIState.IDLE;
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleState(AIState.DEATH);
            //Death();
        }

        //巡逻
        Distance();

        //跟随玩家
        AIFollowPlayer();

        //攻击玩家
        AIAttackPlayer();

        if (Input.GetKeyDown(KeyCode.M))
        {
            HitHard();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            HitNormal();
        }

    }
    

    /// <summary>
    /// 巡逻
    /// </summary>
    private void Distance()
    {
        if (m_AIState == AIState.IDLE || m_AIState == AIState.WALK)
        {
            if (Vector3.Distance(m_Transform.position, dir) <= 1)
            {
                int index = Random.Range(0, posList.Count);
                dir = posList[index];
                m_NavMeshAgent.SetDestination(dir);
                ToggleState(AIState.IDLE);
            }
            else
            {
                ToggleState(AIState.WALK);
            }
        }
    }

    /// <summary>
    /// 距离小于20米跟随玩家
    /// </summary>
    private void AIFollowPlayer()
    {
        if (Vector3.Distance(m_Transform.position,player_Transform.position) <= 20)
        {
            //跟随玩家
            ToggleState(AIState.ENTERRUN);
        }
        else
        {
            //恢复巡逻状态
            ToggleState(AIState.EXITRUN);
        }
    }

    /// <summary>
    /// AI角色攻击玩家
    /// </summary>
    private void AIAttackPlayer()
    {
        if (m_AIState == AIState.ENTERRUN)
        {
            if (Vector3.Distance(m_Transform.position, player_Transform.position) <= 2)
            {
                //攻击玩家
                ToggleState(AIState.ENTERATTACK);
            }
            else
            {
                //恢复奔跑状态
                ToggleState(AIState.EXITATTACK);
            }
        }
        
    }

    //状态切换
    private void ToggleState(AIState aIState)
    {
        switch (aIState)
        {
            case AIState.IDLE:
                IdleState();
                break;
            case AIState.WALK:
                WalkState();
                break;
            case AIState.ENTERRUN:
                EnterRunState();
                break;
            case AIState.EXITRUN:
                ExitRunState();
                break;
            case AIState.ENTERATTACK:
                EnterAttackState();
                break;
            case AIState.EXITATTACK:
                ExitAttackState();
                break;
            case AIState.DEATH:
                DeathState();
                break;
        }
    }

    /// <summary>
    /// 行走状态
    /// </summary>
    private void WalkState()
    {
        m_Animator.SetBool("Walk", true);
        m_AIState = AIState.WALK;
    }

    /// <summary>
    /// 默认状态
    /// </summary>
    private void IdleState()
    {
        m_Animator.SetBool("Walk", false);
        m_AIState = AIState.IDLE;
    }

    /// <summary>
    /// 进入奔跑状态
    /// </summary>
    private void EnterRunState()
    {
        m_Animator.SetBool("Run", true);
        m_AIState = AIState.ENTERRUN;
        m_NavMeshAgent.speed = 2;
        m_NavMeshAgent.enabled = true;
        m_NavMeshAgent.SetDestination(player_Transform.position);
    }

    /// <summary>
    /// 退出奔跑状态
    /// </summary>
    private void ExitRunState()
    {
        m_Animator.SetBool("Run", false);
        WalkState();
        m_NavMeshAgent.speed = 0.8f;
        m_NavMeshAgent.SetDestination(dir);
    }

    /// <summary>
    /// 进入攻击状态
    /// </summary>
    private void EnterAttackState()
    {
        m_Animator.SetBool("Attack", true);
        m_NavMeshAgent.enabled = false;
        m_AIState = AIState.ENTERATTACK;
    }

    /// <summary>
    /// 退出攻击状态
    /// </summary>
    private void ExitAttackState()
    {
        m_Animator.SetBool("Attack", false);
        m_NavMeshAgent.enabled = true;
        ToggleState(AIState.ENTERRUN);
    }

    //头部受到伤害
    private void HitHard()
    {
        m_Animator.SetTrigger("HitHard");
    }


    //任意部位受伤
    private void HitNormal()
    {
        m_Animator.SetTrigger("HitNormal");
    }

    //死亡状态
    private void DeathState()
    {
        m_AIState = AIState.DEATH;
        m_NavMeshAgent.enabled = true;
        m_NavMeshAgent.Stop();
        if (m_AIType == AIType.BOAR)
        {
            m_Animator.SetTrigger("Death");
            AudioManager.Instence.PlayAudioByName(ClipName.BoarDeath, m_Transform.position);
        }
        else if (m_AIType == AIType.CANNIBAL)
        {
            m_Animator.enabled = false;
            gameObject.GetComponent<AIRagdoll>().StartRagdoll();
            AudioManager.Instence.PlayAudioByName(ClipName.ZombieDeath, m_Transform.position);
        }
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(2);
        GameObject.Destroy(gameObject);
        SendMessageUpwards("AIDeath", gameObject);
    }

    /// <summary>
    /// 播放血液特效
    /// </summary>
    public void PlayEffect(RaycastHit hit)
    {
        GameObject.Instantiate<GameObject>(prefab_Effect, hit.point, Quaternion.LookRotation(hit.normal));
    }

    //头部受到伤害
    public void HardHit(int value)
    {
        HitHard();
        Life -= value;
        PlayHitAudio();
        Debug.Log("头部受到伤害----" + value);
    }

    //身体受到伤害
    public void NormalHit(int value)
    {
        HitNormal();
        Life -= value;
        PlayHitAudio();
        Debug.Log("身体受到伤害----" + value);
    }

    /// <summary>
    /// 播放受伤害音效
    /// </summary>
    private void PlayHitAudio()
    {
        if (AIType == AIType.CANNIBAL)
        {
            AudioManager.Instence.PlayAudioByName(ClipName.ZombieInjured, m_Transform.position);
        }
        else if (AIType == AIType.BOAR)
        {
            AudioManager.Instence.PlayAudioByName(ClipName.BoarInjured, m_Transform.position);
        }
    }

    /// <summary>
    /// 攻击玩家
    /// </summary>
    private void AttackPlayer()
    {
        player_Controller.CutHP(Attack);
        if (AIType == AIType.CANNIBAL)
        {
            AudioManager.Instence.PlayAudioByName(ClipName.ZombieAttack,m_Transform.position);
        }
        else if (AIType == AIType.BOAR)
        {
            AudioManager.Instence.PlayAudioByName(ClipName.BoarAttack, m_Transform.position);
        }
    }
}
