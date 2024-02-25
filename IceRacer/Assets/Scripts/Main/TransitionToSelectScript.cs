using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionToSelectScript : MonoBehaviour
{
    public GameObject EndScreen;

    public void DeleteTransition()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().gs = GameState.SelectScreen;
    }

    public void ChangeToEndScene()
    {
        EndScreen.SetActive(true);
    }

    public void GoToSelect()
    {
        EndScreen.SetActive(false);
        SceneManager.LoadScene("MainScene");
    }
    
}
