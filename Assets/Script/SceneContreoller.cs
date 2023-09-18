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

    // ���ص�ǰ����������
    public static string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    // ���ص�ǰ�����ı��
    public static int GetSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    // ��ת��ĳ������ͨ������
    public static void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // ��ת��ĳ������ͨ�����
    public static void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
