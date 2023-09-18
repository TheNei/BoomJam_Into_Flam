using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
public class Movement : MonoBehaviour
{
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
    private  Dictionary<MoveCommand, Vector3> Values = new Dictionary<MoveCommand, Vector3>()
    {
        { MoveCommand.UP, Vector3.up},
        { MoveCommand.DOWN, Vector3.down },
        { MoveCommand.LEFT, -Vector3.right },
        { MoveCommand.RIGHT, Vector3.right },
        { MoveCommand.JUMP, Vector3.up * 2},
        {MoveCommand.Stay,Vector3.zero },
        {MoveCommand.Excute,Vector3.zero }
    };  
    public Tilemap cellMap;
    private float cellsize = 2.0f;
    private Vector3Int currentCell;
    private Vector3 targetPos;
    private MoveCommand currentMoveCommand;
    private List<MoveCommand> moveLists = new List<MoveCommand>();
    private TileFlag isFlag;
    [Header("Button")]
    public Button[] inputBtns = new Button[7];
    private void Awake()
    {
        isFlag = GameObject.FindObjectOfType<TileFlag>();
    }
    private void Start()
    {
        BoundsInt bounds = cellMap.cellBounds;
        currentMoveCommand = MoveCommand.Stay;
        for(int i = 0; i< inputBtns.Length;i++)
        {
            int temp = i;
            inputBtns[i].onClick.AddListener(delegate
            {
                GetInput(temp + 1);
            });

        }
    }
    private void Update()
    {
  /*      GetInput();*/
        
    }
    IEnumerator PlayerMovement()
    {
        foreach(MoveCommand command in moveLists)
        {
            currentCell = cellMap.WorldToCell(transform.position);
            targetPos = transform.position + Values[command];
            if (cellMap.HasTile(cellMap.WorldToCell(targetPos)))
            {
                if(!isFlag.IsFlags(cellMap.WorldToCell(targetPos)))
                {
                    GameManager.Instance.ShowDebug("It is not Interactable");
                    GameManager.Instance.roundScore--;
                    yield return new WaitForSeconds(1.5f);
                    GameManager.Instance.HideDebug();
                   
                }
                transform.position = targetPos;
                isFlag.SetCellFlag(cellMap.WorldToCell(currentCell));
                GameManager.Instance.roundScore++;
                yield return new WaitForSeconds(1.0f);
            }
            else
            {
                GameManager.Instance.roundScore--;
                GameManager.Instance.ShowDebug("It is not Interactable");
                yield return new WaitForSeconds(1.5f);
                GameManager.Instance.HideDebug();
            }
        }
        moveLists.Clear();
        yield return null;
     
    }
    void AddMoveCommand(MoveCommand command)
    {
        if (moveLists.Count == 3 && command == (MoveCommand)7)
        {
            GameManager.Instance.gameRound++;
            StartCoroutine(PlayerMovement());
            return;
        }
        else if (moveLists.Count == 3)
        {
            return;
        }
        moveLists.Add(command);
    }
    void GetInput(int index)
    {
        currentMoveCommand = (MoveCommand)index;
        AddMoveCommand(currentMoveCommand);
    }
}
