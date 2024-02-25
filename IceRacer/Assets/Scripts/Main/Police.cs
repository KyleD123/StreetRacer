using System.Collections;
using UnityEngine;

public class Police : MonoBehaviour
{

    private float timeSpend;
    private PlayerMovement pm;
    private GameManager gm;
    private Vector3 StartPos = new Vector3(70f, 0f, 0f);

    private GameObject freezeSprite;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
        freezeSprite = transform.GetChild(1).gameObject;
        pm.shieldActive = false;
        pm.StartCoroutine("DisableShield");
        StartCoroutine(FlashFreeze());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,pm.transform.position, 0.05f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            pm.transform.GetChild(0).gameObject.SetActive(false);
            pm.anime.SetTrigger("Dead");
            gm.gs = GameState.EndScreen;
            gm.gameOver = true;
            Destroy(this.gameObject);
        }
    }

    IEnumerator FlashFreeze()
    {
        int count = 1;
        for (int i = 0; i < 7; i++)
        {
            freezeSprite.SetActive(false);
            yield return new WaitForSeconds(0.3f);
            freezeSprite.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            count++;
        }

        freezeSprite.SetActive(false);
    }

}
