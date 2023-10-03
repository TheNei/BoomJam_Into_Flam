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
        line.SetPosition(0, prePosition);
    }
   public void SetNewPosition(Vector3 currentPos)
    {
        currentPos.z = 0;
        if (currentPos == Vector3.zero)
            return;
        if (currentPos != prePosition)
        {
            line.positionCount++;
            line.SetPosition(line.positionCount- 1, prePosition+currentPos);
            prePosition = prePosition+currentPos;
        }
        else
        {
            return;
        }

    }
        

}
