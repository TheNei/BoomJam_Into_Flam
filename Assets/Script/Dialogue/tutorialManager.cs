using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorialManager : MonoBehaviour
{
    public static tutorialManager instance;

    public Sprite[] tutorialImage;
    Dictionary<string, Sprite> tutorialDic = new Dictionary<string, Sprite>();

    Image tutorial;
    private void Awake()
    {
        instance = this;

        tutorialDic["1-1"] = tutorialImage[0];
        tutorialDic["1-2"] = tutorialImage[1];
        tutorialDic["1-5"] = tutorialImage[2];
        tutorialDic["2-1"] = tutorialImage[3];
        tutorialDic["2-5"] = tutorialImage[4];
        tutorialDic["3-5"] = tutorialImage[5];
        tutorialDic["4-1"] = tutorialImage[6];
        tutorialDic["4-7"] = tutorialImage[7];
    }
    private void Start()
    {
        GameObject gameobject = GameObject.FindGameObjectWithTag("tutorial");
        tutorial = gameObject.GetComponent<Image>();
    }

    public void OpenTutorial(string number)
    {
        tutorial.gameObject.SetActive(true);
        tutorial.sprite = tutorialDic[number];
    }

    public void CloseTutorial()
    {
        tutorial.gameObject.SetActive(false);
    }
}
