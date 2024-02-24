using System.Collections;
using UnityEngine;

public class FreezeScript : MonoBehaviour
{
    [SerializeField] float reactionTime = 3f;
    private float currentCount = 0f;
    private bool itsFreezingTime = false;
    [SerializeField]private PlayerMovement pm;
    public GameObject policePrefab;

    private Vector2 StopSignEndPosition = new Vector2(23,-1);

    private Animator anime;
    private GameManager gm;

    void Start()
    {
        gm = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        anime = GetComponent<Animator>();
    }

    void Update()
    {
        if(gm.gs != GameState.EndScreen)
        {
            if(!pm)
            {
                pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            }
        }
    }

    public void CallFreezeEvent()
    {
        StartCoroutine(FreezeCounter());
    }

    IEnumerator FreezeCounter()
    {
        anime.SetTrigger("SlideIn");
        currentCount = 0;
        itsFreezingTime = true;
        // Count until reaction time.
        // If button is pressed fast enough, itsfreezingtime is set to false and the player
        // is not dead
        while (currentCount <= reactionTime)
        {
            currentCount += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Keypad0))
            { itsFreezingTime = false; break; }
            yield return null;
        }
        
        anime.SetTrigger("SlideOut");

        // Spawn the fuckin cops and stop other shit cuz its freezing time.
        // The cops then freezed all over
        if (itsFreezingTime)
        {
            gm.failedToFreeze = true;
            Instantiate(policePrefab, new Vector3(70f, 0f, 0f), Quaternion.identity);
        }
        else
        {
            gm.failedToFreeze = false;
            pm.PlayerCurrentSpeed = 0;
        }
        
        yield return null;

    }
}
