using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    private readonly float ROTATE_SPEED = 0.15f;

    private bool m_Rotating = false;
    private SpriteRenderer m_Sprite;

    public int StartingRotation;
    public int TargetRotation;

    // Events
    public Action OnPieceMoved;

    void Awake()
    {
        m_Sprite = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        LeanTween.scale(gameObject, Vector3.one, 0.25f).setDelay(UnityEngine.Random.Range(0.25f, 0.75f)).setEaseInOutQuad();
    }

    public void Disappear()
    {
        LeanTween.scale(gameObject, Vector3.zero, 0.25f).setDelay(0.5f).setEaseInOutQuad();
    }

    public void Init(int startRotation, int targetRotation, Sprite sprite)
    {
        m_Sprite.sprite = sprite;
        StartingRotation = startRotation;
        transform.localEulerAngles = new Vector3(0,0,StartingRotation);
        TargetRotation = targetRotation;
    }

    public bool IsOnTargetPosition()
    {
        return Mathf.Approximately(transform.localEulerAngles.z,TargetRotation);
    }

    private void OnMouseUp()
    {
        Rotate();
    }

    public void Rotate()
    {
        if (m_Rotating)
            return;

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
