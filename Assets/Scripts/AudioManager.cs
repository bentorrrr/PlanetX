using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--------- Audio Source ---------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("--------- Audio Source ---------")]
    public AudioClip background;
    public AudioClip PlayerDamage;
    public AudioClip PlayerShoot;
    public AudioClip PlayerDeath;
    public AudioClip PlayerUpgrade;
    public AudioClip EnemyDeath;
    public AudioClip BossHit;
    public AudioClip BossDeath;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
