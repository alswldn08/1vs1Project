using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private BossHP bossHP;
    public GameObject potal;

    [SerializeField]
    private float moveSpeed = 10f;
    private Rigidbody2D rigid2D;
    
    public float MoveSpeed => moveSpeed;

    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        bossHP = FindObjectOfType<BossHP>();
    }

    private void Update()
    {
    
    }

    public void MoveTo(Vector3 direction)
    {
        rigid2D.velocity = direction * moveSpeed;
    }

    public void StartBossAttack()
    {
        bossHP.bossUI.SetActive(true);
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        yield return null;
    }
}
