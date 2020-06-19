using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    private Vector2 StartingPuzzleArea = new Vector2(-0.5f, 0.5f);
    private int PuzzleSize = 4;

    public List<PuzzlePiece> AllPieces;
    public GameObject PiecePrefab;

    

    void Start()
    {
        PopulateLevel();
    }

    void PopulateLevel()
    {
        PuzzlePiece newPiece;
        for (int i = 0; i < PuzzleSize/2; i++)
        {
            for (int k = 0; k < PuzzleSize / 2; k++)
            {
                newPiece = GameObject.Instantiate(PiecePrefab).GetComponent<PuzzlePiece>();
                newPiece.transform.position = new Vector3(StartingPuzzleArea.x + i, StartingPuzzleArea.y - k, 0);
                newPiece.Init();
            }
                
        }
    }



}
