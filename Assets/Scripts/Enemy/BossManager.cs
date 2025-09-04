using System.Collections;
using System.Diagnostics;
using UnityEngine;

public enum ProjectileType
{
    Straight, 
    Homing, 
    QuadraticHoming, 
    CubicHoming
}

public class BossManager : MonoBehaviour
{
    private BossHP          bossHP;
    public GameObject       potal;

    [SerializeField]
    private GameObject      projectilePrefab;
    [SerializeField]
    private GameObject      lerpPrefab;
    [SerializeField]
    private ProjectileType  projectileType;
    [SerializeField]
    private Transform       owner;
    [SerializeField]
    private Transform       target;

    private Vector3         start, end;
    private float           t;

    private void Start()
    {
        bossHP = FindObjectOfType<BossHP>();
    }

    private void Update()
    {
        if (bossHP.hpSlider.gameObject.activeSelf && bossHP.hpSlider.value <= 0)
        {
            StopAllCoroutines(); // 보스가 죽으면 공격 중지
            potal.SetActive(true);
        }
        if (int.TryParse(Input.inputString, out int index) && (index > 0 && index < 5))
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }

            projectileType = (ProjectileType)index - 1;
            t = 0.0f;
            start = owner.position;
            end = target.position;
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            Process();
            t += 0.1f;
        }
    }

    private void Process()
    {
        if(t > 1.1f) return;

        switch (projectileType)
        {
            case ProjectileType.Straight:
                OnStraight();
                break;
            case ProjectileType.Homing:
                OnHoming(); 
                break;
            case ProjectileType.QuadraticHoming:
                OnQuadraticHoming();
                break;
            case ProjectileType.CubicHoming:
                OnCubicHoming();
                break;

        }
    }

    private void OnStraight()
    {
        Vector3 position = Lerp(start, end, t);
        Instantiate(projectilePrefab, position, Quaternion.identity, transform);

    }

    private void OnHoming()
    {
        end = target.position;

        Vector3 position = Lerp(start, end, t);
        Instantiate(projectilePrefab, position, Quaternion.identity, transform);
    }

    private void OnQuadraticHoming()
    {
        end = target.position;

        Vector3 point = new Vector3(-4f, -5f, 0f);

        Vector3 p1 = Lerp(start, point, t);
        Instantiate(lerpPrefab, p1, Quaternion.identity, transform).GetComponent<SpriteRenderer>().color = Color.red;

        Vector3 p2 = Lerp(point, end, t);
        Instantiate(lerpPrefab, p2, Quaternion.identity, transform).GetComponent<SpriteRenderer>().color = Color.yellow;

        Vector3 position = Lerp(p1, p2, t);
        Instantiate(projectilePrefab, position, Quaternion.identity, transform);
    }

    private void OnCubicHoming()
    {
        end = target.position;

        Vector3 point1 = new Vector3(-4f, 5f, 0f);
        Vector3 point2 = new Vector3(4f, -5f, 0f);

        Vector3 p1 = Lerp(start, point1, t);
        Instantiate(lerpPrefab, p1, Quaternion.identity, transform).GetComponent<SpriteRenderer>().color = Color.red;

        Vector3 p2 = Lerp(point1, point2, t);
        Instantiate(lerpPrefab, p2, Quaternion.identity, transform).GetComponent<SpriteRenderer>().color = Color.yellow;

        Vector3 p3 = Lerp(point1, end, t);
        Instantiate(lerpPrefab, p3, Quaternion.identity, transform).GetComponent<SpriteRenderer>().color = Color.green;
        
        Vector3 p12 = Lerp(p1, p2, t);
        Instantiate(lerpPrefab, p12, Quaternion.identity, transform).GetComponent<SpriteRenderer>().color = Color.blue;
        
        Vector3 p23 = Lerp(p2, p3, t);
        Instantiate(lerpPrefab, p23, Quaternion.identity, transform).GetComponent<SpriteRenderer>().color = Color.magenta;
        
        Vector3 position = Lerp(p12, p23, t);
        Instantiate(projectilePrefab, position, Quaternion.identity, transform);
    }

    private Vector2 Lerp(Vector2 a, Vector2 b, float t)
    {
        return a + (b - a) * t;
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
