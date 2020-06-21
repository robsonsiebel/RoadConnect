using System;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    public Action OnAnimationComplete;

    public void OnTitleAnimationComplete()
    {
        if (OnAnimationComplete != null)
        {
            OnAnimationComplete.Invoke();
        }
    }
}
