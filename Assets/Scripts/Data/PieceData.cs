using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PieceData
{
    public int PieceID;
    public int StartRotation;
    public int TargetRotation;

    public PieceData(int pieceID, int startRotation, int targetRotation)
    {
        PieceID = pieceID;
        StartRotation = startRotation;
        TargetRotation = targetRotation;
    }
}
