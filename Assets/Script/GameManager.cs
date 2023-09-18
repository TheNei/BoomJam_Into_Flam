using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private GameManager() { }
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    instance = new GameObject("GameManager").AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }
    public int gameRound;
    public int roundScore;
    public int targetRound;
    public int targetScore;
    public TextMeshProUGUI round;
    public TextMeshProUGUI score;
    public TextMeshProUGUI warn;

   
    private void Update()
    {
        RefreshUI();
    }
    private void  RefreshUI()
    {
        round.text ="Round : " + gameRound;
        score.text  = "Score: " + roundScore;
        
    }
   public void ShowDebug(string stn)
    {
        warn.text = stn;
        warn.gameObject.SetActive(true);
        RefreshUI();
    }
   public void HideDebug()
    {
        warn.text = string.Empty;
        warn.gameObject.SetActive(false);
    }
    public bool isWin
    {
        get
        {
            if (gameRound <= targetRound && roundScore >= roundScore)
            {
                return true;
            }
            else
                return false;
        }
    }

}
