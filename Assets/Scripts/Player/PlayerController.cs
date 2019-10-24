using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

/// <summary>
/// 玩家死亡状态委托
/// </summary>
public delegate void PlayerDeathDelegate();

public class PlayerController : MonoBehaviour {
    private Transform m_Transform;
    private FirstPersonController m_FPC;
    private PlayerInfoPanel m_PlayerInfoPanel;
    private BloodScreenPanel bloodScreenPanel;
    private AudioSource m_AudioSource;          //呼吸声

    private int hp = 1000;             //生命
    private int vit = 100;             //体力
    private int index;                 //体力标志位 
    private bool audioIsPlay = false;   //呼吸标志位
    private bool IsDeath = false;       //玩家死亡标志位
    public event PlayerDeathDelegate PlayerDeathDel;    //死亡事件

    public int Hp { get { return hp; } set { hp = value; } }
    public int Vit { get { return vit; } set { vit = value; } }

    void Start () {
        m_Transform = gameObject.GetComponent<Transform>();
        m_FPC = gameObject.GetComponent<FirstPersonController>();
        m_PlayerInfoPanel = GameObject.Find("Canvas/MainPanel/PlayerInfoPanel").GetComponent<PlayerInfoPanel>();
        bloodScreenPanel = GameObject.Find("Canvas/MainPanel/BloodScreenPanel").GetComponent<BloodScreenPanel>();
        m_AudioSource = AudioManager.Instence.AddAudioSourceComponent(gameObject, ClipName.PlayerBreathingHeavy, false);

        StartCoroutine(RestoreVit());
    }
	
	
	void Update () {
        CutVIT();
       
    }

    /// <summary>
    /// 生命值削减
    /// </summary>
    public void CutHP(int value)
    {
        Hp -= value;
        m_PlayerInfoPanel.SetHPBar(Hp);
        bloodScreenPanel.SetImageAlpha();
        if (Hp <= 50 && IsDeath == false)
        {
            PlayerDeath();
        }
    }

    /// <summary>
    /// 体力削减
    /// </summary>
    private void CutVIT()
    {
        if (m_FPC.M_PlayerStart == PlayerStart.WALK)
        {
            index++;
            if (index >= 20)
            {
                Vit -= 1;
                index = 0;
                RestoreSpeed();
                m_PlayerInfoPanel.SetVITBar(Vit);
            }
        }

        if (m_FPC.M_PlayerStart == PlayerStart.RUN)
        {
            index++;
            if (index >= 20)
            {
                Vit -= 2;
                index = 0;
                RestoreSpeed();
                m_PlayerInfoPanel.SetVITBar(Vit);
            }
        }

        if (Vit <= 50 && audioIsPlay == false)
        {
            m_AudioSource.Play();
            audioIsPlay = true;
            Debug.Log("播放呼吸声");
        }
    }

    /// <summary>
    /// 体力自动恢复
    /// </summary>
    IEnumerator RestoreVit()
    {
        Vector3 tempPos;
        while (true)
        {
            tempPos = m_Transform.position;
            yield return new WaitForSeconds(1);
            if (Vit <= 95 && m_Transform.position == tempPos)
            {
                Vit += 5;
               
            }
            else if (Vit > 95 && Vit < 100 && m_Transform.position == tempPos)
            {
                Vit = 100;
            }
            if (Vit >= 50 && audioIsPlay == true)
            {
                m_AudioSource.Stop();
                audioIsPlay = false;
                Debug.Log("停止呼吸声");
            }

            RestoreSpeed();
            m_PlayerInfoPanel.SetVITBar(Vit);

        }
    }
    
    //根据体力值修改行走/奔跑速度
    private void RestoreSpeed()
    {
        //新的移动 / 奔跑速度 = 原始默认速度 * (VIT * 0.01f)；
        m_FPC.WalkSpeed = 5 * (Vit * 0.01f);
        m_FPC.RunSpeed = 10 * (Vit * 0.01f);
    }


    /// <summary>
    /// 玩家死亡
    /// </summary>
    private void PlayerDeath()
    {
        IsDeath = true;
        AudioManager.Instence.PlayAudioByName(ClipName.PlayerDeath, m_Transform.position);
        gameObject.GetComponent<FirstPersonController>().enabled = false;
        GameObject.Find("Managers").GetComponent<InputManager>().enabled = false;
        PlayerDeathDel();
        StartCoroutine(JompScene());
    }

    /// <summary>
    /// 场景跳转
    /// </summary>
    /// <returns></returns>
    IEnumerator JompScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("ResetScene");
    }
}
