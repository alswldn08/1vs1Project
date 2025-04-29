using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(SceneManager.GetActiveScene().name == "Stage1")
            {
                MoveScene2();
            }
            else if (SceneManager.GetActiveScene().name == "Stage2")
            {
                MoveScene3();
            }
            else
            {
                SceneManager.LoadScene("Stage1");
            }
        }
    }
}
