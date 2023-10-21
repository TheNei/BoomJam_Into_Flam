using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSprites : MonoBehaviour
{
    private static GetSprites instance;
    public static GetSprites Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GetSprites();
            }
            return instance;
        }
    }

    [Header("角色图片Sprites")]
    public List<Sprite> sprites = new List<Sprite>();

    [Header("背景图片Sprites")]
    public List<Sprite> backSprites = new List<Sprite>();
    
    [Header("剧情文本")]
    public List<TextAsset> plotText = new List<TextAsset>();
    
    [Header("幕间图片Sprites")]
    public List<Sprite> interludeSprites = new List<Sprite>();

    public Dictionary<string, Sprite> characterImageDic = new Dictionary<string, Sprite>();
    public Dictionary<string, Sprite> backImageDic = new Dictionary<string, Sprite>();
    public Dictionary<string, TextAsset> plotTextDic = new Dictionary<string, TextAsset>();
    public Dictionary<string, Sprite> interludeDic = new Dictionary<string, Sprite>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        StartCoroutine(GetResources());
    }
    //初始化
    IEnumerator GetResources()
    {
        GetCharacterSprites();
        GetBackSprites();
        GetPlotText();
        InterludeSprite();
        yield return null;
    }
    //人物
    void GetCharacterSprites()
    {
        characterImageDic["旅人"] = sprites[0];
        characterImageDic["守门人【老年】"] = sprites[1];
        characterImageDic["守门人【幼年】"] = sprites[2];
        characterImageDic["守门人【成年】"] = sprites[3];
        characterImageDic["白衣女（第一关）"] = sprites[4];
        characterImageDic["白衣女（第二关）"] = sprites[5];
        characterImageDic["白衣女（第三关）"] = sprites[6];
        characterImageDic["白衣女（第四关）"] = sprites[7];
        characterImageDic["学者1"] = sprites[8];
        characterImageDic["学者2"] = sprites[9];
        characterImageDic["学者3"] = sprites[10];
        characterImageDic["俄吕司"] = sprites[11];
        characterImageDic["俄吕司2"] = sprites[12];
        characterImageDic["骰子人"] = sprites[13];
        characterImageDic["路人女"] = sprites[14];
        characterImageDic["路人男"] = sprites[15];
    }
    //场景
    void GetBackSprites()
    {
        backImageDic["城外"] = backSprites[0];
        backImageDic["城外cg"] = backSprites[1];
        backImageDic["城内"] = backSprites[2];
        backImageDic["城内cg"] = backSprites[3];
        backImageDic["红河桥"] = backSprites[4];
        backImageDic["红河桥cg"] = backSprites[5];
        backImageDic["白桦林"] = backSprites[6];
        backImageDic["白桦林cg"] = backSprites[7];
        backImageDic["遇到领主"] = backSprites[8];
        backImageDic["遇到领主cg"] = backSprites[9];
        backImageDic["宫殿"] = backSprites[10];
    }
    //剧情文本
    void GetPlotText()
    {
        plotTextDic["第一章"] = plotText[0];
        plotTextDic["第二章"] = plotText[1];
        plotTextDic["第三章"] = plotText[2];
        plotTextDic["第四章"] = plotText[3];
    }
    //幕间
    void InterludeSprite()
    {
        interludeDic["学者"] = interludeSprites[0];
        interludeDic["少女"] = interludeSprites[1];
        interludeDic["守门人"] = interludeSprites[2];
        interludeDic["领主"] = interludeSprites[3];
    }


}
