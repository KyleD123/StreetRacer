using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GasTank gt;
    [SerializeField] private float moveX;
    [SerializeField] private float moveY;
    public float PlayerCurrentSpeed;
    [Space(5)]
    [SerializeField] private float VerticalMovementSpeedMarkiplier = 2f;
    public float CarSpeedMultiplier;
    [Space(5)]
    public int GasSpendRate = 1;
    public float PlayerMaxSpeed;
    [SerializeField] private float PostionChangeSpeed = 0.2f;
    public bool FAMILY = false;

    public float carTypeMarkiplier = 1;

    private GameManager gm;

    public Animator anime;

    private Image speedMeter;

    public Sprite[] speedMeterSprites;
    
    // Start is called before the first frame update
    void Start()
    {
        gt = this.GetComponent<GasTank>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        anime = gameObject.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        AlterPlayerPosition();
        AlterPlayerSpeed();
    }

    void Update()
    {
        if(gm.gs == GameState.GamePlay && !speedMeter)
        {
            speedMeter = GameObject.Find("Speed").GetComponent<Image>();
        }

        if(gm.gs == GameState.GamePlay)
            SetSpeedometer();

    }

    private void SetSpeedometer()
    {
        float normSpeed = (PlayerCurrentSpeed/PlayerMaxSpeed) * 5;
        switch(normSpeed)
        {
            case 0:
                speedMeter.sprite = speedMeterSprites[0];
                break;
            case float n when (n > 0 && n < 2):
                speedMeter.sprite = speedMeterSprites[1];
                break;
            case float n when (n >= 2 && n < 3):
                speedMeter.sprite = speedMeterSprites[2];
                break;
            case float n when (n >= 3 && n < 4):
                speedMeter.sprite = speedMeterSprites[3];
                break;
            case float n when (n >= 4 && n < 5):
                speedMeter.sprite = speedMeterSprites[4];
                break;
            case float n when (n >= 5):
                speedMeter.sprite = speedMeterSprites[5];
                break;
            default:
                break;
        }
    }

    private void AlterPlayerSpeed()
    {
        if (gt.CurrentGas > 0 && gm.gs == GameState.GamePlay)
        {
            // Speed stuff (kinda whack tbh)
            moveX = Input.GetAxisRaw("Horizontal");
            if (PlayerCurrentSpeed < PlayerMaxSpeed)
            {
                PlayerCurrentSpeed += moveX * CarSpeedMultiplier * Time.deltaTime;
            }

            if (moveX == 0)
            {
                if (PlayerCurrentSpeed > 0)
                {
                    PlayerCurrentSpeed -= CarSpeedMultiplier / 2 * Time.deltaTime;
                }
                else
                {
                    PlayerCurrentSpeed = 0;
                }
            }
            
            // Vertical Contraints
            if (this.transform.position.y <= -18.46001f)
            {
                this.transform.position = new Vector3(this.transform.position.x, -18.46001f, 0);
            }
            if (this.transform.position.y >= 18.97278f)
            {
                this.transform.position = new Vector3(this.transform.position.x, 18.97278f, 0);
            }

            if (PlayerCurrentSpeed < 0) PlayerCurrentSpeed = 0;
        }
        else
        {
            if (PlayerCurrentSpeed > 0) PlayerCurrentSpeed -= CarSpeedMultiplier / 2 * Time.deltaTime;
            moveX = 0;
            moveY = 0;
            if(!gm.gameOver && gm.gs == GameState.GamePlay)
            {
                StopCar();
            }
        }
    }

    private void StopCar()
    {
        anime.SetBool("GasEmpty", true);
        gm.gameOver = true;
        gt.StartCoroutine("Flashing");
    }

    private void AlterPlayerPosition()
    {
        if(gm.gs == GameState.GamePlay)
        {
            // Vertical movement
            if (PlayerCurrentSpeed > 0)
            {
                moveY = Input.GetAxisRaw("Vertical");
                Vector3 movement = new Vector3(0, moveY, 0).normalized;
                transform.position += movement * VerticalMovementSpeedMarkiplier * carTypeMarkiplier * Time.deltaTime;
            }

            // Horizontal movement
            float normalizedPos = (PlayerCurrentSpeed / PlayerMaxSpeed) * -28f;
            float xPos = Mathf.Clamp(normalizedPos, -28f, 0);
            gameObject.transform.position = new Vector3(xPos, gameObject.transform.position.y, 0f);
        }
    }

    /// <summary>
    /// This increases the amount of gas the car currently has
    /// </summary>
    /// <param name="amount"></param>
    public void IncreaseGas(int amount)
    {
        this.gameObject.GetComponent<GasTank>().IncreaseGas(amount);
    }

    /// <summary>
    /// This decreases the amount of gas the car currently has
    /// </summary>
    /// <param name="amount"></param>
    public void DecreaseGas(int amount)
    {
        this.gameObject.GetComponent<GasTank>().DecreaseGas(amount);
    }

    /// <summary>
    /// This increases the amount of gas spent 
    /// </summary>
    /// <param name="amount"></param>
    public void IncreaseGasSpendRate(int amount)
    {
        this.GasSpendRate += amount;
        StartCoroutine(DecreaseGasSpendRate(amount));
    }

    

    /// <summary>
    /// Decreases the amount of gas spent
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    private IEnumerator DecreaseGasSpendRate(int amount)
    {
        yield return new WaitForSeconds(6f);
        this.GasSpendRate -= amount;
    }

    /// <summary>
    /// Increases the max KMpH and sets the KMpH to max
    /// </summary>
    /// <param name="amount"></param>
    public void IncreaseMaxKMpH(float amount)
    {
        FAMILY = true;
        anime.SetBool("SpeedUp", true);
        this.PlayerMaxSpeed += amount;
        this.PlayerCurrentSpeed = this.PlayerMaxSpeed;
        IncreaseGasSpendRate(2);
        StartCoroutine(DecreaseMaxKMpH(amount));
    }

    /// <summary>
    /// Decreases the max KMpH and sets the KMpH to max
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    private IEnumerator DecreaseMaxKMpH(float amount)
    {
        FAMILY = false;
        yield return new WaitForSeconds(7);
        anime.SetBool("SpeedUp", false);
        this.PlayerMaxSpeed -= amount;
        this.PlayerCurrentSpeed = this.PlayerMaxSpeed;
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}