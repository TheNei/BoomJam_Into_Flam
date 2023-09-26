using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class Command : MonoBehaviour, IDragHandler, IEndDragHandler
{
    
    public Canvas canvas;//Canvas Component
    private RectTransform rect;
    private Vector2 lastPos;
    public TMP_InputField textBox;
    void Start()
    {
        rect = GetComponent<RectTransform>();
        lastPos = rect.anchoredPosition;
    }
    public void OnDrag(PointerEventData eventData)
    {
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(textBox.GetComponent<RectTransform>(), Input.mousePosition))
        {
            Debug.Log("LastPos");
            
            Destroy(this.gameObject);
            return;
        }
            rect.anchoredPosition = lastPos;

    }

  /*  public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter.gameObject.CompareTag("CommandLine"))
        {
            Debug.Log("LastPos");
            Destroy(this.gameObject);
        }
    }*/
}
    /*   private void OnTriggerEnter2D(Collider2D collision)
       {
           if(collision.gameObject.CompareTag("CommandLine"))
           {
               Debug.Log(lastPos);
               Destroy(this.gameObject);

           }
       }*/

