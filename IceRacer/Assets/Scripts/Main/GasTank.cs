using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GasTank : MonoBehaviour
{
    public int MaxGas = 15;
    public int CurrentGas;
    public Sprite[] sprites;
    private PlayerMovement pm;

    private float TimeTillGasLoss = 15f;
    private float TimeSpent = 0;
    private Image fuelBar;
    private int fuelIndex = 15;
    private GameManager gm;
    private AudioMaster ass;

    // Start is called before the first frame update
    void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        gm = FindObjectOfType<GameManager>();
        CurrentGas = MaxGas;
        ass = GameObject.Find("AudioMaster").GetComponent<AudioMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.gs == GameState.GamePlay && !fuelBar)
        {
            fuelBar = GameObject.Find("Fuel").GetComponent<Image>();
        }

        if(TimeSpent < TimeTillGasLoss)
        {
            TimeSpent += Time.deltaTime;
        }
        else
        {
            DecreaseGas(pm.GasSpendRate);
            TimeSpent = 0;
        }

        if(gm.gs == GameState.GamePlay)
        {
            if(CurrentGas < 0)
            {
                CurrentGas = 0;
            }
            
            fuelBar.sprite = sprites[fuelIndex];
            fuelIndex = CurrentGas;
        }


    }

    public IEnumerator Flashing()
    {
        while(true)
        {
            ass.FuelEmpty();
            yield return new WaitForSeconds(0.5f);
            fuelBar.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            fuelBar.gameObject.SetActive(true);
            if (gm.gs == GameState.EndScreen) yield break;
        }
    }
    
    public void IncreaseGas(int amount)
    {
        CurrentGas += amount;
        if(CurrentGas > MaxGas)
        {
            CurrentGas = MaxGas;
        }
    }

    public void DecreaseGas(int amount)
    {
        CurrentGas -= amount;
    }

}
