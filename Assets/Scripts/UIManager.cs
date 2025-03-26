using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private BulletComponent bulletComponent;
    public Text countBullet;

    void Start()
    {
        bulletComponent = GetComponent<BulletComponent>();
        countBullet = gameObject.GetComponent<Text>();

    }

    void Update()
    {
        countBullet.text = "남은 총알" + bulletComponent.currentBullet;
    }
}
