using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class ExitPanel : MonoBehaviour
{
    [SerializeField] private MMF_Player exitOpenFeedback;
    
    public void OpenPanel()
    {
        gameObject.SetActive(true);
        exitOpenFeedback.PlayFeedbacks();
    }
}
