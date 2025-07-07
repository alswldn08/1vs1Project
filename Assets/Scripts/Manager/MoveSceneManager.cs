using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoveSceneManager : MonoBehaviour
{
    public static MoveSceneManager i { get; private set; }

    private void Awake()
    {
        if(i == null)
        {
            i = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    public void MoveTitle()
    {
        SceneManager.LoadScene("Title");
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

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        if (SceneManager.GetActiveScene().name == "Stage1")
    //        {
    //            LoadingUI.i.StartLoading();
    //        }
    //        else if (SceneManager.GetActiveScene().name == "Stage2")
    //        {
    //            LoadingUI.i.StartLoading();
    //        }
    //        else
    //        {
    //            LoadingUI.i.StartLoading();
    //        }
            
    //    }
    //}
}
