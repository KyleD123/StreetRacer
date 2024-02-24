using System.Collections;
using UnityEngine;

public class FreezeScript : MonoBehaviour
{
    [SerializeField] float reactionTime = 3f;
    private float currentCount = 0f;
    private bool itsFreezingTime = false;
    private PlayerMovement pm;

    void Update()
    {
        if (!pm)
        {
            GameObject.Find("Player");
        }
    }

    public void CallFreezeEvent()
    {
        StartCoroutine(FreezeCounter());
    }

    IEnumerator FreezeCounter()
    {
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

        // Spawn the fuckin cops and stop other shit cuz its freezing time.
        // The cops then freezed all over
        if (itsFreezingTime)
        {
            // Stuff that will happen if they dont stop
        }
        else
        {
            // Stuff that will happen if they do stop
            pm.PlayerCurrentSpeed = 0;
        }
        
        yield return null;

    }
}
