using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AI : MonoBehaviour
{
    [SerializeField] private Transform puck;
    [SerializeField] private Transform p1;
    [SerializeField] private Transform puckPos;
    [SerializeField] private Transform netPos;
    [SerializeField] private float speed;
    [SerializeField] private Transform goalPos;
    [SerializeField] private Sprite[] team;
    [SerializeField] private SOPlayers soPlayers;

    private int currentTeam = 0;

    private bool canShoot;
    private bool isHit;
    private int randomState;
    [SerializeField] private float distance;
    [SerializeField] private int randomSide;
    private Vector2 startPos;
    private Vector2 target;
    private int puckCheck;
    private float cooldown;
    private bool startCount;

    private void Awake()
    {
        startCount = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        ChangeDirection();
        InvokeRepeating("ChangeDirection", 0.5f, 0.5f);
        cooldown = 5;
        if (currentTeam == soPlayers.p1Team)
        {
            SetCurrentTeam();
        }
        GetComponent<SpriteRenderer>().sprite = team[currentTeam];
    }

    void Update()
    {
        if (puckPos.transform.gameObject.activeSelf == false) ChaseBall();
        else if (puckPos.transform.gameObject.activeSelf == true) ShootBall();
        else return;

        if(startCount == true)
        {
            cooldown -= Time.deltaTime;
        }
        else
        {
            cooldown = 5;
        }
    }

    private void ChaseBall()
    {
        if (puck.position.y > netPos.position.y && puck.gameObject.activeSelf == true ||
            cooldown <= 0)
        {
            speed = Random.Range(3f, 6f);
            Vector2 targetPosition = puck.position;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else if (puck.position.y > netPos.position.y && puck.gameObject.activeSelf == true && isHit == true ||
                 puck.position.y < netPos.position.y && puck.gameObject.activeSelf == true)
        {
            Vector2 targetPosition = new Vector2(puck.position.x, Random.Range(goalPos.position.y - 1f, goalPos.position.y - 1.5f));
            float interceptPosX = Mathf.Clamp(targetPosition.x, goalPos.position.x - 1f, goalPos.position.x + 1f);
            Vector2 interceptPos = new Vector2(interceptPosX, targetPosition.y);
            speed = Random.Range(3f, 6f);
            transform.position = Vector2.MoveTowards(transform.position, interceptPos, speed * Time.deltaTime);
        }
        else if (puck.gameObject.activeSelf == false && puckPos.gameObject.activeSelf == false && canShoot == false && puckCheck == 1 ||
                 p1.transform.GetChild(0).gameObject.activeSelf == true)
        {
            Vector2 targetPosition = new Vector2(p1.position.x, Random.Range(goalPos.position.y - 1f, goalPos.position.y - 1.5f));
            float interceptPosX = Mathf.Clamp(targetPosition.x, goalPos.position.x - 1f, goalPos.position.x + 1f);
            Vector2 interceptPos = new Vector2(interceptPosX, targetPosition.y);
            speed = Random.Range(3f, 6f);
            transform.position = Vector2.MoveTowards(transform.position, interceptPos, speed * Time.deltaTime);
        }
        else
        {
            speed = Random.Range(3f, 6f);
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }


    }
    private void ChangeDirection()
    {
        target = new Vector2(Random.Range(-5f, 5f), 4.27f);
    }

    private void ShootBall()
    {
        Vector2 targetPosition = p1.position;
        speed = Random.Range(5f, 10f);
        puckPos.gameObject.SetActive(false);
        puck.transform.gameObject.GetComponent<Puck>().ResetPosition(puckPos);
        puck.gameObject.SetActive(true);
        puck.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1f, 1f), puckPos.up.y).normalized * speed;
        puck.GetComponent<Collider2D>().enabled = true;
        canShoot = false;
        isHit = false;
        startCount = true;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(targetPosition.x, 4.27f), speed * Time.deltaTime);
    }

    private void KeepBall()
    {
        puck.gameObject.SetActive(false);
        puckPos.gameObject.SetActive(true);
        puck.GetComponent<Collider2D>().enabled = false;
        canShoot = true;
        isHit = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Puck>())
        {
            cooldown = 5;
            if (puck.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude <= 3) KeepBall();
            else
            {
                randomState = Random.Range(1, 3);
                if (randomState == 1) KeepBall();
            }
        }
    }

    private void OnColliderEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Puck>())
        {
            isHit = true;
            Debug.Log(isHit);
        }
    }

    public void SetCanShoot(bool value)
    {
        canShoot = value;
    }

    public bool SetIsHit(bool value)
    {
        return isHit == value;
    }

    public int SetPuckCheck(int value)
    {
        return puckCheck = value;
    }

    public bool SetStartCount(bool value)
    {
        return startCount = value;
    }

    public void SetCurrentTeam()
    {
        currentTeam++;
        if(currentTeam == soPlayers.p1Team)
        {
            currentTeam++;
        }

        if (currentTeam > 3) currentTeam = 0;


        GetComponent<SpriteRenderer>().sprite = team[currentTeam];
    }
}
