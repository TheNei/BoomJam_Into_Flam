using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EnhancedMovement : MonoBehaviour
{
    private static EnhancedMovement instance;
    private EnhancedMovement() { }
    public static EnhancedMovement Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<EnhancedMovement>();
                if (instance == null)
                {
                    instance = new GameObject("EnhancedMovement").AddComponent<EnhancedMovement>();
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
    public enum Enhance
    {
        Anti = 1,
        Force = 2
    }
    public List<GameObject> btns = new List<GameObject>();
    public List<Enhance> enhanceLists = new List<Enhance>();
    private void Start()
    {
        for(int i = 0;i <transform.childCount;i++)
        {
            int temp = i;
            btns.Add(transform.GetChild(temp).gameObject);
        }
        ClearList();
    }
    void ClearList()
    {
        enhanceLists.Clear();
        foreach(var btn in btns)
        {
            btn.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;
        }
    }
    void Excute(List<CommandManager.MoveCommand> list, Dictionary<CommandManager.MoveCommand, Vector3> values,int index)
    {
        if (enhanceLists.Count == 0)
            return;
        
            if (values.ContainsKey(list[index]) && list[index] == CommandManager.MoveCommand.JUMP)
            {
            if(enhanceLists[index] == Enhance.Force)
                values[list[index]] += Vector3.up;
          else  if (enhanceLists[index] == Enhance.Anti)
                values[list[index]] *= 2.0f;
             }
            else
            {
            if (enhanceLists[index] == Enhance.Force)
                values[list[index]] *= 2.0f;
           else if (enhanceLists[index] == Enhance.Anti)
            {
                values[list[index]] *= -1.0f;
            }
            }
        
    }
 /*  public void GetInput(Enhance enhance,int index)
    {
        if(CommandManager.Instance.isExcute)
        {
            print("Excute");
       *//*     Excute(CommandManager.Instance.commandLists, CommandManager.Instance.Values, index);*//*
        }
        enhanceLists.Add(enhance);
    }*/
}
