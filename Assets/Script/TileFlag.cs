using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TileFlag : MonoBehaviour
{
    private Tilemap tilemap;
 /*   private Vector3Int preCell;
    private Vector3Int nextCell;*/
    public Tile blackTile;
    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }
    public void SetCellFlag(Vector3Int targetPos,Vector3Int currentPos)
    {
        Vector3Int distance = (targetPos - currentPos) / 2;
        if (tilemap.HasTile(currentPos))
        {
            tilemap.SetTileFlags(targetPos, TileFlags.LockTransform);
            tilemap.SetTile(currentPos, blackTile);
        }
        else
        {
            print("it's not");
        }

        if (tilemap.HasTile(currentPos + distance))
        {
            tilemap.SetTileFlags(currentPos + distance, TileFlags.LockTransform);
            tilemap.SetTile(currentPos + distance, blackTile);
        }
        else
        {
            print("mid's not");
        }
       
    }
    public bool IsFlags(Vector3Int targetPos)
    {
        if(tilemap.GetTileFlags(targetPos) == TileFlags.LockTransform)
        {
            return true;
        }    
        else
        {
            return false;
        }
    }

}
