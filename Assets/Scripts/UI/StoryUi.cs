using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryUi : MonoBehaviour
{
    public Image[] storyScript;
    public GameObject GameUI;
    public bool isStory = true;
    public int index = 0;

    void Start()
    {
        storyScript[0].gameObject.SetActive(true);
        storyScript[1].gameObject.SetActive(true);
        storyScript[2].gameObject.SetActive(true);
        storyScript[3].gameObject.SetActive(true);
        storyScript[4].gameObject.SetActive(true);
        storyScript[5].gameObject.SetActive(true);
        storyScript[6].gameObject.SetActive(true);
        GameUI.gameObject.SetActive(false);
    }

    void Update()
    {
        NextPage();
    }

    public void NextPage()
    {
        while (isStory)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                storyScript[index].gameObject.SetActive(false);
                index += 1;
            }
            if(index > storyScript.Length)
            {
                StopStoryPage();
            }
        }

    }

    public void StopStoryPage()
    {
        isStory = false;
        storyScript[0].gameObject.SetActive(false);
        storyScript[1].gameObject.SetActive(false);
        storyScript[2].gameObject.SetActive(false);
        storyScript[3].gameObject.SetActive(false);
        storyScript[4].gameObject.SetActive(false);
        storyScript[5].gameObject.SetActive(false);
        storyScript[6].gameObject.SetActive(false);
        GameUI.gameObject.SetActive(true);
    }
}
