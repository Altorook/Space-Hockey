using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("---P1---")]
    [SerializeField] private GameObject p1;
    [SerializeField] private GameObject p1Puck;
    [SerializeField] private GameObject p1Goal;

    [Header("---P2---")]
    [SerializeField] private GameObject p2;
    [SerializeField] private GameObject p2Puck;
    [SerializeField] private GameObject p2Goal;
    private int randomPlayer;

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
            p1Puck.SetActive(true);
        }
        else
        {
            p2Puck.SetActive(true);
        }
    }
}
