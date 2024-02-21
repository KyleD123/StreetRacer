using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{

    [SerializeField] private PlayerMovement pm;
    [SerializeField] private float Speed = 5.5f;
    [SerializeField] private float MaxPickUpMovementSpeed;
    [SerializeField] private Vector3 MovementVector;
    [SerializeField] private float PlayerKMpH;

    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    void FixedUpdate()
    {
        // transform.position += MovementVector * Speed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        // MaxPickUpMovementSpeed = pm.PlayerMaxSpeed - 25f;
        // PlayerKMpH = pm.PlayerCurrentSpeed;
        // if (PlayerKMpH <= 0)
        // {
        //     MovementVector = new Vector3(0, 0, 0);
        // }
        // else
        // {
        //     MovementVector = new Vector3(-1, 0, 0);
        //     if (Speed < MaxPickUpMovementSpeed)
        //     {
        //         Speed += PlayerKMpH/10 * Time.deltaTime;
        //     }
        // }

        if(transform.position.x < -100f || transform.position.x > 100f)
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
            pm.IncreaseMaxKMpH(100);
            Destroy(this.gameObject);
        }

    }
}
