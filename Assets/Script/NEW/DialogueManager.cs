using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextAsset TextFile;
    public TMP_Text Text;
    public Sprite PlayerSprite;
    public Sprite NPCSprite;
    public Image Player;
    public Image NPC;
    string[] fileData;
    bool IsDialogue;
    int index;
    public bool isFinish;
    private Fade fade;
    private void Start()
    {
        fade = GameObject.FindAnyObjectByType<Fade>();
        IsDialogue = true;
        fileData = TextFile.text.Split('\n');
        Player.sprite = PlayerSprite;
        NPC.sprite = NPCSprite;
        Text.text = string.Empty;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DilogueLoading();
        }
        if(isFinish)
        {
            fade.PlayAnimtion();
            SceneContreoller.LoadNextScene();
        }
    }
    void DilogueLoading()
    {
        if (IsDialogue)
        {
            for (int i = 0; i < fileData.Length; i++)
            {
                if (index == i)
                {
                    IsDialogue = false;
                    Text.text = string.Empty;
                    StartCoroutine(SetText(fileData[i]));
                    index++;
                    break;
                }
            }
        }
        if (index == fileData.Length)
        {
            isFinish = true;
        }
    }

    public IEnumerator SetText(string content)
    {
        for (int i = 0; i < content.Length; i++)
        {
            Text.text += content[i];
            yield return new WaitForSeconds(0.1f);
        }
        IsDialogue = true;
    }
}
