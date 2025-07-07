using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public Button reTry;
    public Button exitGame;
    // Start is called before the first frame update
    void Start()
    {
        reTry.onClick.AddListener(ReTryBtn);
        exitGame.onClick.AddListener(ExitGameBtn);
    }

    public void ReTryBtn()
    {
        MoveSceneManager.i.MoveScene1();
    }
    public void ExitGameBtn()
    {
        MoveSceneManager.i.MoveTitle();
    }
}
