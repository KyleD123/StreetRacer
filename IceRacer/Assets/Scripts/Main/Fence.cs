using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour
{
    private Vector3 StartPosition = new Vector3(460,22f,0);

    private PlayerMovement pm;

    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += new Vector3(-pm.PlayerCurrentSpeed/150, 0, 0);

        if(transform.position.x <= -559)
        {
            transform.position = StartPosition;
        }
    }

}
