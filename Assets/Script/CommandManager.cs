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
    public DrawLine line;
    private Backtrack back;
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
    public List<Command> childBtns = new List<Command>();
    public List<Enhance> enhanceBtns = new List<Enhance>();

    public List<Image> imageBox = new List<Image>();
    public List<Image> enhanceBox = new List<Image>();
    public GameObject commandLine;
    public GameObject enhanceLine;
    private Movement player;
    public bool isExcute;
    private List<MoveCommand> commandLists = new List<MoveCommand>();
    private List<TextMeshProUGUI> textLists = new List<TextMeshProUGUI>();
    private List<TextMeshProUGUI> enhanceLists = new List<TextMeshProUGUI>();
    private Button excute;
    #endregion
    private void Start()
    {
        back = GameObject.FindAnyObjectByType<Backtrack>().GetComponent<Backtrack>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        excute = GameObject.Find("Excute").GetComponent<Button>();
        excute.onClick.AddListener(() => { GetInput(MoveCommand.Excute, 0); });
        for (int i = 0; i < commandLine.transform.childCount; i++)
        {
            int temp = i;
            textLists.Add(commandLine.transform.GetChild(temp).GetComponentInChildren<TextMeshProUGUI>());
            imageBox.Add(commandLine.transform.GetChild(temp).GetComponent<Image>());
            if (textLists == null)
                print("NULL");
            if (imageBox == null)
                print("image is null");
            if (textLists[temp].text != string.Empty)
                textLists[temp].text = string.Empty;
        }
        for (int i = 0; i < enhanceLine.transform.childCount; i++)
        {
            int temp = i;
            enhanceLists.Add(enhanceLine.transform.GetChild(temp).GetComponentInChildren<TextMeshProUGUI>());
            enhanceBox.Add(enhanceLine.transform.GetChild(temp).GetComponent<Image>());
            if (enhanceLists.Count == 0)
            {
                print("NULL");
                break;
            }
            if (enhanceLists[temp].text != string.Empty)
                enhanceLists[temp].text = string.Empty;
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
        }
        foreach (var text in textLists)
        {
            text.text = string.Empty;
        }
        foreach (var text in enhanceLists)
        {
            text.text = string.Empty;
        }
    }
    public void GetInput(MoveCommand command, int index)
    {
        if (command == (MoveCommand)7 && commandLists.Count == 3)
        {
            isExcute = true;
            GameManager.Instance.gameRound++;
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
            line.SetNewPosition(GetPosition(index));
            while (true)
            {
                if (textLists[index].text == string.Empty)
                {
                    textLists[index].text = command.ToString();

                    break;
                }
                else
                {
                    print("It's not null");
                    break;
                }
            }
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

    public void GetEnhance(Enhance enhance, int index)
    {

       if (enhanceBtns[index] == Enhance.Default)
        {
            enhanceBtns[index] = enhance;
            line.SetNewPosition(GetPosition(index));
        }

        while (true)
        {
            if (enhanceLists[index].text == string.Empty)
            {
                enhanceLists[index].text = enhance.ToString();
                break;
            }
            else
            {
                print("enhance is not null");
                break;
            }
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
                print(temp);
                back.SetList(Values[commandLists[temp]], temp);
                StartCoroutine(player.PlayerMovement(Values[commandLists[temp]]));
                yield return new WaitForSeconds(1.0f);
                continue;
            }
            if (commandLists[temp] != MoveCommand.JUMP)
            {
                if (enhanceBtns[temp] == Enhance.Force)
                {
                    back.SetList(Values[commandLists[temp]], temp);
                    StartCoroutine(player.PlayerMovement((Values[commandLists[temp]] * 2.0f)));
                    yield return new WaitForSeconds(1.0f);
                }

                if (enhanceBtns[temp] == Enhance.Anti)
                {
                    back.SetList(Values[commandLists[temp]], temp);
                    StartCoroutine(player.PlayerMovement((Values[commandLists[temp]] * -1.0f)));
                    yield return new WaitForSeconds(1.0f);
                }
            }
            else
            {
                if (enhanceBtns[temp] == Enhance.Force)
                {
                    back.SetList(Values[commandLists[temp]], temp);
                    StartCoroutine(player.PlayerMovement((Values[commandLists[temp]] + Vector3.up)));
                    yield return new WaitForSeconds(1.0f);
                }
                
                if (enhanceBtns[temp] == Enhance.Anti)
                {
                    back.SetList(Values[commandLists[temp]], temp);
                    StartCoroutine(player.PlayerMovement((Values[commandLists[temp]] * -1.0f)));
                    yield return new WaitForSeconds(1.0f);
                }
                    
            }

        }
        ClearCommand();

        yield return null;
    }
}

