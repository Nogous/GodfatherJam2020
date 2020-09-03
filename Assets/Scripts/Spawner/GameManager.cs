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
    [Range(0f, 100f)]
    public float normalTwitteSpawnRate = 100;
    public float insultesTwitteSpawnRate = 100;
    public float complimentsTwitteSpawnRate = 100;
    public float critiquesTwitteSpawnRate = 100;
    public float nonSenseTwitteSpawnRate = 100;

    [Header("Options")]
    public bool useLikeCountdawn = true;
    public bool useRTCountdawn = true;

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
}
