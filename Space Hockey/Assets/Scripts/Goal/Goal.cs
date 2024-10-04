using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private string playerGoal;

    private GameManager gm;
    private void Awake()
    {
        gm = GameObject.Find("[GameManager]").GetComponent<GameManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Puck>())
        {
            SoundManager.Instance.PlaySFX("Buzzer");
            if (playerGoal == "Player1")
            {
                gm.PlayerScored(playerGoal);
            }
            else
            {
                gm.PlayerScored(playerGoal);
            }
        }
    }
}
