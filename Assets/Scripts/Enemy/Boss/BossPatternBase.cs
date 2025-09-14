using UnityEngine;

public abstract class BossPatternBase : MonoBehaviour
{
    [SerializeField]
    private GameObject hitEffect;
    public BossMovement bossMovement;

    public virtual void Setup(Transform target, float damage, int maxCount = 1, int index = 0)
    {
        bossMovement = FindObjectOfType<BossMovement>();
    }

    private void Update()
    {
        Process();
    }

    public abstract void Process();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Obstacle"))
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
