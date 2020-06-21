using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    public Action OnAnimationComplete;

    public void OnTitleAnimationComplete()
    {
        print("complete");
        if (OnAnimationComplete != null)
        {
            OnAnimationComplete.Invoke();
        }
    }
}
