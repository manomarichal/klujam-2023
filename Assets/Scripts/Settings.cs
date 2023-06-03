using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Settings : MonoBehaviour
{
    
    [SerializeField] private Toggle vsyncButton;

    private void Awake()
    {
        vsyncButton.onValueChanged.AddListener(onChangeVSync);
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
    
}
