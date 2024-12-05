using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioClip cardSelectSound;
    public AudioClip thunderAttackSound;
    public AudioClip bowAttackSound;
    public AudioClip swordAttackSound;
    public AudioClip cardAttackFailSound;  
    public AudioClip enemyHitSound;
    public AudioClip enemyDeathSound;
    public AudioClip towerDestroySound;
    public AudioClip wallDestroySound;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip backgroundMusic;

    private AudioSource musicSource;
    private AudioSource sfxSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            SetupAudioSources();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void SetupAudioSources()
    {
        musicSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = backgroundMusic;
        musicSource.volume = 0.4f;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayCardSelect()
    {
        sfxSource.PlayOneShot(cardSelectSound);
    }

    public void PlayCardAttack(Card.Section section)
    {
        switch (section)
        {
            case Card.Section.Thunder:
                sfxSource.PlayOneShot(thunderAttackSound);
                break;
            case Card.Section.Bow:
                sfxSource.PlayOneShot(bowAttackSound);
                break;
            case Card.Section.Sword:
                sfxSource.PlayOneShot(swordAttackSound);
                break;
        }
    }

    public void PlayCardAttackFail()   
    {
        sfxSource.PlayOneShot(cardAttackFailSound);
    }

    public void PlayEnemyHit()
    {
        sfxSource.PlayOneShot(enemyHitSound);
    }

    public void PlayEnemyDeath()
    {
        sfxSource.PlayOneShot(enemyDeathSound);
    }

    public void PlayTowerDestroy()
    {
        sfxSource.PlayOneShot(towerDestroySound);
    }

    public void PlayWallDestroy()
    {
        sfxSource.PlayOneShot(wallDestroySound);
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlayWinSound()
    {
        sfxSource.PlayOneShot(winSound);
    }

    public void PlayLoseSound()
    {
        sfxSource.PlayOneShot(loseSound);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
