using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuInput : MonoBehaviour
{
    [SerializeField]
    InputActionAsset inputActions;
    MenuScript menuScript;
    InputActionMap menuCtrl;
    // Start is called before the first frame update
    void Awake()
    {
        menuScript = GetComponent<MenuScript>();
    }
    private void OnEnable()
    {
        menuCtrl = inputActions.FindActionMap("MenuCtrl");
        menuCtrl.FindAction("Move").performed += val => menuScript.HandleInput(val.ReadValue<Vector2>());
        menuCtrl.FindAction("Enter").performed += val => menuScript.LaunchGameAsPlayers();
        menuCtrl.Enable();
    }
    public void DisableMenuCtrl()
    {menuCtrl.Disable();

    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
