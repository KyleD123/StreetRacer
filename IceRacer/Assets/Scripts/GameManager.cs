using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject EnemyPrefab;
    public GameObject JerryCanPrefab;
    public GameObject SpeedUpPrefab;
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
            float x = Random.Range(55, 70);
            float y = Random.Range(-18, 18);
            float CarOrPowerUp = Random.Range(0,100);
            if(CarOrPowerUp >= 25)
            {
                SpawnEnemyCar(x,y);    
            }
            else
            {
                SpawnPowerUp(x,y);
            }
            yield return new WaitForSeconds(3);
        }
    }

    public GameObject SpawnEnemyCar(float x, float y)
    {
        return Instantiate(EnemyPrefab, new Vector3(x,y,0), Quaternion.identity);
    }

    public GameObject SpawnPowerUp(float x, float y)
    {
        float rnd = Random.Range(0,100);
        if(rnd > 35)
        {
            return Instantiate(JerryCanPrefab, new Vector3(x,y,0), Quaternion.identity);
        }
        else
        {
            return Instantiate(SpeedUpPrefab, new Vector3(x,y,0), Quaternion.identity);
        }
    }

}
