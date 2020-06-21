using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{

    public int LevelID;
    public bool Cleared;
    public List<PieceData> AllPieces = new List<PieceData>();
}
