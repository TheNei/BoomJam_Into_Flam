using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Text.RegularExpressions;

public class DialogueSystem : MonoBehaviour
{
    [Header("对话框UI")]
    public TMP_Text playerText;
    public TMP_Text npcText;
    public GameObject playerDialogBox;
    public GameObject npcDialogBox;

    [Header("剧情文本")]
    public TextAsset textFile;
    private int index;

    List<string> textList = new List<string>();
    string text;
    bool textFinished;

    void Start()
    {
        textFinished = true;
        GetTextFile(textFile);
    }

    void Update()
    {
        Debug.Log(textList.Count);
        Debug.Log(index);
        if (Input.GetKeyDown(KeyCode.Space) && index == textList.Count)
        {
            playerDialogBox.SetActive(false);
            npcDialogBox.SetActive(false);
            index = 0;
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space) && textFinished == false)
        {
            waitTime = 0.01f;
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space) && textFinished == true)
        {
            waitTime = 0.1f;
            DilogueLoading();
        }
        
    }

    public void GetTextFile(TextAsset file)
    {
        textList.Clear();
        index = 0;
        var fileDate = file.text.Split('\n');
        foreach (var item in fileDate)
        {
            textList.Add(item);
        }
    }
    public void DilogueLoading()
    {
        string processingText = textList[index];
        string pattern = "“.*”";
        Match match = Regex.Match(processingText,pattern);
        text = match.Value.Substring(1, match.Length - 2);
        GetCharacter(Convert.ToInt32(textList[index].Substring(0, 1)));
        
    }
    public float waitTime;
    IEnumerator SetText(TMP_Text Dialoftext)
    {
        textFinished = false;
        Dialoftext.text = null;
        for (int i = 0; i < text.Length; i++)
        {
            Dialoftext.text += text[i];
            yield return new WaitForSeconds(waitTime);
        }
        textFinished = true;
        index++;
    }
    void GetCharacter(int characterID)
    {
        switch (characterID)
        {
            case 1:
                playerDialogBox.SetActive(true);
                npcDialogBox.SetActive(false);
                StartCoroutine(SetText(playerText));
                break;
            case 2:
                npcDialogBox.SetActive(true);
                playerDialogBox.SetActive(false);
                StartCoroutine(SetText(npcText));

                break;
            default:
                break;
        }
    }
}
