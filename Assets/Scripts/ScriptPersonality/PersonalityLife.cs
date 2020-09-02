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

    [Header("DataPoints")]
    public int factorHappinesse = 1;
    public int factorEgo = 1;
    public float likeFactor = 2;

    public int[] twitteTypeFactor;

    void Awake()
    {
        happinesse = startMood;
        ego = startEgo;

        if (image == null)
        {
            image = GetComponent<Image>();
        }
    }

    private void Start()
    {
        UpdateFace();
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

        if (popularity > 100) popularity = 100;

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

        /*
        if (happinesse <= 50 && happinesse > 45)
        {
            statu = StatuDePersonality.Content;
        }
        else if (happinesse <= 45 && happinesse > 23)
        {
            statu = StatuDePersonality.Mitigate;
        }
        else if (happinesse <= 23 && happinesse >=0)
        {
            statu = StatuDePersonality.Anger;
        }

        switch (statu)
        {
            case StatuDePersonality.Content:
                image.sprite = happyPers;
                break;

            case StatuDePersonality.Mitigate:
                image.sprite = mitigatePers;
                break;

            case StatuDePersonality.Anger:
                image.sprite = angerPers;
                break;
            default:
                break;

        }

        if (ego <= 100 && ego > 77)
        {
            statu = StatuDePersonality.Exited;
        }
        else if (ego <= 77 && ego > 55)
        {
            statu = StatuDePersonality.Happy;
        }
        else if (ego <= 55 && ego >= 50)
        {
            statu = StatuDePersonality.Content;
        }
        */
    }
}
