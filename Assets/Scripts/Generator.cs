using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public float speed;
    public float speed_max;
    public int tweetperperson;
   // public AnimationCurve curve;
    public List<Vector2> pos;
    public GameObject prefabTwitte;

    public void Update()
    {
        StartCoroutine(Wait(speed));
        if (speed>speed_max)
        {
            speed = speed_max;
        }
    }
    public IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        Generated(prefabTwitte);

    }
 public void Generated(GameObject obj)
    {
        for (int i = 0; i <tweetperperson ; i++)
        {
            int ran = Random.Range(0, pos.Count);
            var tmp_obj = new GameObject("Tweet " + i);
            tmp_obj = obj;
            tmp_obj.transform.position = pos[ran];
        }
    }
}