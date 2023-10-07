using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CommandManager : MonoBehaviour
{
    public enum Enhance
    {
        Anti = 1,
        Force = 2,
        Default = 3
    }
    public enum MoveCommand
    {
        UP = 1,
        DOWN = 2,
        LEFT = 3,
        RIGHT = 4,
        JUMP = 5,
        Stay = 6,
        Excute = 7
    }
    public Dictionary<MoveCommand, Vector3> Values = new Dictionary<MoveCommand, Vector3>()
    {
        { MoveCommand.UP, Vector3.up},
        { MoveCommand.DOWN, Vector3.down },
        { MoveCommand.LEFT, -Vector3.right },
        { MoveCommand.RIGHT, Vector3.right },
        { MoveCommand.JUMP, Vector3.up * 2},
        {MoveCommand.Stay,Vector3.zero },
        {MoveCommand.Excute,Vector3.zero }
    };
    private static CommandManager instance;
    private CommandManager() { }
    public static CommandManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<CommandManager>();
                if (instance == null)
                {
                    instance = new GameObject("CommandManager").AddComponent<CommandManager>();
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
    #region
    [Header("MoveBtn")]
   /* public List<Command> childBtns = new List<Command>();*/
   

    public List<Image> commandBox = new List<Image>();
    public List<Image> enhanceBox = new List<Image>();
    public GameObject commandLine;
    public GameObject enhanceLine;
    private Movement player;
    private SpawnTile spawner;
    /*    public bool isExcute;*/
    public float delay = 0.8f;
    private List<MoveCommand> commandLists = new List<MoveCommand>();
    private List<Enhance> enhanceBtns = new List<Enhance>();
    public Button excute;
    Quaternion targetRot;
    Quaternion defaultRot;
    #endregion
    private void Start()
    {
        spawner = GameObject.FindAnyObjectByType<UnityEngine.Tilemaps.Tilemap>().GetComponent<SpawnTile>();
        targetRot = Quaternion.Euler(0f, 0f, 0f);
        defaultRot = Quaternion.Euler(0f, 0f, 45f);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        excute = GameObject.Find("Excute").GetComponent<Button>();
        excute.onClick.AddListener(() => { GetInput(MoveCommand.Excute, 0,excute.GetComponent<Image>().sprite); });
        for (int i = 0; i < commandLine.transform.childCount; i++)
        {
            int temp = i;
            commandBox.Add(commandLine.transform.GetChild(temp).GetComponent<Image>());
          
            if (commandBox == null)
                print("image is null");
     
        }
        for (int i = 0; i < enhanceLine.transform.childCount; i++)
        {
            int temp = i;
      
            enhanceBox.Add(enhanceLine.transform.GetChild(temp).GetComponent<Image>());
          
        }
        for(int i = 0;i < 3;i++)
        {
            enhanceBtns.Add(Enhance.Default);
        }

    }
    void ClearCommand()
    {
        commandLists.Clear();
        for (int i = 0; i < 3; i++)
        {
            enhanceBtns[i] = Enhance.Default;
            enhanceBox[i].sprite = null;
          /*  enhanceBox[i].transform.rotation = defaultRot;*/
            commandBox[i].sprite = null;
            commandBox[i].transform.rotation = defaultRot;
        }

       
    }
    public void GetInput(MoveCommand command, int index, Sprite sprite)
    {
        if (command == (MoveCommand)7 && commandLists.Count == 3)
        {
      
            GameManager.Instance.gameRound++;
            spawner.Move();
            StartCoroutine(Excute());
            return;
        }
        else if (command == (MoveCommand)7 && commandLists.Count < 3)
        {
            return;
        }

        if (command != (MoveCommand)7 && commandLists.Count < 3)
        {
            commandLists.Add(command);
            commandBox[index].sprite = sprite;
            commandBox[index].transform.rotation = targetRot;
        }

    }
    Vector3 GetPosition(int index)
    {
        if(index > commandLists.Count)
        {
            print("it's not");
            return Vector3.zero;
        }
        if(enhanceBtns[index] == Enhance.Default)
        {
            return Values[commandLists[index]];
        }
        else
        {
            if (commandLists[index] != MoveCommand.JUMP)
            {
                if (enhanceBtns[index] == Enhance.Anti)
                {
                    return Values[commandLists[index]] * -1;
                }
                else if (enhanceBtns[index] == Enhance.Force)
                {
                    return Values[commandLists[index]] * 2;
                }
            }

          else
            {
                if (enhanceBtns[index] == Enhance.Anti)
                {
                    return Values[commandLists[index]] * -1;
                }
                else if (enhanceBtns[index] == Enhance.Force)
                {
                    return Values[commandLists[index]] + Vector3.up;
                }
            }

        }
        return Vector3.zero;
    }

    public void GetEnhance(Enhance enhance, int index,Sprite sprite)
    {

       if (enhanceBtns[index] == Enhance.Default)
        {
            enhanceBtns[index] = enhance;
    /*        enhanceBox[index].transform.rotation = targetRot;*/
            enhanceBox[index].sprite = sprite;
        }
       else
        {
            print("it's full");
        }
       
    }
    IEnumerator Excute()
    {
        if (commandLists.Count == 0)
        {
            print("it doesn't excute");
           yield return null;
        }
        for (int i = 0; i < commandLists.Count; i++)
        {
            int temp = i;

            if (enhanceBtns[temp] == Enhance.Default)
            {
                
                StartCoroutine(player.PlayerMovement(Values[commandLists[temp]]));
                yield return new WaitForSeconds(delay);
                continue;
            }
                if (commandLists[temp] != MoveCommand.JUMP)
                {
                    if (enhanceBtns[temp] == Enhance.Force)
                    {
                  
                        StartCoroutine(player.PlayerJumpMovement((Values[commandLists[temp]] * 2.0f)));
                        yield return new WaitForSeconds(delay);
                    }

                    if (enhanceBtns[temp] == Enhance.Anti)
                    {
                    
                        StartCoroutine(player.PlayerMovement((Values[commandLists[temp]] * -1.0f)));
                        yield return new WaitForSeconds(delay);
                    }
                }
                else
                {
                    if (enhanceBtns[temp] == Enhance.Force)
                    {
              
                        StartCoroutine(player.PlayerMovement((Values[commandLists[temp]] + Vector3.up)));
                        yield return new WaitForSeconds(delay);
                    }

                    if (enhanceBtns[temp] == Enhance.Anti)
                    {
               
                        StartCoroutine(player.PlayerMovement((Values[commandLists[temp]] * -1.0f)));
                        yield return new WaitForSeconds(delay);
                    }

                
            }
        }
        ClearCommand();

        yield return null;
    }
}

