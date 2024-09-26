using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedBar : MonoBehaviour
{
    [SerializeField]
    private Slider speedSlider;

    private void Start()
    {
        speedSlider.minValue = 5f;
        speedSlider.maxValue = 15f;
        speedSlider.onValueChanged.AddListener(UpdateSpeedBar);
    }



    public void UpdateSpeedBar(float speed)
    {
        speedSlider.value = speed;
    }
}
