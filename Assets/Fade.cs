using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
 
    public float tranlationTime = 1.0f;
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void PlayAnimtion()
    {
        StartCoroutine(loadLevel());
    }
    IEnumerator loadLevel()
    {
        anim.SetTrigger("Start");
        yield return new WaitForSeconds(tranlationTime);
        SceneContreoller.LoadNextScene();
    }
}
