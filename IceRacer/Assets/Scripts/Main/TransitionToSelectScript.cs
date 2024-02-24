using UnityEngine;

public class TransitionToSelectScript : MonoBehaviour
{
    public void DeleteTransition()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().gs = GameState.SelectScreen;
    }
}
