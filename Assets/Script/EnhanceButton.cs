using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class EnhanceButton : MonoBehaviour,IDragHandler,IEndDragHandler
{
    public CommandManager.Enhance enhance;
    public Canvas canvas;
    private RectTransform rect;
    private Vector2 lastPos;
    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("MoveUI").GetComponent<Canvas>();
        rect = GetComponent<RectTransform>();
        lastPos = rect.anchoredPosition;
    }
    public void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
    /*    print("Test");*/
        List<Image> tempList = CommandManager.Instance.enhanceBox;
        if (tempList.Count == 0)
        {
          /*  print("The temp is null");*/
            return;
        }
        foreach (var list in tempList)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(list.GetComponent<RectTransform>(),Input.mousePosition))
            {
                int temp = tempList.IndexOf(list);
             /*   print("enhance is Useful");*/
                CommandManager.Instance.GetEnhance(enhance,temp);
                rect.anchoredPosition = lastPos;
                break;
            }
            else
            {
                rect.anchoredPosition = lastPos;
            }
        }
        rect.anchoredPosition = lastPos;
    }
    }
