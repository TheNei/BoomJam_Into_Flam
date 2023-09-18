using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TileFlag : MonoBehaviour
{
    private Tilemap tilemap;
    private Vector3Int preCell;
    private Vector3Int nextCell;
    public Tile blackTile;
    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }
    public void SetCellFlag(Vector3Int targetPos)
    {
        tilemap.SetTile(targetPos, blackTile);
        tilemap.SetTileFlags(targetPos, TileFlags.LockTransform);
    }
    public bool IsFlags(Vector3Int targetPos)
    {
        if(tilemap.GetTileFlags(targetPos) == TileFlags.LockTransform)
        {
            return false;
        }    
        else
        {
            return true;
        }
    }

}
