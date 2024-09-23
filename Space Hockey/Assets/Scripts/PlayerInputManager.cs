using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerController _playerController;
    private Input _input;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _input = new Input();
    }

    public void OnMove(CallbackContext ctx)
    {
        _playerController.MovementInput(ctx.ReadValue<Vector2>());
    }

    public void OnShoot()
    {
        _playerController.Shoot();
    }
}
