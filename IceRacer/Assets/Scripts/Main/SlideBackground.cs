using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlideBackground : MonoBehaviour
{
    private Vector3 pos1;
    private Vector3 pos2 = new Vector3(0,0,0);
    private Vector3 pos3 = new Vector3(-168,0,0);
    private GameManager gm;
    public int timeBeforeSlide = 10;
    public Coroutine co;
    public GameObject borderDark;
    public GameObject driveDark;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        this.pos1 = this.transform.position;
    }

    void Update()
    {
        if(gm.gs == GameState.GamePlay && co == null)
        {
            co = StartCoroutine(StartSlide());
        }
    }

    IEnumerator StartSlide()
    {
        yield return new WaitForSeconds(timeBeforeSlide);
        yield return StartCoroutine(Slide1());
    }

    IEnumerator Slide1()
    {
        float timeElapsed = 0;
        while(Mathf.Abs(this.transform.position.x - pos2.x) > 0.01f)
        {
            if(timeElapsed < 1)
            {
                timeElapsed += Time.deltaTime;
                this.transform.position = Vector3.Lerp(pos1, pos2, timeElapsed / 1);
                yield return null;
            }
        }

        gm.Day = false;
        driveDark.SetActive(false);
        //borderDark.SetActive(false);
        yield return new WaitForSeconds(timeBeforeSlide);
        yield return StartCoroutine(Slide2());
    }

    IEnumerator Slide2()
    {
        float timeElapsed = 0;
        while(Mathf.Abs(this.transform.position.x - pos3.x) > 0.01f)
        {
            if(timeElapsed < 1)
            {
                timeElapsed += Time.deltaTime;
                this.transform.position = Vector3.Lerp(pos2, pos3, timeElapsed / 1);
                yield return null;
            }
        }
        
        this.transform.position = pos1;

        gm.Day = true;
        driveDark.SetActive(true);
        //borderDark.SetActive(true);
        yield return new WaitForSeconds(timeBeforeSlide);
        yield return StartCoroutine(Slide1());
    }

}
