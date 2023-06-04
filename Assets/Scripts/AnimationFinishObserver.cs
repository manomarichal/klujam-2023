using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFinishObserver : MonoBehaviour
{
     
    public Action OnAnimationFinished;
 

    public void OnAnimationFinishedEvent()
    {
        OnAnimationFinished?.Invoke();
    }
}