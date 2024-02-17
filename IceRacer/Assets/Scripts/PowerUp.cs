using System.Collections;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    private PlayerMovement pm;
    public float KMpH = 55;
    public float Speed = 5.5f;
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
            Movement = new Vector3(-1, 0, 0);
            Speed = 5.5f;
        }
        else
        {
            Movement = new Vector3(0, 0, 0);
        }

        if(this.transform.position.x < -100f || this.transform.position.x > 100f)
        {
            Destroy(this.gameObject);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.tag == "Player")
        {
            pm.IncreaseMaxKMpH(100);
            Destroy(this.gameObject);
        }

    }
}
