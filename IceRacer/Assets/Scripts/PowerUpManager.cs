using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PowerUpManager : MonoBehaviour
{
    public List<GameObject> PowerUpList;

    private PlayerMovement pm;
    private float currentMovementSpeed;
    private float SpeedThreshhold;

    void Start()
    {
        PowerUpList = new List<GameObject>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (pm == null) 
        {
            pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
            SpeedThreshhold = pm.PlayerMaxSpeed / 2f;
        }
        else
        {
            foreach(GameObject PowerUp in PowerUpList)
            {
                float CurrentPMSpeed = pm.PlayerCurrentSpeed - SpeedThreshhold;
                currentMovementSpeed = -CurrentPMSpeed * 3f;
                if (currentMovementSpeed > 0) currentMovementSpeed = 0;
                PowerUp.transform.position += Vector3.right * currentMovementSpeed * Time.deltaTime;
            }
        }
       
    }
}
