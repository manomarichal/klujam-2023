using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class Settings : MonoBehaviour
{
    
    [SerializeField] private PostProcessVolume postProcessing;
    
    [SerializeField] private Toggle vsyncButton;
    [SerializeField] private TMP_Dropdown graphicDropdown;
    [SerializeField] private TMP_Dropdown antiAliasingDropdown;
    [SerializeField] private Toggle motionBlurButton;

    private MotionBlur _motionBlur;
    
    private void Awake()
    {
        vsyncButton.onValueChanged.AddListener(onChangeVSync);
        graphicDropdown.onValueChanged.AddListener(onChangeGraphicPreset);
        antiAliasingDropdown.onValueChanged.AddListener(onChangeAntiAliasing);
        motionBlurButton.onValueChanged.AddListener(onChangeMotionBlur);
    }

    // Start is called before the first frame update
    void Start()
    {

        postProcessing.profile.TryGetSettings(out _motionBlur);
        
        string[] names = QualitySettings.names;
        foreach (var name in names)
        {
            TMP_Dropdown.OptionData newOption = new TMP_Dropdown.OptionData();
            newOption.text = name;
            graphicDropdown.options.Add(newOption);
        }

        graphicDropdown.value = QualitySettings.GetQualityLevel();
        antiAliasingDropdown.value = QualitySettings.antiAliasing;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void onChangeVSync(bool argo0)
    {
        QualitySettings.vSyncCount = argo0 ? 1 : 0;
        Debug.Log(QualitySettings.vSyncCount);
    }

    private void onChangeGraphicPreset(int level)
    {
        QualitySettings.SetQualityLevel(level);
        Debug.Log(QualitySettings.GetQualityLevel());
    }

    private void onChangeAntiAliasing(int level)
    {
        QualitySettings.antiAliasing = level;
    }

    private void onChangeMotionBlur(bool active)
    {
        _motionBlur.active = active;
    }
    
}
