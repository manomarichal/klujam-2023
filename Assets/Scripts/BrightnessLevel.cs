using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class BrightnessLevel : MonoBehaviour
{
    [SerializeField] private Volume volume;
    [SerializeField] private Slider volumeSlider;

    private ColorAdjustments _colorAdjustments;
    private float brightnessValue;
    private void Awake()
    {
        if (volume.profile.TryGet(out _colorAdjustments))
        {
            volumeSlider.value = _colorAdjustments.contrast.value * -1;
        }
        volumeSlider.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(float arg0)
    {
        _colorAdjustments.contrast.value = arg0 * -1;
    }
}
