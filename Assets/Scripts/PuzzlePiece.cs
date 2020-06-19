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

    public void Init()
    {
        StartingRotation = GetRandomRotation(false);
        transform.localEulerAngles = new Vector3(0,0,StartingRotation);
        TargetRotation = GetRandomRotation(true);
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

    public int GetRandomRotation(bool isTarget)
    {
        int r = UnityEngine.Random.Range(0, 3);
        int rotation = 0;

        switch (r)
        {
            case 0:
                rotation = 0;
            break;
            case 1:
                rotation = 90;
            break;
            case 2:
                rotation = 180;
            break;
            case 3:
                rotation = 270;
            break;
        }

        if (isTarget)
        {
            if (rotation == StartingRotation)
            {
                rotation += 90;
            }
        }

        return rotation;
    }
}
