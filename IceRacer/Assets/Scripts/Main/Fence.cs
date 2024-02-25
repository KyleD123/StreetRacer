using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour
{
    private Vector3 StartPosition = new Vector3(960,24f,0);

    private PlayerMovement pm;
    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pm)
        {
            transform.position += new Vector3(-pm.PlayerCurrentSpeed/200, 0, 0);

            if(transform.position.x <= -559)
            {
                transform.position = StartPosition;
            }
        }
    }

}
