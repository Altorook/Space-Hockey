using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] private Transform puck;
    [SerializeField] private Transform p1;
    [SerializeField] private Transform puckPos;
    [SerializeField] private Transform netPos;
    [SerializeField] private float speed;

    private bool canShoot;
    private bool isHit;
    private float randomSpeed;
    private int randomState;
    [SerializeField] private float distance;
    [SerializeField] private int randomSide;
    private Vector2 startPos;
    private Vector2 target;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        ChangeDirection();
        InvokeRepeating("ChangeDirection", 0.5f, 0.5f);

        Debug.Log(canShoot);
    }
        

    void Update()
    {
        if (puckPos.transform.gameObject.activeSelf == false) ChaseBall();
        else if (puckPos.transform.gameObject.activeSelf == true) ShootBall();
        else return;
    }

    private void ChaseBall()
    {
        if(puck.position.y > netPos.position.y && puck.gameObject.activeSelf == true && isHit == false)
        {
            speed = Random.Range(1f, 5f);
            Vector2 targetPosition = puck.position;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else if (puck.position.y > netPos.position.y && puck.gameObject.activeSelf == true && isHit == true || 
                 puck.gameObject.activeSelf == false && puckPos.gameObject.activeSelf == false && canShoot == false ||
                 puck.position.y < netPos.position.y && puck.gameObject.activeSelf == true)
        {
            Vector2 targetPosition = p1.position;
            speed = Random.Range(1f, 5f);
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(targetPosition.x, 4.27f), speed * Time.deltaTime);
        }
        else
        {
            randomSpeed = Random.Range(1f, 5f);
            transform.position = Vector2.MoveTowards(transform.position, target, randomSpeed * Time.deltaTime);
        }


    }
    private void ChangeDirection()
    {
        target = new Vector2(Random.Range(-5f, 5f), 4.27f);
    }

    private void ShootBall()
    {
        Vector2 targetPosition = p1.position;
        randomSpeed = Random.Range(5f, 10f);
        puckPos.gameObject.SetActive(false);
        puck.transform.gameObject.GetComponent<Puck>().ResetPosition(puckPos);
        puck.gameObject.SetActive(true);
        puck.gameObject.GetComponent<Rigidbody2D>().velocity = puckPos.up * randomSpeed;
        canShoot = false;
        isHit = false;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(targetPosition.x, 4.27f), speed * Time.deltaTime);
    }

    private void KeepBall()
    {
        puck.gameObject.SetActive(false);
        puckPos.gameObject.SetActive(true);
        canShoot = true;
        isHit = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Puck>())
        {
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
}
