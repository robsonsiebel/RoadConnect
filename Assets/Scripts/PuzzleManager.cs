using System;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    private readonly int PUZZLE_SIZE = 4;

    private Vector2 m_StartingPuzzleArea = new Vector2(-1.5f, 2.5f);
    
    public List<PuzzlePiece> AllPieces;
    public GameObject PiecePrefab;

    public Action OnLevelComplete;

    #region Private
    private void CheckForLevelComplete()
    {
        foreach (PuzzlePiece piece in AllPieces)
        {
            if (!piece.IsOnTargetPosition())
            {
                return;
            }
        }

        LevelComplete();
    }

    private void LevelComplete()
    {
        SoundLibrary.Instance.PlaySound(SFX.LevelComplete, 0.5f);

        foreach (PuzzlePiece piece in AllPieces)
        {
            piece.Disappear();
        }

        if (OnLevelComplete != null)
        {
            OnLevelComplete.Invoke();
        }
    }

    private void OnDisable()
    {
        foreach (PuzzlePiece piece in AllPieces)
        {
            piece.OnPieceMoved -= CheckForLevelComplete;
        }
    }
    #endregion

    #region Public
    public void ClearLevel()
    {
        foreach (PuzzlePiece piece in AllPieces)
        {
            Destroy(piece.gameObject);
        }

        AllPieces.Clear();
    }

    public void PopulateLevel(LevelData level, Sprite[] levelSprites)
    {
        PuzzlePiece newPiece;

        int hCount = 0;
        int vCount = 0;
        foreach (PieceData piece in level.AllPieces)
        {
            if (piece.PieceID != -1)
            {
                newPiece = GameObject.Instantiate(PiecePrefab).GetComponent<PuzzlePiece>();
                newPiece.transform.position = new Vector3(m_StartingPuzzleArea.x + hCount, m_StartingPuzzleArea.y - vCount, 0);
                newPiece.Init(piece.StartRotation, piece.TargetRotation, levelSprites[piece.PieceID]);
                newPiece.OnPieceMoved += CheckForLevelComplete;
                newPiece.name = "Piece " + hCount + vCount;
                AllPieces.Add(newPiece);
            }

            hCount++;

            if (hCount >= PUZZLE_SIZE)
            {
                hCount = 0;
                vCount++;
            }
        }
    }
    #endregion

}
