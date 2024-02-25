using UnityEngine;

public class ShieldPowerup : MonoBehaviour
{
   private PlayerMovement pm;
   private AudioMaster ass;

   void Start()
   {
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
        ass = GameObject.Find("AudioMaster").GetComponent<AudioMaster>();
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
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            ass.PickUp(3);
            pm.shieldActive = true;
            pm.shield.SetActive(true);
            Destroy(gameObject, 1);
        }
   }
}
