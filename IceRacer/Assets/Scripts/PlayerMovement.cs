using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float CarSpeedMultiplier;

    [SerializeField] private float VerticalMovementSpeedMarkiplier = 2f;
    [SerializeField] private float moveX;
    [SerializeField] private float moveY;

    public float PlayerCurrentSpeed;
    public float PlayerMaxSpeed;

    [SerializeField] private float PostionChangeSpeed = 0.2f;

    public int GasSpendRate = 1;
     [SerializeField] private GasTank gt;

    // Start is called before the first frame update
    void Start()
    {
        gt = this.GetComponent<GasTank>();
    }

    void FixedUpdate()
    {
        AlterPlayerPosition();
        AlterPlayerSpeed();
    }

    private void AlterPlayerSpeed()
    {
        if (gt.CurrentGas > 0)
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
        }
        else
        {
            this.PlayerCurrentSpeed = 0;
            moveX = 0;
            moveY = 0;
        }
    }

    private void AlterPlayerPosition()
    {
        // Vertical movement
        moveY = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(0, moveY, 0).normalized;
        transform.position += movement * VerticalMovementSpeedMarkiplier * Time.deltaTime;

        float normalizeSpeed = (PlayerCurrentSpeed / PlayerMaxSpeed) * -30f;
        if (normalizeSpeed > 0) normalizeSpeed = 0;

        Vector3 newPos = Vector3.Lerp(transform.position, new Vector3(normalizeSpeed, transform.position.y, 0f), 0.1f);
        transform.position = newPos;

        // Horizontal movement
        // if (Input.GetAxisRaw("Horizontal") > 0 && transform.position.x > -30f && gt.CurrentGas > 0) 
        // { transform.position -= new Vector3(PostionChangeSpeed, 0f, 0f); }

        // else if (Input.GetAxisRaw("Horizontal") < 0 && transform.position.x < 0) 
        // { transform.position += new Vector3(PostionChangeSpeed * 2, 0f, 0f); }

        // else 
        // { if (transform.position.x < 0) transform.position += new Vector3(PostionChangeSpeed / 2f, 0f, 0f); }
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
        yield return new WaitForSeconds(7);
        this.PlayerMaxSpeed -= amount;
        this.PlayerCurrentSpeed = this.PlayerMaxSpeed;
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
