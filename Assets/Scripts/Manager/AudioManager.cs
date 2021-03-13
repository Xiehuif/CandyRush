using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
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
    public void PlaySound(AudioClip clip,float volume = 1f)
    {
        if (clip == null) return;
        Sound.volume = volume;
        Sound.PlayOneShot(clip);
    }
}
