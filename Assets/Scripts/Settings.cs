using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Settings : MonoBehaviour
{
    
    [SerializeField] private Toggle vsyncButton;
    [SerializeField] private TMP_Dropdown graphicDropdown;
    [SerializeField] private TMP_Dropdown antiAliasingDropdown;

    private void Awake()
    {
        vsyncButton.onValueChanged.AddListener(onChangeVSync);
        graphicDropdown.onValueChanged.AddListener(onChangeGraphicPreset);
        antiAliasingDropdown.onValueChanged.AddListener(onChangeAntiAliasing);
    }

    // Start is called before the first frame update
    void Start()
    {
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
}
