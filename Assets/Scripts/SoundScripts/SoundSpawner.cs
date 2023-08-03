using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SoundSpawner : Spawner
{
    protected static SoundSpawner instance;
    public static SoundSpawner Instance { get => instance ;}
    public List<AudioClip> ListAudioClips;
    [SerializeField] protected AudioMixerGroup EffectMixer;
    protected AudioSource AudioSource;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadListtAudioClips();
        this.LoadPrefabs();
        this.Loadaudiosourse();
    }
    protected void Loadaudiosourse()
    {
        this.AudioSource = GetComponent<AudioSource>();
    }
    protected override void LoadPrefabs()
    {
        if(prefabs.Count > 0) return;
        foreach(AudioClip element in ListAudioClips)
        {
            GameObject audioObject =  new GameObject(element.name);
            audioObject.transform.SetParent(transform.Find("Prefabs"));
            AudioSource newaudiosourse = audioObject.AddComponent<AudioSource>();
            newaudiosourse.outputAudioMixerGroup = this.EffectMixer;
            newaudiosourse.clip  = GetAudioClipbyName(element.name);
            audioObject.SetActive(false);
            GameObject despawn = new GameObject("Despawn");
            despawn.transform.SetParent(audioObject.transform);
            despawn.AddComponent<SoundDeSpawn>();
            despawn.GetComponent<SoundDeSpawn>().DespawnTime = element.length;
            prefabs.Add(audioObject.transform);
        }
    }
    protected AudioClip GetAudioClipbyName(string name)
    {
        foreach(AudioClip element in ListAudioClips)
        {
            if(name == element.name)
            {
                return element;
            }
        }
    return null;
    }
    public override Transform Spawn(string PrefabName, Vector3 position, Quaternion rotation)
    {
        Transform Newpre =   base.Spawn(PrefabName, position, rotation);
        AudioSource audioSource = Newpre.GetComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = this.EffectMixer;
        if(audioSource == null) Debug.LogWarning("Can founnd audiosourse" + PrefabName);
        //if(Newpre.transform.gameObject.activeInHierarchy) audioSource.PlayOneShot(GetAudioClipbyName(PrefabName));
        return Newpre;
    }
    protected void LoadListtAudioClips()
    {
        if(ListAudioClips.Count > 0 ) return;
        string path = "Sounds/";
        AudioClip[] array = Resources.LoadAll<AudioClip>(path);
        for(int i = 0 ; i < array.Length ; i++)
        {
            ListAudioClips.Add(array[i]);
        }
    }
    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null) Destroy(this);
        else instance = this;
    }
    public void Play_audio(string Name)
    {
        foreach(AudioClip element in ListAudioClips)
        {
            if(element.name == Name)
            {
                AudioSource.PlayOneShot(element);
            }
        }
    }
}
