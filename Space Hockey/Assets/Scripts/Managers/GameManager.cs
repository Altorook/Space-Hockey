using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("---Puck---")]
    [SerializeField] private Puck puck;

    [Header("---Player---")]
    [SerializeField] private GameObject p1;
    [SerializeField] private GameObject p2;

    [Header("---Score---")]
    [SerializeField] private int p1Score;
    [SerializeField] private int p2Score;

    private int randomPlayer;
    private GameObject playerPuck;
    private string winResult;

    // Start is called before the first frame update
    void Start()
    {
        randomPlayer = Random.Range(1, 3);
        Debug.Log(randomPlayer);
        InitializedGame();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitializedGame()
    {
        Invoke("RandomPlayer", 3);
    }

    private void RandomPlayer()
    {
        if (randomPlayer == 1)
        {
            playerPuck = p1.transform.GetChild(0).gameObject;
            playerPuck.SetActive(true);
            p1.gameObject.GetComponent<PlayerController>().SetCanShoot(true);
        }
        else
        {
            playerPuck = p2.transform.GetChild(0).gameObject;
            playerPuck.SetActive(true);
            p2.gameObject.GetComponent<PlayerController>().SetCanShoot(true);
        }
    }

    public void PlayerScored(string player)
    {
        if(player == "Player1")
        {
            playerPuck = p1.transform.GetChild(0).gameObject;
            p2Score += 1;
            puck.ResetPosition(playerPuck.transform);
            Invoke("ActivePuck", 2);
            p1.gameObject.GetComponent<PlayerController>().SetCanShoot(true);
        }
        else
        {
            playerPuck = p2.transform.GetChild(0).gameObject;
            p1Score += 1;
            puck.ResetPosition(playerPuck.transform);
            Invoke("ActivePuck", 2);
            p2.gameObject.GetComponent<PlayerController>().SetCanShoot(true);
        }
        CheckWinCondition();
        if(winResult != null) EndGame();
    }

    private void ActivePuck()
    {
        playerPuck.SetActive(true);
    }

    private void CheckWinCondition()
    {
        if (p1Score == 5 && p2Score < 4) winResult = "Player 1";
        else if (p1Score < 4 && p2Score == 5) winResult = "Player 2";
        else if (p1Score >= 4 && p2Score >= 4)
        {
            if (p1Score - p2Score == 2) winResult = "Player 1";
            else if (p2Score - p1Score == 2) winResult = "Player 2";
        }
        else return;
    }

    private void EndGame()
    {
        p1.SetActive(false);
        p2.SetActive(false);
        GameObject.Find("Arena").SetActive(false);
        Debug.Log(winResult);
    }
}
