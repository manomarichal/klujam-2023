using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MoreMountains.Feedbacks;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenTimer : MonoBehaviour
{
    [SerializeField] private float timerDuration;
    [SerializeField] private CanvasGroup loadingObject;
    [SerializeField] private float loadingObjectFadeDuration;
    [SerializeField] private float startObjectFadeDuration;
    [SerializeField] private TextMeshProUGUI startGameObject;
    [SerializeField] private MMF_Player loadingScreenFeedback;
    
    private bool _canStartGame;


    private void Start()
    {
        Observable.Timer((TimeSpan.FromSeconds(timerDuration))).Subscribe(_ =>
        {
            loadingObject.DOFade(0, loadingObjectFadeDuration).OnComplete((() =>
            {
                startGameObject.gameObject.SetActive(true);
                startGameObject.DOFade(0, startObjectFadeDuration).SetLoops(-1, LoopType.Yoyo);
                _canStartGame = true;
            }));
        });
    }

    private void Update()
    {
        if (_canStartGame && Input.anyKeyDown)
        {
            loadingScreenFeedback.PlayFeedbacks();
        }
    }
}