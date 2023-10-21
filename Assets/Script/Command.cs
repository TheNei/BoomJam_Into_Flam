using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class Command : MonoBehaviour, IDragHandler, IEndDragHandler
{
 /*   private int index = 0;*/
    public Canvas canvas;//Canvas Component
    private RectTransform rect;
    private Vector2 lastPos;
    public Image commandImage;
    public CommandManager.MoveCommand command;
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("MoveUI").GetComponent<Canvas>(); 
        rect = GetComponent<RectTransform>();
        lastPos = rect.anchoredPosition;
        commandImage = this.GetComponent<Image>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        if(command == CommandManager.MoveCommand.Excute)
        {
            return;
        }
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
   
    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        List<Image> tempBox = CommandManager.Instance.commandBox;
        if (tempBox.Count == 0)
            return;

        Vector2 localMousePosition;
        foreach (Image image in tempBox)
        {
            RectTransform rectTransform = image.GetComponent<RectTransform>();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, Camera.main, out localMousePosition);
            if (rectTransform.rect.Contains(localMousePosition))
            {
                int temp = tempBox.IndexOf(image);
               /* print("sucess");*/
                CommandManager.Instance.GetInput(command, temp,commandImage.sprite);
                rect.anchoredPosition = lastPos;
                break;
            }
            else
            {
                rect.anchoredPosition = lastPos;
               /* print("falure");*/
            }
        }
        rect.anchoredPosition = lastPos;
        return;
      
    }
}


