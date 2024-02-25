using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RetryButton : MonoBehaviour
{
    public GameObject transition;
    public Sprite clickedSprite;
    private AudioMaster ass;

    // Start is called before the first frame update
    void Start()
    {
        ass = GameObject.Find("AudioMaster").GetComponent<AudioMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Keypad5))
        { 
            ass.PressButton();
            gameObject.GetComponent<Image>().sprite = clickedSprite;
            gameObject.GetComponent<Button>().onClick.Invoke(); 
        }
    }


    public void ReloadScene()
    {
        transition.GetComponent<Animator>().Play("TransitionToRetry");
    }

}
