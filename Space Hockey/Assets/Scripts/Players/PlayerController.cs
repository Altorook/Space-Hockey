using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("---Puck---")]
    [SerializeField] private Puck puck;
    [SerializeField] private Transform puckPos;

    [Header("---Player Stat---")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float shootSpeed;

    private Rigidbody2D rb;
    private Vector2 _movement;
    private PlayerInput _playerInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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

    public void Shoot()
    {
        if(puckPos.gameObject.activeSelf)
        {
            puckPos.gameObject.SetActive(false);
            puck.ResetPosition(puckPos);
            puck.gameObject.SetActive(true);
            puck.gameObject.GetComponent<Rigidbody2D>().velocity = puckPos.up * puck.PuckRandomSpeed(5f, 10f);
        }
        else
        {
            return;
        }
    }
}
