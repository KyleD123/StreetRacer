using UnityEngine;

public class TransitionToSelectScript : MonoBehaviour
{
    public void DeleteTransition()
    {
        gameObject.SetActive(false);
        GameObject.Find("GameManager").GetComponent<GameManager>().gs = GameState.SelectScreen;
    }
}
