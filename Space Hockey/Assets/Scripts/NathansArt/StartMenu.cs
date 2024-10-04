using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField]
    GameObject[] indicators;
    bool selectOnePlayer = true;
    [SerializeField]
    StartMenuInput startMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void HandleInput(Vector2 inputed)
    {
        if(inputed.y != 0)
        {
           SwitchSelectPlayers();
        }
    }
    public void SwitchSelectPlayers()
    {
        selectOnePlayer = !selectOnePlayer;
        if (selectOnePlayer)
        {
            indicators[0].SetActive(true);
            indicators[1].SetActive(false);
        }
        else
        {
            indicators[1].SetActive(true);
            indicators[0].SetActive(false);
        }
    }
    public void GoToTeamSelect()
    {
        startMenu.DisableMenuCtrl();
        if (selectOnePlayer)
        {
            SceneManager.LoadScene("SinglePlayerTeamSelect");
        }
        else
        {
            SceneManager.LoadScene("2PlayerTeamSelect");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
