using System;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    AudioSource[] _audioSources = null;

    /// <summary>
    /// Sound Volume
    /// </summary>
    public float this[SoundType type]
    {
        get { return _audioSources[(int)type].volume; }
        set { _audioSources[(int)type].volume = value; }
    }

    protected override void InitFromAwake()
    {
        base.InitFromAwake();

        string[] soundTypeNames = Enum.GetNames(typeof(SoundType));
        _audioSources = new AudioSource[soundTypeNames.Length];
        for (int i = 0; i < soundTypeNames.Length; i++)
        {
            GameObject obj = new GameObject { name = soundTypeNames[i] };
            _audioSources[i] = obj.GetOrAddComponent<AudioSource>();
            obj.transform.SetParent(transform);
        }

        _audioSources[(int)SoundType.BGM].loop = true;
    }

    public void Play(SoundType type, string path)
    {
        if (string.IsNullOrEmpty(path))
            return;

        AudioClip clip = Manager.Resource.Load<AudioClip>(path);
        if (clip == null)
            return;

        AudioSource audioSource = _audioSources[(int)type];

        switch (type)
        {
            case SoundType.BGM:
                if (audioSource.isPlaying)
                    audioSource.Stop();

                audioSource.clip = clip;
                audioSource.Play();
                break;
            case SoundType.SFX:
                audioSource.PlayOneShot(clip);
                break;
            default:
                // SoundType 추가 시 수정
                // nothing
                break;
        }
    }

    public void Stop(SoundType type)
    {
        AudioSource audioSource = _audioSources[(int)type];
        audioSource.Stop();
    }

    public void SetPitch(SoundType type, float pitch = 1f)
    {
        AudioSource audioSource = _audioSources[(int)type];
        audioSource.pitch = pitch;
    }

    public void Clear()
    {
        for (int i = 0; i < _audioSources.Length; i++)
        {
            _audioSources[i].Stop();
        }
    }
}

public enum SoundType { BGM, SFX, }