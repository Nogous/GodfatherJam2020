using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance = null;
    private void Awake()
    {
        if (Instance== null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public float gameDuration = 60f;
    private float currentTimeGame;

    [Header("Counrdawn")]
    public float likeCountdawn = 1f;
    public float reTwitteCountdawn = 1f;

    [HideInInspector]public float currentLikeCount = 0;
    [HideInInspector] public float currentRTCount = 0;

    [Header("")]
    public float likeSpawnMultiplicator = 1;
    public float rTSpawnMultiplicator = 1;
    public float likeDuration = 1;
    public float rTDuration = 1;

    [Header("Twitte type spawn rate")]
    public float SpawnRateTwitte = 10f;
    private float currentSpawnCountdawn = 0f;
    [Range(0f, 100f)]
    public float normalTwitteSpawnRate = 100;
    [Range(0f, 100f)]
    public float insultesTwitteSpawnRate = 100;
    [Range(0f, 100f)]
    public float complimentsTwitteSpawnRate = 100;
    [Range(0f, 100f)]
    public float critiquesTwitteSpawnRate = 100;
    [Range(0f, 100f)]
    public float nonSenseTwitteSpawnRate = 100;

    [Header("Options")]
    public bool useLikeCountdawn = true;
    public bool useRTCountdawn = true;

    [Header("PersonalityTwitte")]
    int twiteNumber = 0;
    public List<TwitteData> perso1Twittes;
    public GameObject perso1Obj;

    private void Start()
    {
        currentTimeGame = gameDuration;
    }

    private void Update()
    {
        if (currentLikeCount>0)
        {
            currentLikeCount -= Time.deltaTime;
        }
        if (currentRTCount>0)
        {
            currentLikeCount -= Time.deltaTime;
        }

        currentTimeGame -= Time.deltaTime;
        if (currentTimeGame<=0)
        {
            Debug.Log("End party");
        }

        if (currentSpawnCountdawn>0)
        {
            currentSpawnCountdawn -= Time.deltaTime;
        }
        else
        {
            Debug.Log("spawnRange");
            currentSpawnCountdawn = SpawnRateTwitte;
            Spawner();
        }
    }

    public void RestartLikeCountdawn()
    {
        if (!useLikeCountdawn) return;
        currentLikeCount = likeCountdawn;
    }
    public void RestartRTCountdawn()
    {
        if (!useRTCountdawn) return;
        currentRTCount = reTwitteCountdawn;
    }


    private bool canSpawnTwitte = true;
    public float spawnRange = 10f;
    private void Spawner()
    {
        if (!canSpawnTwitte) return;

        if (ObjectPooler.Instance==null)
        {
            Debug.LogError("No ObjectPooler on scene");
            return;
        }

        //random pos
        Vector3 tmpPos = new Vector3(Random.Range(-1f,1f), Random.Range(-1f,1f),0).normalized * spawnRange;

        while (tmpPos.magnitude <.9f)
        {
            tmpPos = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized * spawnRange;
        }

        ObjectPooler.Instance.SpawnFromPool("Twitte", (perso1Obj.transform.position + tmpPos), Quaternion.identity, perso1Obj.GetComponent<PersonalityLife>(), perso1Twittes[twiteNumber].pseudo, perso1Twittes[twiteNumber].corp, perso1Twittes[twiteNumber].impactEgo, perso1Twittes[twiteNumber].impactHappy);
        twiteNumber++;
        if (twiteNumber>= perso1Twittes.Count)
        {
            twiteNumber = 0;
            //canSpawnTwitte = false;
        }
    }

    #region multiplicator
    public void SpawnMultiplicator(TwitteType _type)
    {
        SpawnMultiplicator(_type, rTSpawnMultiplicator, rTDuration);
    }

    public void SpawnMultiplicator(TwitteType _type, float _multiplicator, float _duration)
    {
        switch (_type)
        {
            case TwitteType.Normal:
                normalTwitteSpawnRate *= _multiplicator;
                break;
            case TwitteType.Insultes:
                insultesTwitteSpawnRate *= _multiplicator;
                break;
            case TwitteType.Compliments:
                complimentsTwitteSpawnRate *= _multiplicator;
                break;
            case TwitteType.Critiques:
                critiquesTwitteSpawnRate *= _multiplicator;
                break;
            case TwitteType.NonSense:
                nonSenseTwitteSpawnRate *= _multiplicator;
                break;
        }
        StartCoroutine(RemoveSpawnMultiplicator(_type, _multiplicator, _duration));
    }

    private IEnumerator RemoveSpawnMultiplicator(TwitteType _type, float _multiplicator, float _duration)
    {
        yield return new WaitForSeconds(_duration);

        switch (_type)
        {
            case TwitteType.Normal:
                normalTwitteSpawnRate /= _multiplicator;
                break;
            case TwitteType.Insultes:
                insultesTwitteSpawnRate /= _multiplicator;
                break;
            case TwitteType.Compliments:
                complimentsTwitteSpawnRate /= _multiplicator;
                break;
            case TwitteType.Critiques:
                critiquesTwitteSpawnRate /= _multiplicator;
                break;
            case TwitteType.NonSense:
                nonSenseTwitteSpawnRate /= _multiplicator;
                break;
        }
    }
    #endregion
}
