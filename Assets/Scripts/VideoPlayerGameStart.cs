using System;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerGameStart : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private GameObject videoPlayerObject;
    [SerializeField] private GameObject virtualCamera1;
    [SerializeField] private GameObject virtualCamera2;
    

    private void Awake()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    private void OnVideoFinished(VideoPlayer source)
    {
        characterMovement.AllowMovement();
        videoPlayerObject.SetActive(false);
        virtualCamera2.SetActive(false);
        virtualCamera1.SetActive(true);
    }
}