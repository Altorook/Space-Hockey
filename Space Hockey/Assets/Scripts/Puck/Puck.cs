using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Puck : MonoBehaviour
{
    [SerializeField] private float puckSpeed;

    public void ResetPosition(Transform pos)
    {
        transform.position = pos.position;
        gameObject.SetActive(false);
    }
}
