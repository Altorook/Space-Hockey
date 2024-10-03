using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuInput : MonoBehaviour
{
    [SerializeField]
    InputActionAsset inputActions;
    MenuScript menuScript;
    // Start is called before the first frame update
    void Awake()
    {
        menuScript = GetComponent<MenuScript>();
    }
    private void OnEnable()
    {
        var menuCtrl = inputActions.FindActionMap("Player1");
        menuCtrl.FindAction("Movement").performed += val => menuScript.HandleInput(val.ReadValue<Vector2>());
        menuCtrl.FindAction("KeepPuck").performed += val => menuScript.LaunchGameAsPlayers();
        menuCtrl.Enable();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
