using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SwitchingScenes : MonoBehaviour
{
    
    public void JumpScene(int number)
    {
        SceneManager.LoadScene(number);
    }
}
