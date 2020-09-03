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
    public float inutileTwitteSpawnRate = 100;
    [Range(0f, 100f)]
    public float critNegativeTwitteSpawnRate = 100;
    [Range(0f, 100f)]
    public float critPositiveTwitteSpawnRate = 100;
    [Range(0f, 100f)]
    public float insulteTwitteSpawnRate = 100;
    [Range(0f, 100f)]
    public float complimentTwitteSpawnRate = 100;

    [Header("Options")]
    public bool useLikeCountdawn = true;
    public bool useRTCountdawn = true;

    [Header("PersonalityTwitte")]
    public List<TwitteData> persoTwittes;
    public GameObject perso1Obj;

    private List<string> blacklistName = new List<string>();

    private void Start()
    {
        GetGoogleShit.Instance.LoadSheet();
    }

    private void Update()
    {


        if (canSpawnTwitte)
            UpdateGame();
    }

    public void StartRound(List<TwitteData> twitteDatas)
    {
        persoTwittes = twitteDatas;

        canSpawnTwitte = true;
        currentLikeCount = 0;
        currentTimeGame = gameDuration;
        currentSpawnCountdawn = 0;

        blacklistName.Clear();
    }

    private void UpdateGame()
    {
        if (currentLikeCount > 0)
        {
            currentLikeCount -= Time.deltaTime;
        }
        if (currentRTCount > 0)
        {
            currentLikeCount -= Time.deltaTime;
        }

        currentTimeGame -= Time.deltaTime;
        if (currentTimeGame <= 0)
        {
            Debug.Log("End party");
        }

        if (currentSpawnCountdawn > 0)
        {
            currentSpawnCountdawn -= Time.deltaTime;
        }
        else
        {
            Spawner();
            currentSpawnCountdawn = SpawnRateTwitte;
        }
    }

    public void AddBan(string _name)
    {
        blacklistName.Add(_name);
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


    private bool canSpawnTwitte = false;
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

        int twiteNumber = Random.Range(0, persoTwittes.Count);

        for (int i = blacklistName.Count; i-->0;)
        {
            if (persoTwittes[twiteNumber].pseudo == blacklistName[i]) return;
        }

        //Debug.Log(persoTwittes[twiteNumber].pseudo);
        ObjectPooler.Instance.SpawnFromPool("Twitte", (perso1Obj.transform.position + tmpPos), Quaternion.identity, perso1Obj.GetComponent<PersonalityLife>(), persoTwittes[twiteNumber].pseudo, persoTwittes[twiteNumber].corp, persoTwittes[twiteNumber].impactEgo, persoTwittes[twiteNumber].impactHappy);
        
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
            case TwitteType.CritNegative:
                critNegativeTwitteSpawnRate *= _multiplicator;
                break;
            case TwitteType.CritPositive:
                critPositiveTwitteSpawnRate *= _multiplicator;
                break;
            case TwitteType.Insulte:
                insulteTwitteSpawnRate *= _multiplicator;
                break;
            case TwitteType.Compliment:
                complimentTwitteSpawnRate *= _multiplicator;
                break;
            case TwitteType.Inutile:
                inutileTwitteSpawnRate *= _multiplicator;
                break;
        }
        StartCoroutine(RemoveSpawnMultiplicator(_type, _multiplicator, _duration));
    }

    private IEnumerator RemoveSpawnMultiplicator(TwitteType _type, float _multiplicator, float _duration)
    {
        yield return new WaitForSeconds(_duration);

        switch (_type)
        {
            case TwitteType.CritNegative:
                critNegativeTwitteSpawnRate /= _multiplicator;
                break;
            case TwitteType.CritPositive:
                critPositiveTwitteSpawnRate /= _multiplicator;
                break;
            case TwitteType.Insulte:
                insulteTwitteSpawnRate /= _multiplicator;
                break;
            case TwitteType.Compliment:
                complimentTwitteSpawnRate /= _multiplicator;
                break;
            case TwitteType.Inutile:
                inutileTwitteSpawnRate /= _multiplicator;
                break;
        }
    }
    #endregion
}
