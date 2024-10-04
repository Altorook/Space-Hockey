using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Puck : MonoBehaviour
{
    [SerializeField] private float puckSpeed;

    public void ResetPosition(Transform pos)
    {
        transform.position = pos.position;
        gameObject.SetActive(false);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            SoundManager.Instance.PlaySFX("PuckHitPaddle");
        }
        else
        {
            SoundManager.Instance.PlaySFX("PuckHitWall");
        }
    }
}
