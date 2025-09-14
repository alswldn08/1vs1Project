using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public void ReTryBtn()
    {
        MoveSceneManager.i.MoveScene1();
    }
    public void ExitGameBtn()
    {
        MoveSceneManager.i.MoveTitle();
    }
}
