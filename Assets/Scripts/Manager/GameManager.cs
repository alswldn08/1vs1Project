using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager i { get; private set; }

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

    public void GameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    public void GameClear()
    {
        SceneManager.LoadScene("GameCreditScene");
    }
}
