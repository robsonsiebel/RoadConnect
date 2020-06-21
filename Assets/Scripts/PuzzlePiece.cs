using System;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    private readonly float ROTATE_SPEED = 0.15f;

    private SpriteRenderer m_Sprite;
    private AudioSource m_Audio;
    private bool m_Rotating = false;
    private bool m_Locked;
    private bool m_IsMirrored;
    private bool m_IsBonus;

    public int StartingRotation;
    public int TargetRotation;

    public Action OnPieceMoved;

    #region Private
    private void Awake()
    {
        m_Sprite = GetComponent<SpriteRenderer>();
        m_Audio = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        LeanTween.scale(gameObject, Vector3.one, 0.25f).setDelay(UnityEngine.Random.Range(0.25f, 0.75f)).setEaseInOutQuad().setOnStart(() => m_Audio.Play());
    }

    private void OnMouseUp()
    {
        if (!m_Locked)
        {
            Rotate();
        }
    }
    #endregion

    #region Public
    public void Disappear()
    {
        m_Locked = true;
        LeanTween.scale(gameObject, Vector3.zero, 0.25f).setDelay(0.5f).setEaseInOutQuad();
    }

    public void Init(int startRotation, int targetRotation, Sprite sprite)
    {
        m_Sprite.sprite = sprite;
        StartingRotation = startRotation;
        transform.localEulerAngles = new Vector3(0, 0, StartingRotation);
        TargetRotation = targetRotation;

        if (sprite.name.Contains("MR180"))
        {
            m_IsMirrored = true;
        }
        else
        if (sprite.name.Contains("BN360"))
        {
            m_IsBonus = true;
        }
    }

    public bool IsOnTargetPosition()
    {
        if (m_IsBonus)
        {
            return true;
        }

        if (m_IsMirrored)
        {
            if (!Mathf.Approximately(transform.localEulerAngles.z, TargetRotation))
            {
                int newTarget = 0;
                switch (TargetRotation)
                {
                    case 0:
                        newTarget = 180;
                        break;
                    case 90:
                        newTarget = 270;
                        break;
                    case 180:
                        newTarget = 0;
                        break;
                    case 270:
                        newTarget = 90;
                        break;
                }

                return Mathf.Approximately(transform.localEulerAngles.z, newTarget);
            }
        }

        return Mathf.Approximately(transform.localEulerAngles.z,TargetRotation);
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

        SoundLibrary.Instance.PlaySound(SFX.ShapeRotate);
    }
    #endregion
}
