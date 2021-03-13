using UnityEngine;
using System.Collections.Generic;
public class AudioManager : Singleton<AudioManager>
{
    public List<string> AudioNames = new List<string>();
    public List<AudioClip> AudioSources = new List<AudioClip>();
    public bool IsBGMPlaying, IsSoundPlaying;
    public AudioSource BGM, Sound;
    override protected void Awake()
    {
        base.Awake();
        if(BGM == null)
        {
            BGM = gameObject.AddComponent<AudioSource>();
            Debug.Log("Create BGM Source!");
        }
        if (Sound == null)
        {
            Sound = gameObject.AddComponent<AudioSource>();
            Debug.Log("Create Sound Source!");
        }
    }
    public void PlaySoundByName(string name,float volume = 1f)
    {
        if (!AudioNames.Contains(name))
        {
            Debug.LogError("The AudioClip Named " + name + " Not Exit!");
            return;
        }
        else PlaySound(AudioSources[AudioNames.IndexOf(name)],volume);
    }
    public void PlaySound(AudioClip clip,float volume = 1f)
    {
        if (clip == null) return;
        Sound.volume = volume;
        Sound.PlayOneShot(clip);
    }
}
