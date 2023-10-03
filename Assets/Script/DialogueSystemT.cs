using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using UnityEngine.U2D;

public class DialogueSystemT : MonoBehaviour
{
    [Header("对话框UI")]
    public TMP_Text playerText;
    public TMP_Text npcText;
    public GameObject playerDialogBox;
    public GameObject npcDialogBox;

    [Header("剧情文本")]
    public TextAsset textFile;
    private int index;

    [Header("角色图片UI及列表")]
    public Image playerImage;
    public Image npcImage;
    public List<Sprite> sprites = new List<Sprite>();

    Dictionary<string,Sprite> imageDic = new Dictionary<string,Sprite>();

    bool textFinished;
    string[] fileDate;

    private void Awake()
    {
        imageDic["旅人"] = sprites[0];
        imageDic["青年"] = sprites[1];
    }
    void Start()
    {
        textFinished = true;
        ReadTextFile(textFile);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && index==fileDate.Length-1)
        {
            playerDialogBox.SetActive(false);
            npcDialogBox.SetActive(false);
            index = 1;
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

    public void ReadTextFile(TextAsset file)
    {
        index = 1;
        fileDate = file.text.Split('\n');
    }
    public void DilogueLoading()
    {
        for (int i = 1; i < fileDate.Length; i++)
        {
            if (i == index)
            {
                string[] line = fileDate[i].Split(",");
                UpdateImage(line[0], line[1]);
                GetCharacter(int.Parse(line[1]), line[2]);
                index++;
                break;
            }
            
        }
    }
    public void UpdateImage(string name,string position)
    {
        if (position.Equals("0"))
        {
            playerImage.sprite=imageDic[name]; 
        }
        else if (position.Equals("1"))
        {
            npcImage.sprite=imageDic[name];
        }
    }

    public void GetCharacter(int position,string content)
    {
        switch (position)
        {
            case 0:
                playerDialogBox.SetActive(true);
                npcDialogBox.SetActive(false);
                StartCoroutine(SetText(content,playerText));
                break;
            case 1:
                npcDialogBox.SetActive(true);
                playerDialogBox.SetActive(false);
                StartCoroutine(SetText(content,npcText));
                break;
            default:
                break;
        }
    }

    [Header("文字显示间隔时间")]
    public float waitTime;
    IEnumerator SetText(string content, TMP_Text DialogText)
    {
        textFinished = false;
        DialogText.text = null;
        for (int i = 0; i < content.Length; i++)
        {
            DialogText.text += content[i];
            yield return new WaitForSeconds(waitTime);
        }
        textFinished = true;
    }

}
