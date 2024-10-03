using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    [SerializeField]
    GameObject[] p1Teams = new GameObject[4];
    [SerializeField]
    GameObject[] p2Teams = new GameObject[4];
    [SerializeField]
    GameObject[] menuIndicators = new GameObject[3];    
    int p1ActiveTeam = 0;
    int p2ActiveTeam = 1;
    int partOfMenu = 1;
    [SerializeField]
    SOPlayers savePlayers;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void HandleInput(Vector2 input)
    {
        if(input.x != 0)
        {
            SwitchP1Team((int)input.x);
            SwitchP2Team((int)input.x);
        }
        if(input.y != 0)
        {
            SwitchTeamPlay(-(int)input.y);
        }
    }
    public void SwitchTeamPlay(int direction)
    {
        menuIndicators[partOfMenu - 1].SetActive(false);
        partOfMenu += direction;
        if (partOfMenu < 1)
        {
            partOfMenu = 3;
        }
        if(partOfMenu > 3)
        {
            partOfMenu = 1;
        }
        menuIndicators[partOfMenu-1].SetActive(true);
    }
    public void SwitchP1Team(int direction)
    {
        if(partOfMenu == 1)
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
    public void SwitchP2Team(int direction)
    {
        if (partOfMenu == 2)
        {
            p2Teams[p2ActiveTeam].SetActive(false);
            p2ActiveTeam += direction;
            if (p2ActiveTeam < 0)
            {
                p2ActiveTeam = 3;
            }
            if (p2ActiveTeam > 3)
            {
                p2ActiveTeam = 0;
            }
            p2Teams[p2ActiveTeam].SetActive(true);
        }

    }
    public void LaunchGameAsPlayers()
    {
        if(partOfMenu == 3)
        {
            savePlayers.p1Team = p1ActiveTeam;
            savePlayers.p2Team = p2ActiveTeam;
        }
    }
}
