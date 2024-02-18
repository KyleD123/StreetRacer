using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject[] EnemyPrefabs;
    public GameObject[] PlayerPrefabs;
    public GameObject JerryCanPrefab;
    public GameObject SpeedUpPrefab;
    private Coroutine co1;

    private PlayerMovement pm;

    [SerializeField]private int playerIndex;

    private bool SelectionState;
    private bool GameState;

    // Start is called before the first frame update
    void Start()
    {
        // Do character select stuff before starting game
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(co1 == null)
        {
            StartCoroutine(Spawner());
        }
    }

    public void StartGame()
    {
        SpawnPlayer(playerIndex);
        pm = FindObjectOfType<PlayerMovement>();
        co1 = StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        yield return new WaitForSeconds(3);
        while(true)
        {
            float x = Random.Range(55, 70);
            if(pm.KMpH <= 0)
            {
                x = Random.Range(-60, -54);
            }
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
        int rnd = Random.Range(0,3);
        Debug.Log(rnd);
        return Instantiate(EnemyPrefabs[rnd], new Vector3(x,y,0), Quaternion.identity);
    }

    public GameObject SpawnPowerUp(float x, float y)
    {
        float rnd = Random.Range(0,100);
        if(rnd > 50)
        {
            return Instantiate(JerryCanPrefab, new Vector3(x,y,0), Quaternion.identity);
        }
        else
        {
            return Instantiate(SpeedUpPrefab, new Vector3(x,y,0), Quaternion.identity);
        }
    }


    public void SpawnPlayer(int index)
    {
        Instantiate(PlayerPrefabs[index], new Vector3(-30, 0, 0), Quaternion.identity);
    }

}
