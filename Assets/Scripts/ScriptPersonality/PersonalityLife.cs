using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonalityLife : MonoBehaviour
{
    public StatuDePersonality statu = StatuDePersonality.Content;

    public Image image;

    public Sprite happiPers;
    public Sprite mitigatePers;
    public Sprite angerPers;
    public Sprite maxEgo;
    public Sprite lowEgo;

    public int happinesse;
    private int startMood = 50;

    public int popularity;
    private int startPopularity = 20;

    public int ego;
    private int startEgo = 50;


    public float popularityCountdawnDuration = 5f;
    private float popularityCountdawn = 5f;

    [Header("DataPoints")]
    public int factorHappinesse = 1;
    public int factorEgo = 1;
    public float likeFactor = 2;

    public int[] twitteTypeFactor;

    public Text follower;
    public Slider sliderEgo;
    public Text sliderEgoText;
    public Slider sliderHappy;
    public Text sliderHappyText;

    void Awake()
    {
        happinesse = startMood;
        ego = startEgo;
        popularity = startPopularity;

        if (image == null)
        {
            image = GetComponent<Image>();
        }
    }

    private void Start()
    {
        sliderEgoText.text = "";
        sliderHappyText.text = "";

        UpdateFace();
        ReceivingTwitte(0,0,TwitteType.Normal);

        popularityCountdawn = popularityCountdawnDuration;
    }


    private void Update()
    {
        if (!isIngame) return;
        popularityCountdawn -= Time.deltaTime;
        if (popularityCountdawn <= 0){
            popularity--;
            follower.text = "Followers " + popularity.ToString();
            popularityCountdawn = popularityCountdawnDuration;

            if (popularity <= 0)
            {
                GameOver();
            }
        }
    }

    private bool isIngame = true;

    public void GameOver()
    {
        string gameOverSting = "";
        if (happinesse>=100)
        {
            gameOverSting = "exiteGameOver";
        }
        else if (happinesse<=0)
        {
            gameOverSting = "colereGameOver";
        }
        else if (ego>=100)
        {
            gameOverSting = "eggoGameOver";
        }
        else if(ego<=0)
        {
            gameOverSting = "depresGameOver";
        }

        Debug.Log("GameOver : " + gameOverSting);

        isIngame = false;
        GameManager.Instance.GameOver(gameOverSting);
    }

    public void ReceivingTwitte(int _happinesse, int _ego, TwitteType type, int _popularity, bool isLiked = false)
    {
        
        int i = type.GetHashCode();
        if (isLiked)
        {
            happinesse += (int)(_happinesse * factorHappinesse * twitteTypeFactor[i]* likeFactor);
            ego += (int)(_ego * factorEgo * twitteTypeFactor[i]* likeFactor);
            popularity += (int)(_popularity* likeFactor);
        }
        else
        {
            happinesse += _happinesse * factorHappinesse * twitteTypeFactor[i];
            ego += _ego * factorEgo * twitteTypeFactor[i];
            popularity += _popularity;
        }
        if (happinesse > 100) happinesse = 100;
        if (ego > 100) ego = 100;

        float tmphappy = ((float)happinesse - 100);
        if (tmphappy<0)
        {
            tmphappy = -tmphappy;
        }

        sliderHappy.value = tmphappy / 100f;
        //sliderHappyText.text = happinesse.ToString();

        float tmpego = ((float)ego - 100);
        if (tmpego<0)
        {
            tmpego = -tmpego;
        }

        sliderEgo.value = tmpego / 100f;
        //sliderEgoText.text = ego.ToString();

        follower.text = "Followers " + popularity.ToString();

        switch (type)
        {
            case TwitteType.CritNegative:
                GameManager.Instance.isRTCritNegative = true;
                break;
            case TwitteType.CritPositive:
                GameManager.Instance.isRTCritPositive = true;
                break;
            case TwitteType.Insulte:
                GameManager.Instance.isRTInsulte = true;
                break;
            case TwitteType.Compliment:
                GameManager.Instance.isRTCompliment = true;
                break;
            case TwitteType.Inutile:
                GameManager.Instance.isRTInutile = true;
                break;
        }
        StartCoroutine(GameManager.Instance.BoolBackTo(type));

        UpdateFace();
    }
    public void ReceivingTwitte(int _happinesse, int _ego, TwitteType type)
    {
        ReceivingTwitte(_happinesse, _ego, type,0);
    }

    void UpdateFace()
    {
        int tmpHappy = happinesse - 50;
        if (tmpHappy < 0)
            tmpHappy *= -1;

        int tmpEgo = ego - 50;
        if (tmpEgo < 0)
            tmpEgo *= -1;
        int tmpPopular = popularity - 50;
        if (tmpPopular < 0)
            tmpPopular *= -1;

        if (tmpHappy > tmpEgo )//&& tmpHappy> tmpPopular)
        {
            if (happinesse < 25)
            {
                image.sprite = angerPers;
            }
            else if (happinesse < 75)
            {
                image.sprite = mitigatePers;
            }
            else
            {
                image.sprite = happiPers;
            }
        }/*else if (tmpEgo < tmpPopular)
        {
            if (popularity < 25)
            {
                image.sprite = angerPers;
            }
            else if (popularity < 75)
            {
                image.sprite = mitigatePers;
            }
            else
            {
                image.sprite = happyPers;
            }
        }*/
        else
        {
            if (ego < 25)
            {
                image.sprite = lowEgo;
            }
            else if (ego < 75)
            {
                image.sprite = mitigatePers;
            }
            else
            {
                image.sprite = maxEgo;
            }
        }

        if (happinesse >= 100)
        {
            GameManager.Instance.GameOver("exiteGameOver");
        }
        else if (happinesse <= 0)
        {
            GameManager.Instance.GameOver("colereGameOver");
        }
        else if (ego >= 100)
        {
            GameManager.Instance.GameOver("eggoGameOver");
        }
        else if (ego <= 0)
        {
            GameManager.Instance.GameOver("depresGameOver");
        }
    }
}
