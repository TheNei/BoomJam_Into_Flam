using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class SpawnTile : MonoBehaviour
{
    public enum Dirction { 
        Up =  1,
        Down = 2,
        Left = 3,
        Right = 4,
    }
    public Dictionary<Dirction, Vector3> Value = new Dictionary<Dirction, Vector3>()
    {
        {Dirction.Up , Vector3.up },
        {Dirction.Down,Vector3.down },
        {Dirction.Left,Vector3.left },
        {Dirction.Right,Vector3.right * 2}
    };
    Tilemap tilemap;
    public TileBase whiteTile;
    public TileBase blackTile;
    public TileBase yellowTile;
    private TileFlag flag;
    public List<Dirction> dir = new List<Dirction>();
    int index = 0;
    private Vector3Int originPos;
    private Vector3Int currentPos;
    private Vector3Int targetPos;
    void Start()
    {
        flag = GetComponent<TileFlag>();
        tilemap = GetComponent<Tilemap>();
        SearchTile();
    }
    void SearchTile()
    {
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
        print(allTiles.Length);
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                TileBase tile = allTiles[x - bounds.xMin + (y - bounds.yMin) * bounds.size.x];
                
                if (tile == blackTile)
                {
                    /* Vector3 tilePos = tilemap.GetCellCenterWorld(new Vector3Int(x, y, 0));*/
                    /*print("x : " + x + "Y :" + y);*/
                    Vector3Int tilePos = new Vector3Int(x, y, 0);
                   /* print(tilePos);*/
                    tilemap.SetTileFlags(tilePos, TileFlags.LockTransform);
                 /*   print(flag.IsFlags(tilePos));*/
                }
                if(tile == yellowTile)
                {
                    Vector3Int tilePos = new Vector3Int(x, y, 0);
                    originPos = tilePos;
                    currentPos = originPos;
                }
            }
        }
    }
   public void Move()
    {
        if(index >= dir.Count)
        {
            index = 0;
            SetTilePosition(currentPos, whiteTile, TileFlags.None);
            SetTilePosition(originPos, yellowTile, TileFlags.LockTransform);
            return;
        }
        
        targetPos = tilemap.WorldToCell(tilemap.CellToWorld(currentPos) + Value[dir[index]]);
        print(tilemap.HasTile(targetPos));
        SetTilePosition(currentPos, whiteTile, TileFlags.None);
        SetTilePosition(targetPos, yellowTile, TileFlags.LockTransform);
        currentPos = targetPos;
        index++;
        
    }
    void SetTilePosition(Vector3Int pos,TileBase tile,TileFlags flag)
    {
        if(!tilemap.HasTile(pos))
        {
            print("it's not tile");
            return;
        }
        tilemap.SetTile(pos, tile);
        tilemap.SetTileFlags(pos, flag);

    }
    

    }

