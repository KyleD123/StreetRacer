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
    private Coroutine co2;

    public GameObject GroundMarkLight1, GroundMarkLight2;
    public GameObject GroundMarkDark1, GroundMarkDark2;

    private PlayerMovement pm;

    [SerializeField] private int playerIndex;

    private bool SelectionState;
    private bool GameState;

    public bool Day = true;

    [SerializeField] private MoveableObjectManager PowerUpMan;

    [SerializeField] private float GroundSpawnRate = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        PowerUpMan = GameObject.Find("PowerUpManager").GetComponent<MoveableObjectManager>();
        // Do character select stuff before starting game
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(co1 == null)
        {
            co1 = StartCoroutine(Spawner());
        }
        if(co2 == null)
        {
            co2 = StartCoroutine(GroundMarkSpawner());
        }
    }

    public void StartGame()
    {
        SpawnPlayer(playerIndex);
        pm = FindObjectOfType<PlayerMovement>();
        co1 = StartCoroutine(Spawner());
        co2 = StartCoroutine(GroundMarkSpawner());
    }

    IEnumerator Spawner()
    {
        yield return new WaitForSeconds(3);
        while(true)
        {
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

    IEnumerator GroundMarkSpawner()
    {
        while(true)
        {
            yield return new WaitForSeconds(GroundSpawnRate);
            if(Day)
            {
                if (pm.PlayerCurrentSpeed > 5f)
                {
                    GameObject mark = SpawnGroundMarkDark();
                    PowerUpMan.MoveableObjectList.Add(mark);
                }

            }
            else
            {
                if (pm.PlayerCurrentSpeed > 5f)
                {
                    GameObject mark = SpawnGroundMarkLight();
                    PowerUpMan.MoveableObjectList.Add(mark);
                }
            }
            yield return new WaitForSeconds(GroundSpawnRate);
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
            PowerUpMan.MoveableObjectList.Add(GOReturn);
        }
        else
        {
            GOReturn = Instantiate(SpeedUpPrefab, new Vector3(x,y,0), Quaternion.identity);
            PowerUpMan.MoveableObjectList.Add(GOReturn);
        }

        return GOReturn;
    }

    public GameObject SpawnGroundMarkLight()
    {
        float y = Random.Range(-16.5f,16.5f);
        float x = 55;

        float rnd = Random.Range(1, 100);
        if (rnd < 50) return Instantiate(GroundMarkLight1, new Vector3(x,y,0), Quaternion.identity);
        return Instantiate(GroundMarkLight2, new Vector3(x,y,0), Quaternion.identity);
        
    }

    public GameObject SpawnGroundMarkDark()
    {
        float y = Random.Range(-16.5f,16.5f);
        float x = 55;

        float rnd = Random.Range(1, 100);
        if (rnd < 50) return Instantiate(GroundMarkDark1, new Vector3(x,y,0), Quaternion.identity);
        return Instantiate(GroundMarkDark2, new Vector3(x,y,0), Quaternion.identity);
    }


    public void SpawnPlayer(int index)
    {
        GameObject player =  Instantiate(PlayerPrefabs[index], new Vector3(0, 0, 0), Quaternion.identity);
        player.name = "Player";

    }

}
