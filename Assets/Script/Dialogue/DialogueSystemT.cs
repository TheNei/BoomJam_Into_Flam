using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System;

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

    public int index;
    public Animator anim;
    public Animator cgAnim;
    bool textFinished;
    public bool isDialogue;
    string[] fileData;
    List<string> eventList = new List<string>();
    GameObject chapterPanel;
    public GameObject gameOver;
    public GameObject ListOF;
    public ParticleSystem chapterParticle;
    public ParticleSystem levelParticle;
    public Button playerButton;
    public Image tutorialButton;
    public Image[] back;
    public RectTransform chapterPlayerTransform;
    public RectTransform chapterManinTransform;
    public RectTransform chapterNPC_Transform;
    public RectTransform levelNPC_Transform;

    [Header("对话框UI")]
    public TMP_Text playerText;
    public TMP_Text npcText;
    public TMP_Text narrationText;
    public TMP_Text chapterPlayerText;
    public TMP_Text chapterNPC_Text;
    public TMP_Text npcDescribeText;
    public TMP_Text npc_Text;
    public GameObject playerDialogBox;
    public GameObject npcDialogBox;
    public GameObject narrationDialogBox;
    public GameObject chapterPlayerDialogBox;
    public GameObject chapterNPC_DialogBox;
    public GameObject npcDescribeDialogBox;
    public GameObject npc_DialogBox;

    [Header("角色图片UI组件")]
    public Image levelPlayerImage;
    public Image levelNpcImage;
    public Image chapterNpcImage;
    public Image npcImage;

    [Header("跳转界面")]
    public Image[] Panel;

    [Header("场景图片UI")]
    public Image backImage;
    public Image backCGImage;
    public Image InterludeImage;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        foreach (var item in Panel)
        {
            item.gameObject.SetActive(false);
        }
        npcDescribeDialogBox.SetActive(false);
        SetDialogueBox(false, false);
        textFinished = true;
        index = 1;
        chapterPanel = GameObject.FindGameObjectWithTag("chapter");
    }


    void Update()
    {
        if (npcImage.sprite != null)
        {
            anim.SetBool("isDiaplay", true);
            npcImage.gameObject.SetActive(true);
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (!textFinished)
            {
                waitTime = 0.01f;
                return;
            }
            if (textFinished)
            {
                waitTime = 0.1f;
                DilogueLoading();
            }
        }
    }

    //获取剧情文本
    public void ReadTextFile(TextAsset file)
    {
        fileData = file.text.Split('\n');
    }
    
    //读取剧情文本
    public void DilogueLoading()
    {
        if (fileData != null && isDialogue == true)
        {
            for (int i = 1; i < fileData.Length; i++)
            {
                if (i == index)
                {
                    string[] line = fileData[i].Split(",");
                    if (line[0] == "END")
                    {
                        isDialogue = false;
                        SetDialogueBox(false, false);
                        SetchapterDialogueBox(false, false, false);
                        npc_DialogBox.SetActive(false);
                        eventList.Clear();  
                    }
                    if (line[3] != "")
                    {
                        if (line[3] == "OVER")
                        {
                            SceneManager.LoadScene(0, LoadSceneMode.Single);
                        }
                        if (line[3] == "*" )
                        {
                            OpenPanel(line[3]);
                            index++;
                            break;
                        }
                        if (line[3] != "播放" && line[3] != "T" && line[3] != "播放1"&& line[3] != "播放2" && line[3] != "Z")
                        {
                            if (line[3] == "#")
                                isDialogue = false;
                            StartCoroutine(AnimatorManager.Instance.StartTransition(line[3]));
                        }
                        else if (line[3] == "播放")
                        {
                            AnimatorManager.Instance.StartAnimator();
                        }
                        else if (line[3] == "播放1")
                        {
                            AnimatorManager.Instance.StartNPC_1Animator("npcTrigger");
                            index++;
                            isDialogue = false;
                            break;
                        }
                        else if (line[3] == "播放2")
                        {
                            AnimatorManager.Instance.StartNPC_1Animator("npc_2Trigger");
                            index++;
                            isDialogue = false;
                            break;
                        }
                        else if (line[3] == "T" || line[3] == "Z")
                            OpenPanel(line[3]);
                    }
                    if (line[0] == "形象描写")
                    {
                        eventList.Add(line[2]);
                        index++;
                        DilogueLoading();
                        break;
                    }
                    //设置角色精灵图
                    if (line[0] != "旁白" && line[0] != "幕间" && line[0] != "END" && line[0] != "")
                    {
                        UpdateImage(line[0], int.Parse(line[1]));
                    }
                    
                    if (line[2] != "")
                    {
                        GetCharacter(int.Parse(line[1]), line[2]);
                    }
                    
                    index++;
                    break;
                }

            }
        }
    }
    //更换素材
    void UpdateImage(string name, int position)
    {
        switch(position)
        {
            case 0:
                levelPlayerImage.sprite = GetSprites.Instance.characterImageDic[name];
                break;
            case 1:
                if(levelNpcImage.sprite == GetSprites.Instance.characterImageDic["俄吕司2"])
                    SoundManager.instance.SetAudioClip("变身");
                levelNpcImage.sprite = GetSprites.Instance.characterImageDic[name];
                break;
            case 5:
                chapterNpcImage.sprite= GetSprites.Instance.characterImageDic[name];
                break;
            case 51:
                npcImage.sprite = GetSprites.Instance.characterImageDic[name];
                break;
            case 11:
                isDialogue = false;
                backCGImage.sprite = GetSprites.Instance.backImageDic[name];
                cgAnim.SetTrigger("cgTrigger");
                break;
            case 12:
                if(name == "宫殿")
                {
                    isDialogue = false;
                    SoundManager.instance.SetAudioClip(name);
                }
                if(name == "红河桥")
                {
                    SoundManager.instance.SetAudioClip2("小河");
                }
                if(name == "白桦林")
                {
                    SoundManager.instance.StopAudio2();
                }
                
                
                    
                StartCoroutine(AnimatorManager.Instance.ChangeBackground(name, position));
                break;
            case 13:
                InterludeImage.sprite = GetSprites.Instance.interludeDic[name];
                break;
            default:
                break;
        }
    }

    void GetCharacter(int position, string content)
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
            case 6:
                StartCoroutine(InterludeDialogue.Instance.InterludeDia(content, position - 6));
                break;
            case 7:
                StartCoroutine(InterludeDialogue.Instance.InterludeDia(content, position - 6));
                break;
            case 8:
                StartCoroutine(InterludeDialogue.Instance.InterludeDia(content, position - 6));
                break;
            case 9:
                StartCoroutine(InterludeDialogue.Instance.InterludeDia(content, position - 6)); 
                break;
            case 10:
                StartCoroutine(InterludeDialogue.Instance.InterludeDia(content, position - 6)); 
                break;
            case 51:
                if (npc_DialogBox.activeInHierarchy)
                    npc_DialogBox.SetActive(false);
                npc_DialogBox.SetActive(true);
                StartCoroutine(SetText(content, npc_Text));
                break;
            default:
                break;
        }
    }

    [Header("文字显示间隔时间")]
    public float waitTime;
    public IEnumerator SetText(string content, TMP_Text DialogText)
    {
        textFinished = false;
        DialogText.text = null;
        for (int i = 0; i < content.Length; i++)
        {
            DialogText.text += content[i];
            yield return new WaitForSeconds(waitTime);
        }
        yield return new WaitForSeconds(0.2f);
        textFinished = true;
        if (npc_DialogBox.activeInHierarchy)
        {
            isDialogue = true; 
        }
        else if (npcDescribeDialogBox.activeInHierarchy)
        {
            yield return new WaitForSeconds(2f);
            npcDescribeDialogBox.SetActive(false);
        }      
    }
    //设置关卡内对话框
    void SetDialogueBox(bool player, bool npc)
    {
        playerDialogBox.SetActive(player);
        npcDialogBox.SetActive(npc);
    }
    //设置章节间对话框
    void SetchapterDialogueBox(bool narration, bool player, bool npc)
    {
        narrationDialogBox.SetActive(narration);
        chapterPlayerDialogBox.SetActive(player);
        chapterNPC_DialogBox.SetActive(npc);
    }

    //切换场景
    public void OpenPanel(string value)
    {
        switch (value)
        {
            case "0":
                if (backImage.sprite != GetSprites.Instance.backImageDic["宫殿"])
                {
                    SoundManager.instance.SetAudioClip("非战斗");
                }
                chapterParticle.Play();
                levelParticle.Stop();
                chapterNpcImage.gameObject.SetActive(true);
                Panel[2].gameObject.SetActive(false);
                Panel[1].gameObject.SetActive(false);
                Panel[int.Parse(value)].gameObject.SetActive(true);
                isDialogue = true;
                break;
            case "1":
                SoundManager.instance.StopAudio2();
                Panel[0].gameObject.SetActive(false);
                Panel[2].gameObject.SetActive(false);
                Panel[int.Parse(value)].gameObject.SetActive(true);
                isDialogue = true;
                break;
            case "2":
                chapterParticle.Stop();
                levelParticle.Play();
                Panel[int.Parse(value)].gameObject.SetActive(true);
                isDialogue = true;
                break;         
            case "T":
                isDialogue = true;
                break;
            case "第二章":
                if (back[0].gameObject.activeInHierarchy)
                {
                    foreach (var item in back)
                    {
                        item.gameObject.SetActive(false);
                    }
                }
                SoundManager.instance.SetAudioClip("非战斗2");
                AnimatorManager.Instance.PlayerAnim.SetBool("PlayerBool", false);
                playerButton.enabled = true;
                fileData = GetSprites.Instance.plotTextDic[value].text.Split('\n');
                index = 1;
                isDialogue = false;
                break;
            case "第三章":
                SoundManager.instance.SetAudioClip("非战斗");
                AnimatorManager.Instance.PlayerAnim.SetBool("PlayerBool", false);
                fileData = GetSprites.Instance.plotTextDic[value].text.Split('\n');
                playerButton.enabled = true;
                index = 1;
                isDialogue = false;
                break;
            case "第四章":
                AnimatorManager.Instance.PlayerAnim.SetBool("PlayerBool", false);
                playerButton.enabled = true;
                fileData = GetSprites.Instance.plotTextDic[value].text.Split('\n');
                index = 1;
                isDialogue = false;
                break;
            case "$":
                gameOver.SetActive(true);
                break;
            case "Z":
                cgManager.instance.Back2Animator();
                AnimatorManager.Instance.characterAnim.SetTrigger("DisTrigger");
                AnimatorManager.Instance.PlayerAnim.SetBool("PlayerBool", true);
                AnimatorManager.Instance.PlayerAnim.SetTrigger("New Trigger");
                break;
            case "B":
                playerButton.enabled = true;
                if (!back[0].gameObject.activeInHierarchy)
                {
                    foreach (var item in back)
                    {
                        item.gameObject.SetActive(true);
                    }
                }
                chapterNpcImage.gameObject.SetActive(false);
                break;
            case "H":
                AnimatorManager.Instance.transitionAnim.SetTrigger("Trigger");
                isDialogue = true;
                break;  
            case "*":
                ListOF.SetActive(true);
                AnimatorManager.Instance.ListOf.SetTrigger("ListTrigger");
                break;
        }
    }

    //形象描述
    public void ButtonOnClickEvent()
    {
        if (eventList != null && textFinished && eventList.Count != 0) 
        {
            System.Random ran = new System.Random();
            int n = ran.Next(eventList.Count);
            npcDescribeDialogBox.SetActive(true);
            StartCoroutine(SetText(eventList[n], npcDescribeText));
        }

    }

    //开始按钮
    public void PlayGameButton(TextAsset file)
    {
        ReadTextFile(file);
        isDialogue = false;
    }
    //cg点击事件
    public void OnClickEvent()
    {
        isDialogue = true; 
        DilogueLoading();
    }

    //结局选项按钮
    public void GameOverEvent(TextAsset file)
    {
        fileData = file.text.Split('\n');
        playerButton.enabled = true;
        index = 1;
        isDialogue = true;
        gameOver.SetActive(false);
    }
}
