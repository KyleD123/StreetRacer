using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    TitleScreen,
    SelectScreen,
    GamePlay,
    Pause,
    EndScreen
}

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
    public PlayerMovement pm;
    [SerializeField] private int playerIndex;
    private bool SelectionState;
    public bool Day = true;
    [SerializeField] private MoveableObjectManager PowerUpMan;
    [SerializeField] private float GroundSpawnRate = 0.25f;
    public GameState gs;
    public GameObject driveBtn, rightBtn, leftBtn;
    private bool rightPressed = false;
    private bool leftPressed = false;
    public SelectManager sm;
    [SerializeField] private Sprite[] btnSprites;
    [SerializeField] private Animator animator;
    public bool carSelected = false;
    private Text countDown;
    public float kmTraveled = 0;

    // Start is called before the first frame update
    void Start()
    {
        PowerUpMan = GameObject.Find("PowerUpManager").GetComponent<MoveableObjectManager>();
        gs = GameState.SelectScreen;
        driveBtn.GetComponent<Button>().onClick.AddListener(SelectPlayerCar);
    }

    // Update is called once per frame
    void Update()
    {
        if (gs == GameState.SelectScreen)
        {
            SelectScreenInput();
        }

        if (gs == GameState.GamePlay)
        {
            IncreaseKM();

            if(co1 == null)
            {
                co1 = StartCoroutine(Spawner());
            }
            if(co2 == null)
            {
                co2 = StartCoroutine(GroundMarkSpawner());
            }
        }

        
    }

    private void SelectScreenInput()
    {
        if (!carSelected)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Keypad6))
            {
                rightBtn.GetComponent<Button>().onClick.Invoke();
                rightPressed = true;
            }
            if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.Keypad6)) rightPressed = false;

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Keypad4))
            {
                leftBtn.GetComponent<Button>().onClick.Invoke();
                leftPressed = true;
            }
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.Keypad4)) leftPressed = false;

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyUp(KeyCode.Keypad5))
            { driveBtn.GetComponent<Button>().onClick.Invoke(); }

            UpdateButtonImage();
        }
        
    }

    void UpdateButtonImage()
    {
        if (rightPressed)
        {
            rightBtn.GetComponent<Image>().sprite = btnSprites[3];
        }
        else
        {
            rightBtn.GetComponent<Image>().sprite = btnSprites[2];
        }

        if (leftPressed)
        {
            leftBtn.GetComponent<Image>().sprite = btnSprites[1];
        }
        else
        {
            leftBtn.GetComponent<Image>().sprite = btnSprites[0];
        }
    }

    public void StartGame()
    {
        pm.PlayerCurrentSpeed = 0;
        gs = GameState.GamePlay;
        pm = FindObjectOfType<PlayerMovement>();
        co1 = StartCoroutine(Spawner());
        co2 = StartCoroutine(GroundMarkSpawner());
    }

    public void SelectPlayerCar()
    {
        if (!carSelected)
        {
            carSelected = true;
            animator.SetBool("SlideSelect", true);
            playerIndex = sm.SelectCar();
        }   
    }

    private void IncreaseKM()
    {
        kmTraveled += pm.PlayerCurrentSpeed / 1500;
        countDown.text = Mathf.RoundToInt(kmTraveled).ToString() + " km";
    }

    public void StartSelectCoroutine()
    {
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        countDown = GameObject.Find("Drive!").GetComponent<Text>();
        float timeSpent = 0f;
        while(pm.transform.position.x < 0f)
        {
            if(timeSpent < 1)
            {
                if (timeSpent > 0.5) countDown.text = "SET";
                if (timeSpent > 0.9) countDown.text = "GO!";

                timeSpent += Time.deltaTime;
                pm.transform.position = Vector3.Lerp(new Vector3(-26f, 2f, 0f), Vector3.zero, timeSpent / 1 );
                yield return new WaitForSeconds(0.005f);
            }
            
        }
        transform.position = Vector3.zero;
        StartGame();
        yield return new WaitForSeconds(1f);
        countDown.text = kmTraveled.ToString();
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
            if(CarOrPowerUp >= 25 && !pm.FAMILY)
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
        else if (!pm.FAMILY)
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
