using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InterludeDialogue : MonoBehaviour
{
    private static InterludeDialogue instance;
    public static InterludeDialogue Instance
    {
        get
        {
            if(instance == null)
            {
                instance = Instance;
            }
            return instance;
        }
    }
    private void Awake()
    {
        //if (instance != null && instance != this)
        //{
        //    Destroy(this);
        //}
        if(instance == null)
        {
            instance = this; 
        }
    }
    public TMP_Text[] InterludeDialogueText;
    public GameObject[] InterludeDialogueBox;
    public float time;
    private void Start()
    {
        time = 5f;

        foreach (var item in InterludeDialogueText)
        {
            item.text = string.Empty;
        }
    }

    public IEnumerator InterludeDia(string content, TMP_Text DialogText)
    {
        StartCoroutine(DialogueSystemT.Instance.SetText(content, DialogText));
        yield return new WaitForSeconds(time);
        
    }
}
