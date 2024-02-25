using UnityEngine;
using UnityEngine.UI;

public class TitleScreenController : MonoBehaviour
{
    [SerializeField] private Sprite pressedSprite; 
    private AudioMaster ass;

    void Start()
    {
        ass = GameObject.Find("AudioMaster").GetComponent<AudioMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            ass.StartGame();
            gameObject.GetComponent<Button>().onClick.Invoke();
            gameObject.GetComponent<Image>().sprite = pressedSprite;
        }
    }


    public void StartGame()
    {
        GameObject.Find("Transition").GetComponent<Animator>().Play("Transition");
    }
}
