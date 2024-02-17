using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public PlayerMovement pm;

    public float KMpH = 55;

    public float Speed = 10;
    public float MaxSpeed = 100;

    public Vector3 Movement;

    private float PlayerKMpH;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        this.transform.position += Movement * Speed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerKMpH = pm.KMpH;
        if(this.KMpH > PlayerKMpH)
        {
            Movement = new Vector3(1, 0, 0);
            Speed = 5.5f;
        }
        else
        {
            Movement = new Vector3(-1, 0, 0);
            if(Speed < MaxSpeed)
            {
                Speed += PlayerKMpH/10 * Time.deltaTime;
            }
        }

        // if(Speed > 0)
        // {
        //     Speed += KMpH/10;
        // }

        if(this.transform.position.x < -100f)
        {
            Destroy(this.gameObject);
        }
    }
}
