using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("---Puck---")]
    [SerializeField] private Puck puck;
    [SerializeField] private GameObject arena;

    [Header("---Player---")]
    [SerializeField] private GameObject p1;
    [SerializeField] private GameObject p2;

    [Header("---Score---")]
    [SerializeField] private int p1Score;
    [SerializeField] private int p2Score;
    [SerializeField] private TMP_Text p1ScoreText;
    [SerializeField] private TMP_Text p2ScoreText;
    [SerializeField] private TMP_Text gameResultText;

    [Header("---Obstacles---")]
    [SerializeField] private GameObject[] obstacles;

    private int randomPlayer;
    private GameObject playerPuck;
    private string winResult;
    private int currentLevel;
    private Vector2 p1StartPoint;
    private Vector2 p2StartPoint;
    private int obstacleCount;

    private void Awake()
    {
        if(p2.gameObject.GetComponent<PlayerController>().GetPlayerName() == "AI")
        {
            Destroy(p2.gameObject.GetComponent<PlayerController>());
            Destroy(p2.gameObject.GetComponent<PlayerInputManager>());
        }
        else
        {
            Destroy(p2.gameObject.GetComponent<AI>());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        p1StartPoint = p1.gameObject.transform.position;
        p2StartPoint = p2.gameObject.transform.position;
        InitializedGame();

        p1ScoreText.text = p1Score.ToString();
        p2ScoreText.text = p1Score.ToString();
        gameResultText.gameObject.SetActive(false);
        currentLevel = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializedGame()
    {
        randomPlayer = Random.Range(1, 3);
        if (randomPlayer == 1) p2.gameObject.GetComponent<AI>().SetPuckCheck(1);
        if (p2.gameObject.GetComponent<AI>() != null)
        {
            p2.gameObject.GetComponent<AI>().SetCanShoot(false);
            p2.gameObject.GetComponent<AI>().SetIsHit(false);
        }

        Invoke("RandomPlayer", 3);
        p2.gameObject.GetComponent<AI>().SetPuckCheck(0);
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
            if(p2.gameObject.GetComponent<PlayerController>() != null) p2.gameObject.GetComponent<PlayerController>().SetCanShoot(true);
            else
            {
                p2.gameObject.GetComponent<AI>().SetCanShoot(true);
                p2.gameObject.GetComponent<AI>().SetIsHit(true);
                p2.gameObject.GetComponent<AI>().SetStartCount(true);
            }
        }
    }

    public void PlayerScored(string player)
    {
        if (player == "Player1")
        {
            playerPuck = p1.transform.GetChild(0).gameObject;
            p2Score += 1;
            p1ScoreText.text = p2Score.ToString();
            CheckWinCondition();
            if (winResult != null)
            {
                EndGame();
            }
            else
            {
                puck.ResetPosition(playerPuck.transform);
                Invoke("ActivePuck", 2);
                p1.gameObject.GetComponent<PlayerController>().SetCanShoot(true);
                if (p2.gameObject.GetComponent<AI>() != null)
                {
                    p2.gameObject.GetComponent<AI>().SetStartCount(false);
                }
            }
        }
        else
        {
            playerPuck = p2.transform.GetChild(0).gameObject;
            p1Score += 1;

            p2ScoreText.text = p1Score.ToString();
            CheckWinCondition();
            if (winResult != null)
            {
                EndGame();
            }
            else
            {
                puck.ResetPosition(playerPuck.transform);
                Invoke("ActivePuck", 2);
                if (p2.gameObject.GetComponent<PlayerController>() != null) p2.gameObject.GetComponent<PlayerController>().SetCanShoot(true);
                else
                {
                    p2.gameObject.GetComponent<AI>().SetCanShoot(true);
                    p2.gameObject.GetComponent<AI>().SetIsHit(true);
                }
                if (p2.gameObject.GetComponent<AI>() != null)
                {
                    p2.gameObject.GetComponent<AI>().SetStartCount(false);
                }
            }
            
        }
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
        puck.gameObject.SetActive(false);
        p1.SetActive(false);
        p2.SetActive(false);
        p1ScoreText.gameObject.SetActive(false);
        p2ScoreText.gameObject.SetActive(false);
        arena.gameObject.SetActive(false);
        gameResultText.text = $"{winResult} Win";
        gameResultText.gameObject.SetActive(true);
        HideObstacle();
        Debug.Log(winResult);
        StartCoroutine(GameSet());
    }

    private IEnumerator GameSet()
    {
        yield return new WaitForSeconds(3);
        if (p2.gameObject.GetComponent<PlayerController>() == null)
        {
            if(winResult == "Player 1")
            {
                currentLevel++;
                if (currentLevel >= 4)
                {
                    //Script to Go back to main menu
                    SceneManager.LoadScene("SampleScene");
                }
                else
                {
                    p1.transform.position = p1StartPoint;
                    p2.transform.position = p2StartPoint;
                    p1.SetActive(true);
                    p2.SetActive(true);
                    p1ScoreText.gameObject.SetActive(true);
                    p2ScoreText.gameObject.SetActive(true);
                    arena.gameObject.SetActive(true);
                    gameResultText.gameObject.SetActive(false);
                    p1Score = 0;
                    p1ScoreText.text = p2Score.ToString();
                    p2Score = 0;
                    p2ScoreText.text = p1Score.ToString();

                    if (currentLevel == 2)
                    {
                        obstacles[obstacleCount].gameObject.SetActive(true);
                    }
                    else
                    {
                        obstacles[obstacleCount].gameObject.SetActive(false);
                        obstacleCount++;
                        obstacles[obstacleCount].gameObject.SetActive(true);
                    }
                    InitializedGame();
                }
                
                
                p2.GetComponent<AI>().SetCurrentTeam();
                winResult = null;
            }
            else
            {
                //Script to Go back to main menu
                SceneManager.LoadScene("SampleScene");
            }
        }
        if (p2.gameObject.GetComponent<PlayerController>() != null)
        {
            if(winResult == "Player 1" || winResult == "Player 2")
            {
                //Script to Go back to main menu
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

    private void HideObstacle()
    {
        foreach(var obstacle in obstacles)
        {
            obstacle.gameObject.SetActive(false);
        }
    }
}
