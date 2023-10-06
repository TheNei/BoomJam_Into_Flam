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
    private static DialogueSystemT instance;
    public static DialogueSystemT Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DialogueSystemT();
            }
            return instance;
        }
    }
    [Header("对话框UI")]
    public TMP_Text playerText;
    public TMP_Text npcText;
    public TMP_Text narrationText;
    public TMP_Text chapterPlayerText;
    public TMP_Text chapterNPC_Text;
    public GameObject playerDialogBox;
    public GameObject npcDialogBox;
    public GameObject narrationDialogBox;
    public GameObject chapterPlayerDialogBox;
    public GameObject chapterNPC_DialogBox;

    [Header("剧情文本")]
    public TextAsset textFile;
    private int index;

    [Header("角色图片UI及列表")]
    public Image playerImage;
    public Image npcImage;
    public List<Sprite> sprites = new List<Sprite>();

    [Header("跳转界面")]
    public Image[] Panel;

    Dictionary<string,Sprite> imageDic = new Dictionary<string,Sprite>();
    public Animator anim;
    bool textFinished;
    public bool isDialogue;
    string[] fileData;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        imageDic["旅人"] = sprites[0];
        imageDic["守门人【老年】"] = sprites[1];
        imageDic["守门人【幼年】"] = sprites[2];
        imageDic["守门人【成年】"] = sprites[3];
    }
    void Start()
    {
        SetDialogueBox(false, false);
        textFinished = true;
        ReadTextFile(textFile);
    }

    void Update()
    {
        if (npcImage.sprite != null)
        {
            anim.SetBool("isDiaplay", true);
            npcImage.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && index == fileData.Length - 1 && textFinished == true && fileData != null)
        {
            SetDialogueBox(false, false);
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
        fileData = file.text.Split('\n');
    }
    public void DilogueLoading()
    {
        if(fileData != null && isDialogue == true)
        {
            for (int i = 1; i < fileData.Length; i++)
            {
                if (i == index)
                {
                    string[] line = fileData[i].Split(",");
                    if (line[0] == "END")  
                    {
                        SetDialogueBox(false, false);
                        SetchapterDialogueBox(false, false, false);
                        isDialogue = false;
                        if (line[3] != "\r")
                            OpenPanel(line[3]);                    
                    }
                    if (line[0] != "旁白")
                    {
                        UpdateImage(line[0], line[1]);
                    }
                    if (line[2]!= "")
                    {
                        GetCharacter(int.Parse(line[1]), line[2]);
                    }
                    index++;
                    break;
                }

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
                SetDialogueBox(true, false);
                StartCoroutine(SetText(content, playerText));
                break;
            case 1:
                SetDialogueBox(false, true);
                StartCoroutine(SetText(content, npcText));
                break;
            case 2:
                break;          
            case 3:
                SetchapterDialogueBox(true, false, false);
                StartCoroutine(SetText(content, narrationText));
                break;
            case 4:
                SetchapterDialogueBox(false, true, false);
                StartCoroutine(SetText(content, chapterPlayerText));
                break;
            case 5:
                SetchapterDialogueBox(false, false, true);
                StartCoroutine(SetText(content, chapterNPC_Text));
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
    void SetDialogueBox(bool player,bool npc)
    {
        playerDialogBox.SetActive(player);
        npcDialogBox.SetActive(npc); 
    }
    void SetchapterDialogueBox(bool narration, bool player, bool npc)
    {
        narrationDialogBox.SetActive(narration);
        chapterPlayerDialogBox.SetActive(player);
        chapterNPC_DialogBox.SetActive(npc);
    }
    void OpenPanel(string value)
    {
        switch (value)
        {
            case "0\r":
                Panel[int.Parse(value)].gameObject.SetActive(true);
                isDialogue = true;
                break;
            case "播放\r":
                AnimatorManager.Instance.StartAnimator();
                isDialogue = true;
                break;
        }
    }
}
