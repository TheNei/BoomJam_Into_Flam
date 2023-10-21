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
        RightUp = 5,
        RightDown = 6,
        LeftDown = 7,
        Left1Down2 = 8,
        Right1Down2 = 9,
        Right3Down1 = 10,
        
    }
    public Dictionary<Dirction, Vector3> Value = new Dictionary<Dirction, Vector3>()
    {
        {Dirction.Up , Vector3.up },
        {Dirction.Down,Vector3.down },
        {Dirction.Left,Vector3.left },
        {Dirction.Right,Vector3.right * 2},
        {Dirction.RightUp,new Vector3(2.0f,1.0f,0f)},
        {Dirction.RightDown,new Vector3(2.0f,-1.0f,0f)},
        {Dirction.LeftDown,new Vector3(-1.0f,-1.0f,0f) },
        {Dirction.Left1Down2,new Vector3(-1f,-2f,0f)},
        {Dirction.Right1Down2,new Vector3(2f,-2f,0f) },
        {Dirction.Right3Down1,new Vector3(4f,-1f,0f) }
    };
    Tilemap tilemap;
    public TileBase whiteTile;
    public TileBase blackTile;
    public TileBase yellowTile;
    public List<Dirction> dir = new List<Dirction>();
    int index = 0;
    Vector3Int originPos;
    Vector3Int currentPos;

    private Vector3Int targetPos;

  /*  public DrawLine line;*/
    [HideInInspector]
    public bool isMove;
    void Start()
    {
        
        tilemap = GetComponent<Tilemap>();
        GetTilePos();
/*        line.SetNewPosition(tilemap.CellToWorld(originPos), 0);
        GetTargetPos();*/
    }

    public void GetTilePos()
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
                    Vector3Int tilePos = new Vector3Int(x, y, 0);
                    tilemap.SetTileFlags(tilePos, TileFlags.LockTransform);
                }
                if (tile == yellowTile)
                {
                    print("find yellow");
                    Vector3Int tilePos = new Vector3Int(x, y, 0);
                    originPos = tilePos;
                    currentPos = originPos;
                    index = 0;
                }

            }
        }
    }
        public void Move()
    {
        if (dir.Count == 0)
            return;
        if (index >= dir.Count)
        {
            index = 0;
            SetTilePosition(currentPos, whiteTile, TileFlags.None);
            SetTilePosition(originPos, yellowTile, TileFlags.LockTransform);
            currentPos = originPos;
            return;
        }

        targetPos = tilemap.WorldToCell(tilemap.CellToWorld(currentPos) + Value[dir[index]]);
      /*  print(tilemap.HasTile(targetPos));*/
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
 /*   void GetTargetPos()
    {
        for(int i = 0;i < dir.Count;i++)
        {
            line.SetNewPosition(line.GetLocalPosition(i)+Value[dir[i]],i+1);
        }
        
    }*/

    }

