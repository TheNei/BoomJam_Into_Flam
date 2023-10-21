using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimatorManager : MonoBehaviour
{
    private static AnimatorManager instance;
    public static AnimatorManager Instance
    {
        get
        {
            if (instance == null)
                instance = new AnimatorManager();
            return instance;
        }
    }

    public Image statement;
    //仪式开始
    public Animator theRitualBegins;
    public Animator tutorial;
    //开发人员
    public Animator ListOf;


    [Header("章节人物")]
    public Image[] chapterCharacterImage;

    [Header("移动动画")]
    public Animator characterAnim;
    public Animator NPC_1Anim;
    public Animator Back1Anim;
    public Animator Back2Anim;
    public Animator Back3Anim;
    public Animator PlayerAnim;
    
    [Header("过渡动画")]
    public Animator transitionAnim;
    public Animator statementonAnim;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        foreach (var item in chapterCharacterImage)
        {
            item.gameObject.SetActive(false);
        }
    }

    //NPC移动
    public void StartAnimator()
    {
        chapterCharacterImage[0].gameObject.SetActive(true);
        characterAnim.SetTrigger("characterMove");
    }
    public void StartNPC_1Animator(string Trigger)
    {
        chapterCharacterImage[1].gameObject.SetActive(true);
        NPC_1Anim.SetTrigger(Trigger);
    }

    //过渡
    public IEnumerator StartTransition(string value)
    {
        transitionAnim.SetTrigger("Trigger");
        yield return new WaitForSeconds(2f);
        DialogueSystemT.Instance.OpenPanel(value);
        yield return new WaitForSeconds(0.5f);
        transitionAnim.SetTrigger("Trigger");
        yield return new WaitForSeconds(2.1f);
        if (value == "#")
        {
            theRitualBegins.SetTrigger("ritualTrigger");
        }
    }

    //切换地图
    public IEnumerator ChangeBackground(string name, int position)
    {
        transitionAnim.SetTrigger("Trigger");
        yield return new WaitForSeconds(2f);
        if (name == "宫殿")
        {
            DialogueSystemT.Instance.chapterPlayerTransform.anchoredPosition = new Vector2(-470, 0);
            DialogueSystemT.Instance.chapterManinTransform.anchoredPosition = new Vector2(100, 50);
            DialogueSystemT.Instance.chapterNPC_Transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 650);
            DialogueSystemT.Instance.levelNPC_Transform.anchoredPosition = new Vector2(700, 0);
            DialogueSystemT.Instance.levelNPC_Transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 535);
            DialogueSystemT.Instance.levelNPC_Transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1080);
            yield return new WaitForSeconds(1.5f);
            DialogueSystemT.Instance.isDialogue = true;
        }
        DialogueSystemT.Instance.backImage.sprite = GetSprites.Instance.backImageDic[name];
        yield return new WaitForSeconds(0.5f);
        transitionAnim.SetTrigger("Trigger");
    }
    //出版声明
    IEnumerator TransitionAnim()
    {
        statement.gameObject.SetActive(true);
        statementonAnim.SetTrigger("Trigger");
        yield return new WaitForSeconds(5f);
        statementonAnim.SetTrigger("closeTrigger");
        DialogueSystemT.Instance.Panel[0].gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        statement.gameObject.SetActive(false);
    }
    //启动出版声明
    public void StartCoroutine()
    {
        StartCoroutine(TransitionAnim());
    }


}
