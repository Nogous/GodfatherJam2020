using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    public Transform target;
    public float speed;
    public float offsetKill = 1f;

    public int hp = 10;
    public bool isMoving = true;

    private void Update()
    {
        if (isMoving)
        {
            transform.position += (target.position - transform.position).normalized * speed * Time.deltaTime;
        }

        if (Vector2.Distance(transform.position, target.position) < offsetKill)
        {
            Destroy();
        }
    }

    public void Hit()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            speed = speed / 2;
            //isMoving = false;
            StartCoroutine("stopForSecond");
        }

        hp--;
        if (hp <= 0)
        {
            //Destroy();
        }
    }

    private IEnumerator stopForSecond()
    {
        yield return new WaitForSeconds(3f);
        isMoving = true;
        speed = speed * 2;
    }

    public void RT()
    {

    }

    public void Like()
    {

    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
