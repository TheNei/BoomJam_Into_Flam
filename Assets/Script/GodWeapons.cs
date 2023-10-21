using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
public class GodWeapons : MonoBehaviour
{

    private Tilemap tilemap;
    public Button fly;
    public Button dead;
    public Button Freeze;
    public TileBase blackTile;
    public TileBase whiteTile;
    [HideInInspector]
    public bool isFlyUse;
    [HideInInspector]
    public bool isDeadUse;
    [HideInInspector]
    public bool torisionTrigger;
    [HideInInspector]
    public Vector3 direction = Vector3.zero;
    [HideInInspector]
    public bool isFreeze;
    
    private int index = 2;
    public bool isFly
    {
        get;
        set;
    }
    
    private void Start()
    {
        
        tilemap = GameObject.FindAnyObjectByType<Tilemap>();
        fly.onClick.AddListener(OnFly);
        dead.onClick.AddListener(OnDead);
        Freeze.onClick.AddListener(OnFreeze);
      /*  fly.interactable = false;
        dead.interactable = false;
        Freeze.interactable = false;*/
    }
    private void OnFreeze()
    {
        isFreeze = true;
        Freeze.interactable = false;
        
    }
    void OnFly()
    {
        isFly = true;
        isFlyUse = false;
        fly.interactable = isFlyUse;
    }
    void OnDead()
    {
        SearchTile();
        isDeadUse = false;
        dead.interactable = isDeadUse;
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
                if(index <= 0)
                {
                    index = 2;
                    return;
                }
                if (tile ==blackTile && index > 0)
                {
                    index--;
                    Vector3Int tilePos = new Vector3Int(x, y, 0);
                    tilemap.SetTile(tilePos, whiteTile);
                    tilemap.SetTileFlags(tilePos, TileFlags.None);

                }
             

            }
        }
        index = 2;
    }
  
   
    
}
