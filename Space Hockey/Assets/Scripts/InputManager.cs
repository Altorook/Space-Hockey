using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Transform p1Spawnpos;
    [SerializeField] Transform p2Spawnpos;

    void Start()
    {
        var p1 = PlayerInput.Instantiate(player, controlScheme: "Player1", pairWithDevice: Keyboard.current);
        p1.transform.position = p1Spawnpos.position;
        p1.transform.rotation = Quaternion.identity;
        var p2 = PlayerInput.Instantiate(player, controlScheme: "Player2", pairWithDevice: Keyboard.current);
        p2.transform.position = p2Spawnpos.position;
        p2.transform.rotation = Quaternion.identity;

    }
}