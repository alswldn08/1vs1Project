using UnityEngine;
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
        SoundManager.i.PlayBGM(0);
    }
    public void MoveScene1()
    {
        SceneManager.LoadScene("Stage1");
        SoundManager.i.PlayBGM(2);
    }
    public void MoveScene2()
    {
        SceneManager.LoadScene("Stage2");
        SoundManager.i.PlayBGM(3);
    }
    public void MoveScene3()
    {
        SceneManager.LoadScene("Stage3");
        SoundManager.i.PlayBGM(4);
    }
    public void MoveScene4()
    {
        SceneManager.LoadScene("EndingScene");
        SoundManager.i.PlayBGM(6);
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
