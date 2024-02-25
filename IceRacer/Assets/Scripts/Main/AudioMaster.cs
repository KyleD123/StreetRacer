using System;
using System.Collections;
using UnityEngine;

public class AudioMaster : MonoBehaviour
{
    [Header("Audio Source")]
    public AudioSource ass;
    private AudioSource music;
    [Header("Audio Clips")]
    public AudioClip buttonPress;
    public AudioClip explode;
    public AudioClip speedPickUp, gasPickUp, shieldPickup;
    public AudioClip freezeSound;
    public AudioClip stopSignAppear, stopSignQTE, stopSignedFail;
    public AudioClip gameOver;
    public AudioClip startGame;
    public AudioClip readySet, go;
    public AudioClip drive;
    public AudioClip fuelEmpty;
    public AudioClip shieldBreak;
    
    public delegate void SoundToPlay();
    
    // Start is called before the first frame update
    void Start()
    {
        ass = GetComponent<AudioSource>();
        music = GameObject.Find("Music").GetComponent<AudioSource>();
    }

    IEnumerator PlaySound()
    {
        music.Pause();
        ass.Play();
        while(ass.isPlaying)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.05f);
        music.UnPause();
    }

    public void PressButton()
    {
        ass.clip = buttonPress;
        StartCoroutine(PlaySound());
    }

    public void Explode()
    {
        ass.clip = explode;
        StartCoroutine(PlaySound());
    }

    public void PickUp(int sound)
    {
        switch(sound)
        {
            case 1:
                ass.clip = speedPickUp;
                break;

            case 2:
                ass.clip = gasPickUp;
                break;

            case 3:
                ass.clip = shieldPickup;
                break;

            default:
                break;
        }
        StartCoroutine(PlaySound());
    }

    public void Freeze()
    {
        ass.clip = freezeSound;
        StartCoroutine(PlaySound());
    }

    public void StopSignAppear()
    {
        ass.clip = stopSignAppear;
        StartCoroutine(PlaySound());
    }

    public void StopSignQTE()
    {
        ass.clip = stopSignQTE;
        StartCoroutine(PlaySound());
    }

    public void StopSignFail()
    {
        ass.clip = stopSignedFail;
        StartCoroutine(PlaySound());
    }

    public void GameOver()
    {
        ass.clip = gameOver;
        StartCoroutine(PlaySound());
    }

    public void StartGame()
    {
        ass.clip = startGame;
        StartCoroutine(PlaySound());
    } 

    public void ReadySet()
    {
        ass.clip = readySet;
        StartCoroutine(PlaySound());
    } 

    public void Go()
    {
        ass.clip = go;
        StartCoroutine(PlaySound());
    } 

    public void Drive()
    {
        ass.clip = drive;
        StartCoroutine(PlaySound());
    } 

    public void FuelEmpty()
    {
        ass.clip = fuelEmpty;
        StartCoroutine(PlaySound());
    } 

    public void ShieldBreak()
    {
        ass.clip = shieldBreak;
        StartCoroutine(PlaySound());
    } 
}
