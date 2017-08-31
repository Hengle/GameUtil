using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundEffectManager : MonoBehaviour 
{
    public static SoundEffectManager Instance;
    void Awake()
    {
        Instance = this;
    }
    
    public float volumn = 1.0f;

    public List<AudioClip> audio_clips = new List<AudioClip>();

    public List<AudioSource> channels = new List<AudioSource>();

    public List<AudioSource> loop_channels = new List<AudioSource>();

    public List<AudioClip> bgm_clips = new List<AudioClip>();

    public AudioSource bgm_channel;

    private Dictionary<string, AudioClip> audio_clip_index = new Dictionary<string, AudioClip>();

    void Start() 
    {
        BuildDict();
    }
    
    void BuildDict() 
    {
        foreach (var clip in audio_clips) 
        {
            if (clip != null) 
            {
                audio_clip_index.Add(clip.name, clip);
            }
        }
    }

    AudioSource FindAvailableChannel() 
    {
        for( int i = 0; i< channels.Count; i++)
        {
            if (channels[i].isPlaying == false) 
            {
                return channels[i];
            }
        }

        Debug.LogWarning("音频通道不够用了，尝试扩展新的音频通道");
        return null;
    }

    AudioSource FindAvailableLoopChannel() 
    {
        for (int i = 0; i < loop_channels.Count; i++)
        {
            if (loop_channels[i].isPlaying == false)
            {
                return loop_channels[i];
            }
        }

        Debug.LogWarning("音频通道不够用了，尝试扩展新的音频通道");
        return null;
    }


	public AudioSource PlaySoundEffect(string clip_name) 
    {
        AudioClip clip;
        audio_clip_index.TryGetValue(clip_name, out clip);
        if (clip != null) 
        {
            AudioSource ass = FindAvailableChannel();
            if (ass != null) 
            {
                ass.PlayOneShot(clip, volumn);
            }
			return ass;
        }
		return null;
    }



    public void StartLoopSoundEffect(string clip_name) 
    {
        AudioClip clip;
        audio_clip_index.TryGetValue(clip_name, out clip);
        if (clip != null)
        {
            AudioSource ass = FindAvailableLoopChannel();
            if (ass != null)
            {
                ass.loop = true;
                ass.clip = clip;
                ass.Play();
            }
        }
    }

    public void StopLoopSoundEffect(string clip_name) 
    {
       AudioClip clip;
       audio_clip_index.TryGetValue(clip_name, out clip);
       List<AudioSource> ass = loop_channels.FindAll(x => x.clip == clip);

       foreach (var sound in ass) 
       {
           sound.Stop();
       }
    }

    public void PlayRandomBgm() 
    {
        if (bgm_clips.Count > 0)
        {
            AudioClip ac = bgm_clips[Random.Range(0, bgm_clips.Count)];
            bgm_channel.PlayOneShot(ac);

            Invoke("PlayRandomBgm", ac.length);
        }
    }

    private int current_bgm = 0;
    public void LoopBgms( bool reset = false ) 
    {
        if (reset) 
        {
            current_bgm = 0;
        }

        if (bgm_clips.Count > 0)
        {
            AudioClip ac = bgm_clips[current_bgm%(bgm_clips.Count-1)];
            current_bgm++;

            bgm_channel.PlayOneShot(ac);

            Invoke("PlayRandomBgm", ac.length);
        }
    }

    public void PlayBgm(string bgm, float speed_scale = 1.0f ) 
    {
        if (bgm_clips.Count > 0)
        {
            AudioClip ac = bgm_clips.Find(x => x.name == bgm);
            bgm_channel.pitch = speed_scale;
            bgm_channel.PlayOneShot(ac);
        }
    }
}
