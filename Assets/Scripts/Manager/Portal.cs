using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (SceneManager.GetActiveScene().name == "Stage1")
            {
                LoadingUI.i.StartLoading();
            }
            else if (SceneManager.GetActiveScene().name == "Stage2")
            {
                LoadingUI.i.StartLoading();
            }
            else
            {
                LoadingUI.i.StartLoading();
            }

        }
    }
}
