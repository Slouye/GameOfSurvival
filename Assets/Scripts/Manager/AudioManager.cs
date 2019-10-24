using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClipName
{
    /// <summary>
    /// 野猪攻击音效.
    /// </summary>
    BoarAttack,
    /// <summary>
    /// 野猪死亡音效.
    /// </summary>
    BoarDeath,
    /// <summary>
    /// 野猪受伤音效.
    /// </summary>
    BoarInjured,
    /// <summary>
    /// 丧尸攻击音效.
    /// </summary>
    ZombieAttack,
    /// <summary>
    /// 丧尸死亡音效.
    /// </summary>
    ZombieDeath,
    /// <summary>
    /// 丧尸受伤音效.
    /// </summary>
    ZombieInjured,
    /// <summary>
    /// 丧尸尖叫音效.
    /// </summary>
    ZombieScream,
    /// <summary>
    /// 子弹命中地面音效.
    /// </summary>
    BulletImpactDirt,
    /// <summary>
    /// 子弹命中身体音效.
    /// </summary>
    BulletImpactFlesh,
    /// <summary>
    /// 子弹命中金属音效.
    /// </summary>
    BulletImpactMetal,
    /// <summary>
    /// 子弹命中石头音效.
    /// </summary>
    BulletImpactStone,
    /// <summary>
    /// 子弹命中木材音效.
    /// </summary>
    BulletImpactWood,
    /// <summary>
    /// 玩家角色急促呼吸声.
    /// </summary>
    PlayerBreathingHeavy,
    /// <summary>
    /// 玩家角色受伤音效.
    /// </summary>
    PlayerHurt,
    /// <summary>
    /// 玩家角色死亡音效.
    /// </summary>
    PlayerDeath,
    /// <summary>
    /// 身体命中音效.
    /// </summary>
    BodyHit
}

public class AudioManager : MonoBehaviour {
    public static AudioManager Instence;
    private AudioClip[] audioClip;
    private Dictionary<string, AudioClip> audioClipDic;

    private void Awake()
    {
        Instence = this;
        audioClipDic = new Dictionary<string, AudioClip>();
        audioClip = Resources.LoadAll<AudioClip>("Audios/All");

        for (int i = 0; i < audioClip.Length; i++)
        {
            audioClipDic.Add(audioClip[i].name, audioClip[i]);
        }

        
    }

    public AudioClip GetAudioClipByName(ClipName ClipName)
    {
        AudioClip tempClip;
        audioClipDic.TryGetValue(ClipName.ToString() ,out tempClip);
        return tempClip;
    }

    public void PlayAudioByName(ClipName AudioName, Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(GetAudioClipByName(AudioName), pos);
    }

    public AudioSource AddAudioSourceComponent(GameObject go, ClipName ClipName, bool playOnAwke = true, bool loop = true)
    {
        AudioSource tempAudioSource = go.AddComponent<AudioSource>();
        tempAudioSource.clip = GetAudioClipByName(ClipName);
        tempAudioSource.playOnAwake = playOnAwke;
        if (playOnAwke)
        {
            tempAudioSource.Play();
        }
        tempAudioSource.loop = loop;
        return tempAudioSource;
    }
}
