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
    public Image enhanceImage;
    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("MoveUI").GetComponent<Canvas>();
        rect = GetComponent<RectTransform>();
        lastPos = rect.anchoredPosition;
        enhanceImage = this.GetComponent<Image>();
    }
    public void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        List<Image> tempList = CommandManager.Instance.enhanceBox;
        if (tempList.Count == 0)
        {
            return;
        }
        Vector2 localMousePosition;
        foreach (var list in tempList)
        {
            RectTransform rectTransform = list.GetComponent<RectTransform>();
            RectTransformUtility.ScreenPointToLocalPointInRectangle( rectTransform, Input.mousePosition, Camera.main, out localMousePosition);
            if(rect.rect.Contains(localMousePosition))
            {
                int temp = tempList.IndexOf(list);
                CommandManager.Instance.GetEnhance(enhance,temp,enhanceImage.sprite);
                rect.anchoredPosition = lastPos;
                break;
            }
            else
            {
                rect.anchoredPosition = lastPos;
                print("enhance faliure");
            }
        }
        rect.anchoredPosition = lastPos;
    }
    }
