using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puck : MonoBehaviour
{
    private float puckSpeed;

    public float PuckRandomSpeed(float min, float max)
    {
        puckSpeed = Random.Range(min, max);
        return puckSpeed;
    }
    public void ResetPosition(Transform pos)
    {
        transform.position = pos.position;
        gameObject.SetActive(false);
    }
}
