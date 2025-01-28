using System;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private AudioSource sourceButton;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
        
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySoundButton()
    {
        sourceButton.Play();
    }

    public void DestroyThis()
    {
        instance = null;
        Destroy(gameObject);
    }
    
    
}
