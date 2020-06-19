using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    private readonly float ROTATE_SPEED = 0.15f;

    private bool m_Rotating = false;

    public int StartingRotation { get; set; }
    public int TargetRotation { get; set; }

    // Events
    public Action OnPieceMoved;

    void Start()
    {
        
    }

    private void OnMouseUp()
    {
        Rotate();
    }

    public void Rotate()
    {
        if (m_Rotating)
            return;

        print("rotation: " + transform.localEulerAngles);

        m_Rotating = true;

        LeanTween.rotateZ(gameObject, transform.localEulerAngles.z - 90, ROTATE_SPEED).setOnComplete(() =>
        {
            m_Rotating = false;
            if (OnPieceMoved != null)
                OnPieceMoved.Invoke();
        });
    }
}
