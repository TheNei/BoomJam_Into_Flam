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

    [Header("章节人物")]
    public Image[] chapterCharacterImage;

    [Header("移动动画")]
    public Animator characterAnim;

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

    public void StartAnimator()
    {
        chapterCharacterImage[0].gameObject.SetActive(true);
        characterAnim.SetTrigger("characterMove");
    }

}
