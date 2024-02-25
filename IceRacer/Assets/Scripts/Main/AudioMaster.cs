using UnityEngine;

public class AudioMaster : MonoBehaviour
{
    [Header("Audio Source")]
    public AudioSource ass;
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
    
    
    // Start is called before the first frame update
    void Start()
    {
        ass = GetComponent<AudioSource>();
    }

    public void PressButton()
    {
        ass.clip = buttonPress;
        ass.Play();
    }

    public void Explode()
    {
        ass.clip = explode;
        ass.Play();
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
        ass.Play();
    }

    public void Freeze()
    {
        ass.clip = freezeSound;
        ass.Play();
    }

    public void StopSignAppear()
    {
        ass.clip = stopSignAppear;
        ass.Play();
    }

    public void StopSignQTE()
    {
        ass.clip = stopSignQTE;
        ass.Play();
    }

    public void StopSignFail()
    {
        ass.clip = stopSignedFail;
        ass.Play();
    }

    public void GameOver()
    {
        ass.clip = gameOver;
        ass.Play();
    }

    public void StartGame()
    {
        ass.clip = startGame;
        ass.Play();
    } 

    public void ReadySet()
    {
        ass.clip = readySet;
        ass.Play();
    } 

    public void Go()
    {
        ass.clip = go;
        ass.Play();
    } 

    public void Drive()
    {
        ass.clip = drive;
        ass.Play();
    } 

    public void FuelEmpty()
    {
        ass.clip = fuelEmpty;
        ass.Play();
    } 

    public void ShieldBreak()
    {
        ass.clip = shieldBreak;
        ass.Play();
    } 
}
