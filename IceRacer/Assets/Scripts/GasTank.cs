using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GasTank : MonoBehaviour
{
    public int MaxGas = 15;
    public int CurrentGas;
    public Image[] sprites;
    private PlayerMovement pm;

    private float TimeTillGasLoss = 7.5f;
    private float TimeSpent = 0;

    // Start is called before the first frame update
    void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        CurrentGas = MaxGas;
    }

    // Update is called once per frame
    void Update()
    {
        if(TimeSpent < TimeTillGasLoss)
        {
            TimeSpent += Time.deltaTime;
        }
        else
        {
            DecreaseGas(1);
            TimeSpent = 0;
        }
    }

    public void IncreaseGas(int amount)
    {
        CurrentGas += amount;
    }

    public void DecreaseGas(int amount)
    {
        CurrentGas -= amount;
    }

}
