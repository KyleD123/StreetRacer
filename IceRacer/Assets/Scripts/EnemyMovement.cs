using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovement pm;
    [SerializeField] private float PlayerKMpH;
    public float KMpH = 55f;
    public float EnemyMovementSpeed;
    public float MaxEnemySpeed;
    [SerializeField] private Vector3 MovementVector;
    private float SpeedThreshhold;

    [SerializeField] int gasDamange = 1;

    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        SpeedThreshhold = pm.PlayerMaxSpeed / 2f;
    }

    void FixedUpdate()
    {
        ApplyEnemyMovement();
    }

    private void ApplyEnemyMovement()
    {
        this.transform.position += Vector3.right * EnemyMovementSpeed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateEnemySpeed();
        if(transform.position.x < -100f || this.transform.position.x > 100f)
        {
            Destroy(this.gameObject);
        }
        
    }

    /// <summary>
    /// Changes the enemies speed relative to the players current speed.
    /// </summary>
    private void CalculateEnemySpeed()
    {
        // Get Current player speed
        // Negative == move forward | Positive == move backwards 
        float CurrentPMSpeed = pm.PlayerCurrentSpeed - SpeedThreshhold;

        EnemyMovementSpeed = -CurrentPMSpeed * 3f;

        if (EnemyMovementSpeed > MaxEnemySpeed) EnemyMovementSpeed = MaxEnemySpeed;
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ADD EXPLOSTION ANIMATION
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            pm.DecreaseGas(gasDamange);
            Destroy(gameObject);
        }
    }
}
