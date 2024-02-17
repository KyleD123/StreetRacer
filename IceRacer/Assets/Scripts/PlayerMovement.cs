using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private float moveX;
    private float moveY;

    public float KMpH;
    public float MaxKMpH;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(0, moveY, 0).normalized;
        transform.position += movement * speed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        if(KMpH < MaxKMpH && KMpH >= 0)
        {
            KMpH += moveX * speed * Time.deltaTime;
        }

        if(moveX == 0)
        {
            if(KMpH > 0)
            {
                KMpH -= speed/2 * Time.deltaTime;
            }
            else
            {
                KMpH = 0;
            }
        }

        moveY = Input.GetAxisRaw("Vertical");
        if(this.transform.position.y <= -19.02722f)
        {
            this.transform.position = new Vector3(this.transform.position.x, -19.02722f, 0);
        }
        if(this.transform.position.y >= 18.97278f)
        {
            this.transform.position = new Vector3(this.transform.position.x, 18.97278f, 0);
        }
    }
}
