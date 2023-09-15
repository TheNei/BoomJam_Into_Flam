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

    [Header("Button")]
    public Button[] inputBtns = new Button[7];
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
    void PlayerMovement()
    {
        foreach(MoveCommand command in moveLists)
        {
            currentCell = cellMap.WorldToCell(transform.position);
            targetPos = transform.position + Values[command];
            if (cellMap.HasTile(cellMap.WorldToCell(targetPos)))
            {
                transform.position = targetPos;
                currentMoveCommand = MoveCommand.Stay;
            }
            else
            {
                Debug.Log("tile is not Exits");
            }
        }
        moveLists.Clear();
     
    }
    void AddMoveCommand(MoveCommand command)
    {
        if (moveLists.Count == 3 && command == (MoveCommand)7)
        {
            PlayerMovement();
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
        Debug.Log(currentMoveCommand);
    }
}
