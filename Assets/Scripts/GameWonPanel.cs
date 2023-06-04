using System.Collections;
using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWonPanel : MonoBehaviour
{
    [SerializeField] private GameObject gameWonPanel;
    [SerializeField] private TextMeshProUGUI mainMenuButtonText;
    [SerializeField] private MMF_Player textFeedback;
    [SerializeField] private float timerValue = 5;
    [SerializeField] private AnimationFinishObserver animationFinishObserver;


    // TODO Add some animated shit here
    public void OpenAnimated()
    {
        gameWonPanel.SetActive(true);

        animationFinishObserver.OnAnimationFinished += OnAnimationFinished;
    }

    public void OnGoToMainMenuClick() => SceneManager.LoadSceneAsync("Menu");
    private void OnAnimationFinished() => StartTextAnimation();
    public void StartTextAnimation() => StartCoroutine(CountdownTimer());

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