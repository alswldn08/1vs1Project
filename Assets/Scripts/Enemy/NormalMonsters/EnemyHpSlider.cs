using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHpSlider : MonoBehaviour
{
    private EnemyMovement enemyMovement;

    public Slider enemySlider;

    #region 프로퍼티
    private float hp        => enemyMovement.Hp;
    private float maxHp     => enemyMovement.MaxHp;
    #endregion


    private void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();

        enemySlider.maxValue = maxHp;
        enemySlider.value = hp;
        enemySlider.interactable = false;
    }

    private void Update()
    {
        enemySlider.value = hp;
    }
}
