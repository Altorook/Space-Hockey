using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("---Puck---")]
    [SerializeField] private Puck puck;
    [SerializeField] private Transform puckPos;

    [Header("---Player Stat---")]
    [SerializeField] private string playerName;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float speedIncrease;
    [SerializeField] SpeedBar speedBar;

    private Rigidbody2D rb;
    private Vector2 _movement;
    private bool isPuckHere;
    private bool canShoot;

    #region Puck Variables
    private float puckSpeed;
    private bool randomSpeed;
    private float speed = 5;
    private float min = 5;
    private float max = 15;
    private bool increase = true;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        speedBar.transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovementHandler();
    }

    private void MovementHandler()
    {
        rb.velocity = _movement * movementSpeed;
    }

    public void MovementInput(Vector2 input)
    {
        _movement = input;
    }

    public void KeepBall()
    {
        if(canShoot == false)
        {
            if (isPuckHere == true)
            {
                puck.gameObject.SetActive(false);
                puckPos.gameObject.SetActive(true);
                canShoot = true;
            }
            else
            {
                return;
            }
        }

        else if(canShoot == true && randomSpeed == false && puckPos.gameObject.activeSelf)
        {
            speedBar.transform.GetChild(0).gameObject.SetActive(true);
            randomSpeed = true;
            speed = 5;
            StartCoroutine(StartRandom());
        }
        else if(canShoot == true && randomSpeed == true && puckPos.gameObject.activeSelf)
        {
            randomSpeed = false;
            if (randomSpeed == false) puckSpeed = speed;
            StopCoroutine(StartRandom());
            puckPos.gameObject.SetActive(false);
            puck.ResetPosition(puckPos);
            puck.gameObject.SetActive(true);
            puck.gameObject.GetComponent<Rigidbody2D>().velocity = puckPos.up * puckSpeed;
            speed = 5;
            canShoot = false;
            speedBar.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private IEnumerator StartRandom()
    {
        while ((randomSpeed == true))
        {
            if (increase)
            {
                speed += speedIncrease;
                if (speed >= max)
                {
                    speed = max;
                    increase = false;
                }
            }
            else
            {
                speed -= speedIncrease;
                if (speed <= min)
                {
                    speed = min;
                    increase = true;
                }
            }
            speedBar.UpdateSpeedBar(speed);
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Puck>())
        {
            isPuckHere = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Puck>())
        {
            isPuckHere = false;
        }
    }

    public void SetCanShoot(bool value)
    {
        canShoot = value;
    }

    public string GetPlayerName()
    {
        return playerName;
    }
}
