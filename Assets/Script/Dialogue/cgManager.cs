using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class cgManager : MonoBehaviour
{
    public static cgManager instance;
    private void Awake()
    {
        instance = this;
    }
    //开启对话
    void SetIsDialogue()
    {
        DialogueSystemT.Instance.isDialogue = true;
    }

    //路人移动
    void Dia()
    {
        DialogueSystemT.Instance.isDialogue = true;
        DialogueSystemT.Instance.DilogueLoading();
        DialogueSystemT.Instance.isDialogue = false;
    }
    //启动2号雪地背景
    void BackAnimator()
    {
        AnimatorManager.Instance.Back2Anim.SetTrigger("BackTrigger");
    }
    //主角停止、雪地背景停止
    void Back1Animator()
    {
        AnimatorManager.Instance.PlayerAnim.SetBool("PlayerBool", false);
        AnimatorManager.Instance.Back1Anim.SetFloat("Float", 0f);
        AnimatorManager.Instance.Back2Anim.SetFloat("Back2Float", 0f);
        AnimatorManager.Instance.Back3Anim.SetFloat("Back3Float", 0f);
    }

    //雪地背景可以移动
    public void Back2Animator()
    {
        AnimatorManager.Instance.Back1Anim.SetFloat("Float", 1f);
        AnimatorManager.Instance.Back2Anim.SetFloat("Back2Float", 1f);
        AnimatorManager.Instance.Back3Anim.SetFloat("Back3Float", 1f);
    }
    //角色走动
    public void PlayerMove()
    {
        if (!AnimatorManager.Instance.PlayerAnim.GetBool("PlayerBool"))
        {
            AnimatorManager.Instance.PlayerAnim.SetBool("PlayerBool", true);
        }
    }

    public void OpenTutorial()
    {
        DialogueSystemT.Instance.tutorialButton.enabled = true;
        AnimatorManager.Instance.tutorial.SetTrigger("tutorialTrigger");
        DialogueSystemT.Instance.isDialogue = false;
    }
    public void CloseTutorial()
    {
        DialogueSystemT.Instance.tutorialButton.enabled = false;
        AnimatorManager.Instance.tutorial.SetTrigger("closeTrigger");
    }
    //关闭人员名单
    void CloseListOF()
    {
        DialogueSystemT.Instance.ListOF.SetActive(false);
        DialogueSystemT.Instance.isDialogue = true;
    }
    
    void SetAudioClip(string name)
    {
        SoundManager.instance.SetAudioClip2(name);
    }
}
