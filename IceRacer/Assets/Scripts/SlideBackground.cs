using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideBackground : MonoBehaviour
{

    //Position one: x 168
    //Position two: x 0
    //Position three: x -168

    private Vector3 pos1;
    private Vector3 pos2 = new Vector3(0,0,0);
    private Vector3 pos3 = new Vector3(-168,0,0);


    public int timeBeforeSlide = 10;

    // Start is called before the first frame update
    void Start()
    {
        this.pos1 = this.transform.position;
        SlideToPosition2();
    }

    public void SlideToPosition2()
    {
        StartCoroutine(Slide1());
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

        yield return new WaitForSeconds(timeBeforeSlide);

        yield return StartCoroutine(Slide1());
    }

}
