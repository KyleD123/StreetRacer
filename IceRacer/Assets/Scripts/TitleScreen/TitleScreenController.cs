using UnityEngine;
using UnityEngine.UI;

public class TitleScreenController : MonoBehaviour
{
    [SerializeField] private Sprite pressedSprite; 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            gameObject.GetComponent<Button>().onClick.Invoke();
            gameObject.GetComponent<Image>().sprite = pressedSprite;
        }
    }


    public void StartGame()
    {
        GameObject.Find("Transition").GetComponent<Animator>().Play("Transition");
    }
}
