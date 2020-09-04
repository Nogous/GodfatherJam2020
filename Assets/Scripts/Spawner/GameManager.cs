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

    [Header("Counrdawn")]
    public float likeCountdawn = 1f;
    public float reTwitteCountdawn = 1f;
    public float banTwitteCountdawn = 1f;

    [HideInInspector]public float currentLikeCount = 0;
    [HideInInspector] public float currentRTCount = 0;
    [HideInInspector] public float currentBanCount = 0;

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

    public GameObject colereGameOver;
    public GameObject depresGameOver;
    public GameObject eggoGameOver;
    public GameObject exiteGameOver;
    public GameObject noFanGameOver;
    public GameObject screenGameOver;

    private void Start()
    {
        screenGameOver.SetActive(false);
    }

    private void Update()
    {
        if (canSpawnTwitte)
        {
            UpdateGame();
        }
    }

    public void GameOver(string i)
    {
        screenGameOver.SetActive(true);
        switch (i)
        {
            case "colereGameOver":
                colereGameOver.SetActive(true);
                break;
            case "depresGameOver":
                depresGameOver.SetActive(true);
                break;
            case "eggoGameOver":
                eggoGameOver.SetActive(true);
                break;
            case "exiteGameOver":
                exiteGameOver.SetActive(true);
                break;
            default:
                noFanGameOver.SetActive(true);
                break;
        }
        canSpawnTwitte = false;
        StopAllCoroutines();
    }

    public void StartRound(List<TwitteData> twitteDatas)
    {
        persoTwittes = twitteDatas;

        for (int i = twitteDatas.Count;i-->0;)
        {
            switch (twitteDatas[i].twitteType)
            {
                case TwitteType.CritNegative:
                    lRTCritNegative.Add(twitteDatas[i]);
                    break;
                case TwitteType.CritPositive:
                    lRTCritPositive.Add(twitteDatas[i]);
                    break;
                case TwitteType.Insulte:
                    lRTInsulte.Add(twitteDatas[i]);
                    break;
                case TwitteType.Compliment:
                    lRTCompliment.Add(twitteDatas[i]);
                    break;
                case TwitteType.Inutile:
                    lRTInutile.Add(twitteDatas[i]);
                    break;
            }
        }

        canSpawnTwitte = true;
        currentLikeCount = 0;
        currentSpawnCountdawn = 0;

        blacklistName.Clear();
    }

    public bool isRTCritNegative;
    private List<TwitteData> lRTCritNegative = new List<TwitteData>();
    public bool isRTCritPositive;
    private List<TwitteData> lRTCritPositive = new List<TwitteData>();
    public bool isRTInsulte;
    private List<TwitteData> lRTInsulte = new List<TwitteData>();
    public bool isRTCompliment;
    private List<TwitteData> lRTCompliment = new List<TwitteData>();
    public bool isRTInutile;
    private List<TwitteData> lRTInutile = new List<TwitteData>();

    public IEnumerator BoolBackTo(TwitteType type)
    {
        yield return new WaitForSeconds(rTDuration);

        switch (type)
        {
            case TwitteType.CritNegative:
                isRTCritNegative = false;
                break;
            case TwitteType.CritPositive:
                isRTCritPositive = false;
                break;
            case TwitteType.Insulte:
                isRTInsulte = false;
                break;
            case TwitteType.Compliment:
                isRTCompliment = false;
                break;
            case TwitteType.Inutile:
                isRTInutile = false;
                break;
        }
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

        if (currentSpawnCountdawn > 0)
        {
            currentSpawnCountdawn -= Time.deltaTime;
        }
        else
        {
            Spawner();
            currentSpawnCountdawn = SpawnRateTwitte;
            if (isRTCritNegative)
            {
                Spawner();
                if (isRTCritNegative)
                    Spawner(TwitteType.CritNegative);
                if (isRTCritPositive)
                    Spawner(TwitteType.CritPositive);
                if (isRTInsulte)
                    Spawner(TwitteType.Insulte);
                if (isRTCompliment)
                    Spawner(TwitteType.Compliment);
                if (isRTInutile)
                    Spawner(TwitteType.Inutile);
            }
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

    private void Spawner(TwitteType type)
    {
        if (!canSpawnTwitte) return;

        if (ObjectPooler.Instance == null)
        {
            Debug.LogError("No ObjectPooler on scene");
            return;
        }

        //random pos
        Vector3 tmpPos = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized * spawnRange;

        while (tmpPos.magnitude < .9f)
        {
            tmpPos = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized * spawnRange;
        }

        List<TwitteData> _persoTwittes;

        switch (type)
        {
            case TwitteType.CritNegative:
                _persoTwittes = lRTCritNegative;
                break;
            case TwitteType.CritPositive:
                _persoTwittes = lRTCritPositive;
                break;
            case TwitteType.Insulte:
                _persoTwittes = lRTInsulte;
                break;
            case TwitteType.Compliment:
                _persoTwittes = lRTCompliment;
                break;
            case TwitteType.Inutile:
                _persoTwittes = lRTInutile;
                break;
            default:
                return;
        }

        int twiteNumber = Random.Range(0, _persoTwittes.Count);
        Debug.Log(twiteNumber);

        for (int i = blacklistName.Count; i-- > 0;)
        {
            if (_persoTwittes[twiteNumber].pseudo == blacklistName[i]) return;
        }


        //Debug.Log(persoTwittes[twiteNumber].pseudo);
        ObjectPooler.Instance.SpawnFromPool("Twitte", (perso1Obj.transform.position + tmpPos), Quaternion.identity, perso1Obj.GetComponent<PersonalityLife>(), _persoTwittes[twiteNumber].pseudo, persoTwittes[twiteNumber].corp, _persoTwittes[twiteNumber].impactEgo, _persoTwittes[twiteNumber].impactHappy);

    }

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
