using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OnePlayerTeamInput : MonoBehaviour
{
    [SerializeField]
    InputActionAsset inputActions;
    OnePlayerTeamSelect menuScript;
    InputActionMap menuCtrl;
    // Start is called before the first frame update
    void Awake()
    {
        menuScript = GetComponent<OnePlayerTeamSelect>();
    }
    private void OnEnable()
    {
         menuCtrl = inputActions.FindActionMap("MenuCtrl");
        menuCtrl.FindAction("Move").performed += val => menuScript.HandleInputOne(val.ReadValue<Vector2>());
        menuCtrl.FindAction("Enter").performed += val => menuScript.LaunchGame();
        menuCtrl.Enable();
      
    }
    public void DisableMenuCtrl()
    {
        menuCtrl.Disable();
    }

        
 
    // Update is called once per frame
    void Update()
    {
        
    }
}
