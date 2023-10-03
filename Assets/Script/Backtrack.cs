using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
public class Backtrack : MonoBehaviour
{
    public Vector3[] backLists = new Vector3[3];
    private Movement player;
    public Button backBtn;
    public Tile whiteTile;
    public Tilemap tilemap;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        backBtn.onClick.AddListener(delegate { StartCoroutine(Back()); });
    }
    public void SetList(Vector3 pos,int index)
    {
        backLists[index] = pos * -1;
    }
   
    IEnumerator Back()
    {
        bool isEmpty = true;
        foreach(var vector3 in backLists)
        {
            if (vector3 != null)
                isEmpty = false;
        }
        if(isEmpty)
        {
            yield return null;
        }

        for (int i = backLists.Length - 1; i >= 0; i--)
        {
            StartCoroutine(player.PlayerMovement(backLists[i]));
            if (tilemap.HasTile(tilemap.WorldToCell(backLists[i])))
            {
                tilemap.SetTileFlags(tilemap.WorldToCell(backLists[i]), TileFlags.None);
                tilemap.SetTile(tilemap.WorldToCell(backLists[i]), whiteTile);
            }
            else
            {
                print("it's Has not");
            }
            yield return new WaitForSeconds(1.0f);
        }
        Array.Clear(backLists, 0, backLists.Length);
        yield return null;
    }
    

}
