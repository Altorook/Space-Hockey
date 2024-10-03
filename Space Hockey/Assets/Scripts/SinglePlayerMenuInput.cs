using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SinglePlayerMenuInput : MonoBehaviour
{
    [SerializeField]
    InputActionAsset inputActions;
    SinglePlayerTeamSelect menuScript;
    // Start is called before the first frame update
    void Awake()
    {
        menuScript = GetComponent<SinglePlayerTeamSelect>();
    }
    private void OnEnable()
    {
        var menuCtrl = inputActions.FindActionMap("Player1");
        menuCtrl.FindAction("Movement").performed += val => menuScript.HandleInput(val.ReadValue<Vector2>());
        menuCtrl.FindAction("KeepPuck").performed += val => menuScript.LaunchGame();
        menuCtrl.Enable();
    }
}
