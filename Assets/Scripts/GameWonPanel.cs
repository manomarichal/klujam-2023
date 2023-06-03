using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;
using Observable = UniRx.Observable;

public class GameWonPanel : MonoBehaviour
{
    [SerializeField] private GameObject gameWonPanel;
    [SerializeField] private TextMeshProUGUI mainMenuButtonText;
    [SerializeField] private MMF_Player textFeedback;
    [SerializeField] private float timerValue = 5;


    // TODO Add some animated shit here
    public void OpenAnimated()
    {
        gameWonPanel.SetActive(true);
        StartTextAnimation();
    }

    public void OnGoToMainMenuClick()
    {
        SceneManager.LoadSceneAsync("Menu");
    }


    public void StartTextAnimation()
    {
        StartCoroutine(CountdownTimer());
    }

    private IEnumerator CountdownTimer()
    {
        float currentTime = timerValue;

        while (currentTime > 0)
        {
            currentTime -= 1f;
            Debug.Log("Timer: " + currentTime + " second(s)");

            textFeedback.PlayFeedbacks();
            mainMenuButtonText.SetText($"Going to Main Menu in {currentTime}...");

            yield return new WaitForSeconds(1f);
        }

        // Timer has completed
        Debug.Log("Timer completed!");
        OnGoToMainMenuClick();
    }
}