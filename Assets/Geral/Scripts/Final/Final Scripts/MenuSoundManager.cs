using UnityEngine;
using System;
public enum MenuSoundType
{
    ButtonClick,
}

[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class MenuSoundManager : MonoBehaviour
{
    [SerializeField] private SoundList[] soundList;
    private static MenuSoundManager instance;
    private AudioSource audioSource, musicSource;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        musicSource = gameObject.GetComponent<AudioSource>();
        musicSource.loop = true;
    }

    public static void PlaySound(MenuSoundType sound, float volume = 0.75f)
    {
        AudioClip[] clips = instance.soundList[(int)sound].sounds;
        AudioClip randomclip = clips[UnityEngine.Random.Range(0, clips.Length)];
        instance.audioSource.PlayOneShot(randomclip, volume);
    }

    public static void PlayMusic(AudioClip musicClip, float volume = 0.1f)
    {
        instance.musicSource.clip = musicClip;
        instance.musicSource.volume = volume;
        instance.musicSource.Play();
    }

    public static void StopMusic() { instance.musicSource.Stop(); }

#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] names = Enum.GetNames(typeof(MenuSoundType));
        Array.Resize(ref soundList, names.Length);
        for (int i = 0; i < soundList.Length; i++)
        {
            soundList[i].name = names[i];
        }
    }
#endif
}

[Serializable]
public struct SoundList
{
    public AudioClip[] sounds { get => Sounds; }
    [HideInInspector] public string name;
    [SerializeField] private AudioClip[] Sounds;
}
