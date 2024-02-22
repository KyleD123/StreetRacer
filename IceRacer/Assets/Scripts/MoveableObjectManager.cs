using System.Collections.Generic;
using UnityEngine;


public class MoveableObjectManager : MonoBehaviour
{
    public List<GameObject> MoveableObjectList;

    private PlayerMovement pm;
    private float currentMovementSpeed;

    void Start()
    {
        MoveableObjectList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pm == null) 
        {
            pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        }
        else
        {
            // Clear nulls
            MoveableObjectList.RemoveAll(item => item == null);

            foreach(GameObject Object in MoveableObjectList)
            {
                if (Object == null) { MoveableObjectList.Remove(Object); }
                else
                {
                    float CurrentPMSpeed = pm.PlayerCurrentSpeed;
                    currentMovementSpeed = -CurrentPMSpeed * 3f;
                    if (currentMovementSpeed > 0) currentMovementSpeed = 0;
                    Object.transform.position += Vector3.right * currentMovementSpeed * Time.deltaTime;
                }
            }
        }
    }
}
