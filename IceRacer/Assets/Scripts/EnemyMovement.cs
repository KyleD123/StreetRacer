using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private PlayerMovement pm;
    public float KMpH = 55;
    public float Speed;
    public float MaxSpeed;
    private Vector3 Movement;
    private float PlayerKMpH;

    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    void FixedUpdate()
    {
        this.transform.position += Movement * Speed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        this.MaxSpeed = pm.MaxKMpH - 25;
        PlayerKMpH = pm.KMpH;
        if(this.KMpH > PlayerKMpH)
        {
            Movement = new Vector3(1, 0, 0);
            Speed = 9f;
        }
        else
        {
            Movement = new Vector3(-1, 0, 0);
            if(Speed < MaxSpeed)
            {
                Speed += PlayerKMpH/10 * Time.deltaTime;
            }
        }

        if(this.transform.position.x < -100f || this.transform.position.x > 100f)
        {
            Destroy(this.gameObject);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // ADD EXPLOSTION ANIMATION
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }
}
