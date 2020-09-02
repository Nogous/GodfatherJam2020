using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwitteSpawner : MonoBehaviour
{
    ObjectPooler objectPooler;

    private void Start()
    {
        if (ObjectPooler.Instance != null)
        {
            objectPooler = ObjectPooler.Instance;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        objectPooler.SpawnFromPool("Twitte", transform.position, Quaternion.identity);
    }
}
