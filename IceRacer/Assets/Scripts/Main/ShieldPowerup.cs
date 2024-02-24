using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerup : MonoBehaviour
{
   private PlayerMovement pm;

   void Start()
   {
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
   }

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
            pm.shieldActive = true;
            pm.shield.SetActive(true);
            Destroy(gameObject);
        }
   }
}
