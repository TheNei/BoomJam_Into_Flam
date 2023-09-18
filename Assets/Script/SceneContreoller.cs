using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneContreoller : MonoBehaviour
{
    public static void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    // 返回当前场景的名字
    public static string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    // 返回当前场景的编号
    public static int GetSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    // 跳转到某个场景通过名字
    public static void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // 跳转到某个场景通过编号
    public static void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
