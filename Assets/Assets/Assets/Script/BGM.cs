using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public AudioSource source;
    public AudioClip MainBgm;
    public AudioClip BossBgm;

    private void Awake()
    {
        AudioSource source = GetComponent<AudioSource>();
        source.PlayOneShot(MainBgm);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        source.volume = 0.2f;
        source.loop = true;
        if (collision.CompareTag("BossZone"))
        {
            source.clip = BossBgm;
            source.Play();
        }
       
    }

    public void LoadAudioClips()
    {
        MainBgm = Resources.Load<AudioClip>("Audio/MainBgm");
        BossBgm = Resources.Load<AudioClip>("Audio/BossBgm");
    }
}
