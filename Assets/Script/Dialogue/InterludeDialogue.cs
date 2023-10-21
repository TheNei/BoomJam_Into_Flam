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
        foreach (var item in InterludeDialogueBox)
        {
            item.SetActive(false);
        }
    }

    public IEnumerator InterludeDia(string content, int index)
    {
        InterludeDialogueBox[index].SetActive(true);
        StartCoroutine(DialogueSystemT.Instance.SetText(content, InterludeDialogueText[index]));
        yield return new WaitForSeconds(time);
        if(InterludeDialogueText[index].text == content)
        {
            InterludeDialogueBox[index].SetActive(false);
        }
            
    }
}
