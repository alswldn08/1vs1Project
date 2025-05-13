using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomEnemy : MonoBehaviour
{
    private Rigidbody2D rigid;
    public float speed = 3f;
    public float EnemyMove = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MushroomMove());
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        

        rigid.velocity = new Vector2(EnemyMove * speed, 0);
    }

    IEnumerator MushroomMove()
    {
        while (true)
        {
            EnemyMove *= -1;

            yield return new WaitForSeconds(3f);
        }


    }
}
