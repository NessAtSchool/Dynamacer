using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager musicManagerInstance;
    public AudioClip BackgroundMusic;
    public AudioClip BombSoundFX;
    public GameObject SFXManager;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        CreateSingleton();
    }

    private void CreateSingleton()
    {
        if (musicManagerInstance != null && musicManagerInstance != this) Destroy(gameObject);
        else musicManagerInstance = this;
    }

    public void PlayBombSound()
    {
        gameObject.GetComponentInChildren<AudioSource>().clip = BombSoundFX ;
    }

}
