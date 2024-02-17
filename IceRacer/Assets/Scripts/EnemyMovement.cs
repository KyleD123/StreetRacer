using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private PlayerMovement pm;
    public float KMpH = 55;
    public float Speed = 10;
    private float MaxSpeed = 35;
    private Vector3 Movement;
    private float PlayerKMpH;

    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
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

        if(this.transform.position.x < -100f || this.transform.position.y > 100f)
        {
            Destroy(this.gameObject);
        }
    }
}
