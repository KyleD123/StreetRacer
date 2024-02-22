using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectManager : MonoBehaviour
{
    public GameManager gm;
    public Button rightBtn;
    public Button leftBtn;
    public int index = 0;

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = Instantiate(gm.PlayerPrefabs[index], new Vector3(-30,0,0), Quaternion.identity);
        player.name = "Player";
    }

    public void MoveToNext()
    {
        if(player != null)
        {
            Destroy(player);
        }
        index += 1;
        if(index > 2) index = 0;
        player = Instantiate(gm.PlayerPrefabs[index], new Vector3(-30,0,0), Quaternion.identity);
        player.name = "Player";
    }

    public void MoveToPrev()
    {
        if(player != null)
        {
            Destroy(player);
        }
        index -= 1;
        if(index < 0) index = 2;
        player = Instantiate(gm.PlayerPrefabs[index], new Vector3(-30,0,0), Quaternion.identity);
        player.name = "Player";
    }

    public int SelectCar()
    {
        return index;
    }
}
