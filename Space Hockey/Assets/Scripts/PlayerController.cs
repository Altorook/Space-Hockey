using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;

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
        Debug.Log("shoot");
    }
}
