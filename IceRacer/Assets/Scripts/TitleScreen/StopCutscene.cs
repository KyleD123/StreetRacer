using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCutscene : MonoBehaviour
{
    public void DisableCutsceneComp()
    {
        GameObject.Find("Button").GetComponent<TitleScreenController>().Cutscene = false;
        gameObject.SetActive(false);
    }
}
