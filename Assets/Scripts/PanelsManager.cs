using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class PanelsManager : MMSingleton<PanelsManager>
{
    [SerializeField] private GameObject[] panels;
    [SerializeField] private bool gameWasPlayed;
    [SerializeField] private GameObject dlcButton;

    private void Start()
    {
        if (gameWasPlayed)
        {
            dlcButton.SetActive(true);
        }
    }

    public void OpenPanel(int index)
    {
        for (var i = 0; i < panels.Length; i++) panels[i].SetActive(i == index);
    }
}