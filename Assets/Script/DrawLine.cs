using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    private Vector3 prePosition;
    private LineRenderer line;
    private Movement player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        line = GetComponent<LineRenderer>();
        prePosition = player.transform.position;
    }
    public void SetNewPosition(Vector3 currentPos, int index)
    {
            currentPos.z = 0;
            line.positionCount++;
            line.SetPosition(line.positionCount- 1, currentPos);
      
    }
    public Vector3 GetLocalPosition(int index)
    {
        return line.GetPosition(index);
    }
        

}
