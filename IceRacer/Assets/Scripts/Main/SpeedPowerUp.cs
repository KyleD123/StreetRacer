using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{

    [SerializeField] private PlayerMovement pm;
    [SerializeField] private MoveableObjectManager PowerUpMan;
    [SerializeField] private float Speed = 5.5f;
    [SerializeField] private float MaxPickUpMovementSpeed;
    [SerializeField] private Vector3 MovementVector;
    [SerializeField] private float PlayerKMpH;

    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        PowerUpMan = GameObject.Find("PowerUpManager").GetComponent<MoveableObjectManager>();
    }

    // Update is called once per frame
    void Update()
    {
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
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            if (pm.GetCurrentGasVal() > 0)
            {
                pm.IncreaseMaxKMpH(100);
            }
            Destroy(this.gameObject, 1);
        }

    }
}
