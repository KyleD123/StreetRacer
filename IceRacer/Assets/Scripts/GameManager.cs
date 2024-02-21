using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject[] EnemyPrefabs;
    public GameObject[] PlayerPrefabs;
    public GameObject JerryCanPrefab;
    public GameObject SpeedUpPrefab;
    private Coroutine co1;

    public GameObject GroundMarkLight;
    public GameObject GroundMarkDark;

    private PlayerMovement pm;

    [SerializeField] private int playerIndex;

    private bool SelectionState;
    private bool GameState;

    [SerializeField] private PowerUpManager PowerUpMan;

    // Start is called before the first frame update
    void Start()
    {
        PowerUpMan = GameObject.Find("PowerUpManager").GetComponent<PowerUpManager>();
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
            SpawnGroundMarkLight();
            float x = Random.Range(55, 70);
            if(pm.PlayerCurrentSpeed <= 0)
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
        int rnd = Random.Range(0, EnemyPrefabs.Length);
        return Instantiate(EnemyPrefabs[rnd], new Vector3(x,y,0), Quaternion.identity);
    }

    public GameObject SpawnPowerUp(float x, float y)
    {
        GameObject GOReturn = null;
        float rnd = Random.Range(0,100);
        if(rnd > 50)
        {
            GOReturn = Instantiate(JerryCanPrefab, new Vector3(x,y,0), Quaternion.identity);
            PowerUpMan.PowerUpList.Add(GOReturn);
        }
        else
        {
            GOReturn = Instantiate(SpeedUpPrefab, new Vector3(x,y,0), Quaternion.identity);
            PowerUpMan.PowerUpList.Add(GOReturn);
        }

        return GOReturn;
    }

    public void SpawnGroundMarkLight()
    {
        float y = Random.Range(-16.5f,16.5f);
        float x = 55;
        Instantiate(GroundMarkLight, new Vector3(x,y,0), Quaternion.identity);
    }

    public void SpawnGroundMarkDark()
    {
        float y = Random.Range(-16.5f,16.5f);
        float x = 55;
        Instantiate(GroundMarkDark, new Vector3(x,y,0), Quaternion.identity);
    }


    public void SpawnPlayer(int index)
    {
        GameObject player =  Instantiate(PlayerPrefabs[index], new Vector3(0, 0, 0), Quaternion.identity);
        player.name = "Player";

    }

}
