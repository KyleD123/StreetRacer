using UnityEngine;
using UnityEngine.UI;

public class SelectManager : MonoBehaviour
{
    public GameManager gm;
    public GameObject fence;
    public Button rightBtn;
    public Button leftBtn;
    public int index = 0;

    [SerializeField] private Sprite[] cards;

    public GameObject currentCard;

    private float startingOffsetX = -15f;
    private float startingOffsetY = 8;

    [SerializeField] private GameObject HUDReference;

    public GameObject player;
    public GameObject sunMoon;
    // Start is called before the first frame update
    void Start()
    {
        player = Instantiate(gm.PlayerPrefabs[index], new Vector3(startingOffsetX, startingOffsetY,0f), Quaternion.identity);
        player.name = "Player";

        gm.pm = player.GetComponent<PlayerMovement>();
    }

    public void MoveToNext()
    {
        if(player != null)
        {
            Destroy(player);
        }
        index += 1;
        if(index > 2) index = 0;
        player = Instantiate(gm.PlayerPrefabs[index], new Vector3(startingOffsetX,startingOffsetY,0f), Quaternion.identity);
        player.name = "Player";

        gm.pm = player.GetComponent<PlayerMovement>();

        currentCard.GetComponent<SpriteRenderer>().sprite = cards[index];
    }

    public void MoveToPrev()
    {
        if(player != null)
        {
            Destroy(player);
        }
        index -= 1;
        if(index < 0) index = 2;
        player = Instantiate(gm.PlayerPrefabs[index], new Vector3(startingOffsetX,startingOffsetY,0f), Quaternion.identity);
        player.name = "Player";

        gm.pm = player.GetComponent<PlayerMovement>();

        currentCard.GetComponent<SpriteRenderer>().sprite = cards[index];
    }

    public int SelectCar()
    {
        return index;
    }

    public void StartOtherStartMethod()
    {
        GameObject.Find("SelectScreen").SetActive(false);
        sunMoon.SetActive(true);
        fence.SetActive(true);
        HUDReference.SetActive(true);
        gm.StartSelectCoroutine();
    }
}
