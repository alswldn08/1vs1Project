using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoveSceneManager : MonoBehaviour
{
    public static MoveSceneManager i {  get; private set; }

    private void Awake()
    {
        i = this;
    }

    public void MoveScene1()
    {
        SceneManager.LoadScene("Stage1");
    }
    public void MoveScene2()
    {
        SceneManager.LoadScene("Stage2");
    }
    public void MoveScene3()
    {
        SceneManager.LoadScene("Stage3");
    }
}
