using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
public class Movement : MonoBehaviour
{
  /*  public enum MoveCommand
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
    [SerializeField]
    private Tilemap cellMap;
    private float cellsize = 2.0f;
    };  
    public Tilemap cellMap;
    private float cellsize = 2.0f;
    };  */
    public Tilemap cellMap;
   /* private float cellsize = 2.0f;*/
    private Vector3Int currentCell;
    private Vector3 targetPos;
  /*  private MoveCommand currentMoveCommand;*/
/*    private List<CommandManager.MoveCommand> moveLists = new List<CommandManager.MoveCommand>();*/
    private TileFlag isFlag;
    public float speed = 1.0f;
    public float delay = 0.8f;
    /*    public Button[] inputBtns = new Button[7];*/
    private void Awake()
    {
        isFlag = GameObject.FindObjectOfType<TileFlag>();
    }
    private void Start()
    {
        
        BoundsInt bounds = cellMap.cellBounds;
    }
    private void Update()
    {
  /*      GetInput();*/
        
    }
    public IEnumerator PlayerMovement(Vector3 target)
    {

        currentCell = cellMap.WorldToCell(transform.position);
        targetPos = transform.position +target ;
        if (cellMap.HasTile(cellMap.WorldToCell(targetPos)))
        {

                if (isFlag.IsFlags(cellMap.WorldToCell(targetPos)))
                {
                isFlag.SetCellFlag(currentCell);
                GameManager.Instance.ShowDebug("It is Black");
                    GameManager.Instance.roundScore--;
                float startTime = Time.time;
                float journeyLength = Vector3.Distance(transform.position, targetPos);
                while (transform.position != targetPos)
                {
                    float distCovered = (Time.time - startTime) * speed;
                    float fractionOfJourney = distCovered / journeyLength;
                    transform.position = Vector3.Lerp(transform.position, targetPos, fractionOfJourney);
                    yield return null;
                }
             
                yield return new WaitForSeconds(delay);
                    GameManager.Instance.HideDebug();
               

            }
            else
            {
                isFlag.SetCellFlag(currentCell);
                GameManager.Instance.roundScore++;
                float startTime = Time.time;
                float journeyLength = Vector3.Distance(transform.position, targetPos);
                while (transform.position != targetPos)
                {
                    float distCovered = (Time.time - startTime) * speed;
                    float fractionOfJourney = distCovered / journeyLength;
                    transform.position = Vector3.Lerp(transform.position, targetPos, fractionOfJourney);
                    yield return null;
                }

               
                yield return new WaitForSeconds(delay);
             
            }

        }
         else{
                GameManager.Instance.roundScore--;
                GameManager.Instance.ShowDebug("It is not Interactable");
                yield return new WaitForSeconds(0.5f);
                GameManager.Instance.HideDebug();
            }

        yield return null;
     
    }
    public IEnumerator PlayerJumpMovement(Vector3 target)
    {

        currentCell = cellMap.WorldToCell(transform.position);
        targetPos = transform.position + target;
        if (cellMap.HasTile(cellMap.WorldToCell(targetPos)))
        {

            if (isFlag.IsFlags(cellMap.WorldToCell(targetPos)))
            {
                isFlag.SetCellFlag(cellMap.WorldToCell(targetPos),currentCell);
                isFlag.SetCellFlag(currentCell);
                GameManager.Instance.ShowDebug("It is Black");
                GameManager.Instance.roundScore--;
                float startTime = Time.time;
                float journeyLength = Vector3.Distance(transform.position, targetPos);
                while (transform.position != targetPos)
                {
                    float distCovered = (Time.time - startTime) * speed;
                    float fractionOfJourney = distCovered / journeyLength;
                    transform.position = Vector3.Lerp(transform.position, targetPos, fractionOfJourney);
                    yield return null;
                }
               
                yield return new WaitForSeconds(delay);
                GameManager.Instance.HideDebug();


            }
            else
            {
                isFlag.SetCellFlag(cellMap.WorldToCell(targetPos),currentCell);
                isFlag.SetCellFlag(currentCell);
                GameManager.Instance.roundScore++;
                float startTime = Time.time;
                float journeyLength = Vector3.Distance(transform.position, targetPos);
                while (transform.position != targetPos)
                {
                    float distCovered = (Time.time - startTime) * speed;
                    float fractionOfJourney = distCovered / journeyLength;
                    transform.position = Vector3.Lerp(transform.position, targetPos, fractionOfJourney);
                    yield return null;
                }

                
                yield return new WaitForSeconds(0.5f);

            }

        }
        else
        {
            GameManager.Instance.roundScore--;
            GameManager.Instance.ShowDebug("It is not Interactable");
            yield return new WaitForSeconds(0.5f);
            GameManager.Instance.HideDebug();
        }

        yield return null;

    }
}
