using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
public class WeaPon : MonoBehaviour,IDragHandler, IEndDragHandler
{
    
    [HideInInspector]
    public bool isTorisionUse;
    [HideInInspector]
    public bool torisionTrigger;
    [HideInInspector]
    public Vector3 currentPos;
    [HideInInspector]
    public Vector3Int targetPos;
    [HideInInspector]
    public Vector3 direction = Vector3.zero;
    
    private Tilemap tilemap;
    private Canvas canvas;
    private RectTransform rect;
    public TileBase blackTile;
    public TileBase whiteTile;
    public TileBase yellowTile;
    public GodWeapons godWeapon;
    private Vector3 lastPos;
    private void Start()
    {
        tilemap = GameObject.FindAnyObjectByType<Tilemap>();
        rect = GetComponent<RectTransform>();
        canvas = GameObject.FindGameObjectWithTag("MoveUI").gameObject.GetComponent<Canvas>();
        lastPos = rect.anchoredPosition;
    }
    public void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        currentPos = Input.mousePosition;
        targetPos = tilemap.WorldToCell(currentPos);
        print(targetPos);
        if (tilemap.HasTile(targetPos))
        {
            tilemap.SetTile(targetPos, yellowTile);
            tilemap.SetTileFlags(targetPos, TileFlags.LockColor);
            direction = Vector3.left;
            godWeapon.torisionTrigger = true;
            godWeapon.direction = this.direction;
        }
        else
        {
            print("it's not exist");
        }
        rect.anchoredPosition = lastPos;
    }

}
