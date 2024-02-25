using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasPowerUp : MonoBehaviour
{
    private PlayerMovement pm;
    private float Speed = 5.5f;
    private float MaxSpeed;
    private Vector3 Movement;
    private float PlayerKMpH;

    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.x < -100f || this.transform.position.x > 100f)
        {
            Destroy(this.gameObject);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }

        if(collision.gameObject.tag == "Player")
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            pm.IncreaseGas(3);
            Destroy(this.gameObject, 1);
        }

    }
}
