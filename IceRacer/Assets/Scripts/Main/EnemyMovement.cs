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
    [SerializeField] Animator anime;
    public bool dead = false;
    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = gm = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        anime = gameObject.GetComponent<Animator>();
        SpeedThreshhold = pm.PlayerMaxSpeed / 2f;
    }

    void FixedUpdate()
    {
        if(!dead)
            ApplyEnemyMovement();
    }

    private void ApplyEnemyMovement()
    {
        this.transform.position += Vector3.right * EnemyMovementSpeed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.gs != GameState.EndScreen)
        {
            CalculateEnemySpeed();
            if(transform.position.x < -200f || this.transform.position.x > 200f)
            {
                Destroy(this.gameObject);
            }
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
            transform.GetChild(0).gameObject.SetActive(false);
            anime.SetBool("Dead", true);
            dead = true;
        }

        if (collision.gameObject.tag == "Player")
        {
            transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("GameManager").GetComponent<GameManager>().playerKills++;
            pm.DecreaseGas(gasDamange);
            anime.SetBool("Dead", true);
            dead = true;
        }
    }

    private void DestroyCar()
    {
        Destroy(gameObject);
    }
}
