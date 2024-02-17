using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject EnemyPrefab;

    private Coroutine co1;

    // Start is called before the first frame update
    void Start()
    {
        co1 = StartCoroutine(SpawnCars());
    }

    // Update is called once per frame
    void Update()
    {
        if(co1 == null)
        {
            StartCoroutine(SpawnCars());
        }
    }

    IEnumerator SpawnCars()
    {
        yield return new WaitForSeconds(3);
        while(true)
        {
            float x = 45;
            float y = Random.Range(-18, 18);
            SpawnEnemyCar(x,y);
            yield return new WaitForSeconds(3);
        }
    }

    public GameObject SpawnEnemyCar(float x, float y)
    {
        return Instantiate(EnemyPrefab, new Vector3(x,y,0), Quaternion.identity);
    }

}