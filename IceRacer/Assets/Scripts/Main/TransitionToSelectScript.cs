using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionToSelectScript : MonoBehaviour
{
    public GameObject EndScreen;
    private AudioMaster ass;

    void Start()
    {
        ass = GameObject.Find("AudioMaster").GetComponent<AudioMaster>();
    }

    public void DeleteTransition()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().gs = GameState.SelectScreen;
    }

    public void ChangeToEndScene()
    {
        ass.GameOver();
        EndScreen.SetActive(true);
    }

    public void GoToSelect()
    {
        EndScreen.SetActive(false);
        SceneManager.LoadScene("MainScene");
    }
    
}
