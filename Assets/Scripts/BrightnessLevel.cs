using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class BrightnessLevel : MonoBehaviour
{
    [SerializeField] private Volume volume;
    [SerializeField] private Slider volumeSlider;

    private ColorAdjustments _colorAdjustments;
    private float _brightnessValue;

    private void Awake()
    {
        _brightnessValue = PlayerPrefs.GetFloat("Brightness");

        if (volume.profile.TryGet(out _colorAdjustments))
        {
            if (_brightnessValue != 0)
            {
                _colorAdjustments.contrast.value = _brightnessValue;
            } 

            volumeSlider.value = _colorAdjustments.contrast.value * -1;
        }

        volumeSlider.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(float value)
    {
        _colorAdjustments.contrast.value = value * -1;
    }

    private void OnDestroy()
    {
        volumeSlider.onValueChanged.RemoveListener(OnValueChanged);
        PlayerPrefs.SetFloat("Brightness", volumeSlider.value * -1);
    }
}