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
    public GodWeapons weapon;
   /* private float cellsize = 2.0f;*/
    private Vector3Int currentCell;
    private Vector3 targetPos;
  /*  private MoveCommand currentMoveCommand;*/
/*    private List<CommandManager.MoveCommand> moveLists = new List<CommandManager.MoveCommand>();*/
    private TileFlag isFlag;
    public float speed = 1.0f;
    public float delay = 0.8f;
    private int flyIndex = 3;
    /*    public Button[] inputBtns = new Button[7];*/
    private void Awake()
    {
        isFlag = GameObject.FindObjectOfType<TileFlag>();
    }
    private void Start()
    {
        flyIndex = 3;
        weapon = GameObject.FindAnyObjectByType<GodWeapons>();
        cellMap = GameObject.FindAnyObjectByType<Tilemap>();
      
    }
    private void Update()
    {
  /*      GetInput();*/
        
    }
    public IEnumerator PlayerMovement(Vector3 target)
    {
       if(GameManager.Instance.IsWin)
        {
            StopAllCoroutines();
        }
        currentCell = cellMap.WorldToCell(transform.position);
        targetPos = transform.position +target ;
        if (cellMap.HasTile(cellMap.WorldToCell(targetPos)))
        {
           
            if (isFlag.IsFlags(cellMap.WorldToCell(targetPos)))
                {

                GameManager.Instance.ShowDebug("踩到黑格子扣1分");
                if (weapon.isFly && flyIndex>0)
                {
                    print("Score do nothing");
                    flyIndex--;
                }
                else
                {
                    if(!isFlag.IsFlags(currentCell))
                    GameManager.Instance.roundScore--;
                    isFlag.SetCellFlag(currentCell);
                }
                if (flyIndex == 0)
                {
                    weapon.isFly = false;
                    flyIndex = 3;
                }
                float startTime = Time.time;
                float journeyLength = Vector3.Distance(transform.position, targetPos);
                AuidoManager.Instance.PlayPlayerMove();
                while (transform.position != targetPos)
                {
                    float distCovered = (Time.time - startTime) * speed;
                    float fractionOfJourney = distCovered / journeyLength;
                    transform.position = Vector3.Lerp(transform.position, targetPos, fractionOfJourney);
                    yield return null;
                }
                GameManager.Instance.IFWin();
                yield return new WaitForSeconds(delay);
                    GameManager.Instance.HideDebug();
               

            }
            else
            {

                
                if (weapon.isFly && flyIndex > 0)
                {
                    print("Score do nothing");
                    flyIndex--;
                }
                else
                {
                    isFlag.SetCellFlag(currentCell);
                    GameManager.Instance.roundScore++;
                }
                if (flyIndex == 0)
                {
                    weapon.isFly = false;
                    flyIndex = 3;
                    
                }
                float startTime = Time.time;
                float journeyLength = Vector3.Distance(transform.position, targetPos);
                AuidoManager.Instance.PlayPlayerMove();
                while (transform.position != targetPos)
                {
                    float distCovered = (Time.time - startTime) * speed;
                    float fractionOfJourney = distCovered / journeyLength;
                    transform.position = Vector3.Lerp(transform.position, targetPos, fractionOfJourney);
                    yield return null;
                }

                GameManager.Instance.IFWin();
                yield return new WaitForSeconds(delay);
             
            }

        }
         else{
                GameManager.Instance.roundScore--;
                GameManager.Instance.ShowDebug("越界扣1分");
                yield return new WaitForSeconds(0.5f);
                GameManager.Instance.HideDebug();
            yield return null;
        }
    }
    public IEnumerator PlayerJumpMovement(Vector3 target)
    {

        currentCell = cellMap.WorldToCell(transform.position);
        targetPos = transform.position + target;
        if (cellMap.HasTile(cellMap.WorldToCell(targetPos)))
        {

            if (isFlag.IsFlags(cellMap.WorldToCell(targetPos)))
            {
               
                
                GameManager.Instance.ShowDebug("踩到黑格子扣1分");
                if (weapon.isFly && flyIndex > 0)
                {
                    print("Score do nothing");
                    flyIndex--;
                }
                else
                {
                    isFlag.SetCellFlag(cellMap.WorldToCell(targetPos), currentCell);
                    isFlag.SetCellFlag(currentCell);
                    GameManager.Instance.roundScore--;
                }
                if (flyIndex == 0)
                {
                    weapon.isFly = false;
                    flyIndex = 3;
                    
                }
                float startTime = Time.time;
                float journeyLength = Vector3.Distance(transform.position, targetPos);
                AuidoManager.Instance.PlayPlayerMove();
                while (transform.position != targetPos)
                {
                    float distCovered = (Time.time - startTime) * speed;
                    float fractionOfJourney = distCovered / journeyLength;
                    transform.position = Vector3.Lerp(transform.position, targetPos, fractionOfJourney);
                    yield return null;
                }
                GameManager.Instance.IFWin();
                yield return new WaitForSeconds(delay);
                GameManager.Instance.HideDebug();


            }
           
            else
            {
            
                if (weapon.isFly && flyIndex > 0)
                {
                    print("Score do nothing");
                    flyIndex--;
                }
                else
                {
                    isFlag.SetCellFlag(cellMap.WorldToCell(targetPos), currentCell);
                    isFlag.SetCellFlag(currentCell);
                    GameManager.Instance.roundScore++;
                }
                if(flyIndex == 0)
                {
                    weapon.isFly = false;
                    flyIndex = 3;
                        
                }
                float startTime = Time.time;
                float journeyLength = Vector3.Distance(transform.position, targetPos);
                AuidoManager.Instance.PlayPlayerMove();
                while (transform.position != targetPos)
                {
                    float distCovered = (Time.time - startTime) * speed;
                    float fractionOfJourney = distCovered / journeyLength;
                    transform.position = Vector3.Lerp(transform.position, targetPos, fractionOfJourney);
                    yield return null;
                }
                GameManager.Instance.IFWin();

                yield return new WaitForSeconds(0.5f);

            }

        }
        else
        {
            GameManager.Instance.roundScore--;
            GameManager.Instance.ShowDebug("越界扣1分");
            yield return new WaitForSeconds(0.5f);
            GameManager.Instance.HideDebug();
        }

        yield return null;

    }
}
