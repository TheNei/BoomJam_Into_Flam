using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
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
       /* DontDestroyOnLoad(this);*/
    }
    [HideInInspector]
    public int gameRound;
    [HideInInspector]
    public int roundScore;
    public int targetRound;
    public int targetScore;
    public TextMeshProUGUI round;
    public TextMeshProUGUI score;
    public TextMeshProUGUI warn;
    public TextMeshProUGUI targetround;
    public TextMeshProUGUI targetscore;
    public Button ReLoad;
    public TextMeshProUGUI level;
    public TileBase winTile;
    private Tilemap tilemap;
    private Movement player;
    private GameObject diceEvent;
    public GameObject getWeapon;
    public Button dice;
    public Button excuteDice;
    public Button cancelDice;
   private GodWeapons weapon;
    public Image weaponSprite;
    public Button getWeaponButton;
    public List<Sprite> weaponimage = new List<Sprite>();
    private Fade fade;
    private void Start()
    {
        fade = GameObject.FindAnyObjectByType<Fade>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        tilemap = GameObject.FindAnyObjectByType<Tilemap>();
        weapon = GameObject.FindAnyObjectByType<GodWeapons>();
     /*   weaponSprite = getWeapon.transform.Find("Weapon").GetComponent<Image>();*/
        getWeapon.gameObject.SetActive(false);
        ReLoad.onClick.AddListener(() =>
        {
            SceneContreoller.LoadSceneByIndex(SceneManager.GetActiveScene().buildIndex);
        });
        level.text = SceneContreoller.GetSceneName();
        diceEvent = GameObject.FindGameObjectWithTag("IsLoadNextLevel").gameObject;
        diceEvent.gameObject.SetActive(false);
        dice.onClick.AddListener(DiceEvent);
        excuteDice.onClick.AddListener(ExcuteDiceEvent);
        cancelDice.onClick.AddListener(CancelDiceEvent);
        for(int i = 0;i < 3;i++)
        {
            weaponimage.Add(Resources.Load<Sprite>("Sprites/" + i + 1.ToString()));
        }
    }
    private void Update()
    {
        RefreshUI();
    }
    private void  RefreshUI()
    {
        round.text ="当前回合 : " + gameRound;
        score.text  = "当前分数: " + roundScore;
        targetround.text = "目标回合 :" + targetRound;
        targetscore.text = "目标分数 :" + targetScore;
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
    public bool IsWin
    {
        get
        {
            if (roundScore >= targetScore && gameRound <= targetRound)
            {
                return true;
            }
            else
               {
                print("Score or Round");
                return false;}
        }
    }
    public void IFWin()
    {
       if(tilemap.GetTile(tilemap.WorldToCell(player.transform.position)) == winTile)
        {
            print("IsWin :" + IsWin);
            if(IsWin)
            {if (level.text == "1-4")
                {
                    getWeaponButton.onClick.AddListener(getFlyEvent);
                    ShowWeapon();
                    return;
                }
                else if (level.text == "2-4")
                {
                    getWeaponButton.onClick.AddListener(getDeadEvent);
                    ShowWeapon();
                }
                else if (level.text == "3-4")
                {
                    getWeaponButton.onClick.AddListener(getFreezeEvent);
                    ShowWeapon();
                }
                ShowDebug("你赢了");
                Invoke("HideDebug", 1.5f);
                fade.PlayAnimtion();
                Invoke("LoadNextScene", 3f);
                StopAllCoroutines();
            }
        }
       else
        {
            print("it's not exist");
        }
    }
    public void StopAllCoroutinesInGame()
    {
        MonoBehaviour[] scriptInstances = FindObjectsOfType<MonoBehaviour>();

        foreach (MonoBehaviour script in scriptInstances)
        {
            script.StopAllCoroutines();
        }
    }
   public void DiceEvent()
    {
        diceEvent.gameObject.SetActive(true);
    }
    public void getFlyEvent()
    {
       /* weaponSprite.sprite = weaponimage[0];*/
        weapon.fly.gameObject.SetActive(true);
        weapon.fly.interactable = true;
        getWeapon.gameObject.SetActive(false);
        Invoke("LoadNextScene", 2f);
    }
    public void getDeadEvent()
    {
     /*   weaponSprite.sprite = weaponimage[1];*/
        weapon.dead.gameObject.SetActive(true);
        weapon.dead.interactable = true;
        getWeapon.gameObject.SetActive(false);
        Invoke("LoadNextScene", 2f);
    }
    public void getFreezeEvent()
    {
      /*  weaponSprite.sprite = weaponimage[2];*/
        weapon.Freeze.gameObject.SetActive(true);
        weapon.Freeze.interactable = true;
        getWeapon.gameObject.SetActive(false);
        Invoke("LoadNextScene", 2f);
    }
    public void ExcuteDiceEvent()
    {
        diceEvent.gameObject.SetActive(false);
        Invoke("LoadNextScene", 2f);
    }
    public void CancelDiceEvent()
    {
        diceEvent.gameObject.SetActive(false);
    }
    void LoadNextScene()
    {
        SceneContreoller.LoadNextScene();
    }
    void ShowWeapon()
    {
        getWeapon.gameObject.SetActive(true);
    }
}
