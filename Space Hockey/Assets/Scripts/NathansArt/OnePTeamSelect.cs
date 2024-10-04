using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnePlayerTeamSelect : MonoBehaviour
{
    [SerializeField]
    GameObject[] p1Teams = new GameObject[4];
    [SerializeField]
    GameObject[] menuIndicators = new GameObject[2];
    int p1ActiveTeam = 0;
    int partOfMenu = 1;
    [SerializeField]
    SOPlayers savePlayers;
    [SerializeField]
    OnePlayerTeamInput inputControl;
    // Start is called before the first frame update

    public void HandleInputOne(Vector2 input)
    {
        if (input.x != 0)
        {
            SwitchTeam((int)input.x);
        }
        if (input.y != 0)
        {
            SwitchSelect(-(int)input.y);
        }
    }
    public void SwitchSelect(int direction)
    {
        menuIndicators[partOfMenu - 1].SetActive(false);
        partOfMenu += direction;
        if (partOfMenu < 1)
        {
            partOfMenu = 2;
        }
        if (partOfMenu > 2)
        {
            partOfMenu = 1;
        }
        menuIndicators[partOfMenu - 1].SetActive(true);
    }
    public void SwitchTeam(int direction)
    {
        if (partOfMenu == 1)
        {
            p1Teams[p1ActiveTeam].SetActive(false);
            p1ActiveTeam += direction;
            if (p1ActiveTeam < 0)
            {
                p1ActiveTeam = 3;
            }
            if (p1ActiveTeam > 3)
            {
                p1ActiveTeam = 0;
            }
            p1Teams[p1ActiveTeam].SetActive(true);
        }
    }
   
    public void LaunchGame()
    {
        if (partOfMenu == 2)
        {
            savePlayers.p1Team = p1ActiveTeam;
            inputControl.DisableMenuCtrl();
            SceneManager.LoadScene("OnePlayerPlay");
        }
    }
}
