using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] private string mapName;
    [SerializeField] private InputActionAsset inputActionAsset;
    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        var playerCtrl = inputActionAsset.FindActionMap(mapName);
        playerCtrl.FindAction("Movement").performed += ctx => _playerController.MovementInput(ctx.ReadValue<Vector2>());
        playerCtrl.FindAction("KeepPuck").performed += ctx => _playerController.KeepBall();

        playerCtrl.Enable();
    }
}
