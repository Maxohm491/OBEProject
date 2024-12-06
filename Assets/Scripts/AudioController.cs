using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = audioClip;
        audioSource.loop = true;
        audioSource.volume = 0.8f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayAudio();
        }
    }

    void PlayAudio()
    {
        if (audioSource.clip != null)
        {
            audioSource.Play();
        }
    }
}
