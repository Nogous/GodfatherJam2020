using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalityLifeScript : MonoBehaviour
{
    public StatuDePersonality statu = StatuDePersonality.Content;

    private SpriteRenderer persoSprite;

    public Sprite happyPers;
    public Sprite mitigatePers;
    public Sprite angerPers;

    public int currentMood;
    public int maxMood = 50;

    public int currentEgo;
    public int maxEgo = 50;

    void Awake()
    {
        currentMood = maxMood;
        currentEgo = maxEgo;

        if (persoSprite == null)
        {
            persoSprite = GetComponent<SpriteRenderer>();
        }
    }

    private void Start()
    {
        CheckMood();
    }

    public void MoodValue(int i)
    {
        currentMood -= i;
        CheckMood();
    }

    void CheckMood()
    {
        if (currentMood <= 50 && currentMood > 45)
        {
            statu = StatuDePersonality.Content;
        }
        else if (currentMood <= 45 && currentMood > 23)
        {
            statu = StatuDePersonality.Mitigate;
        }
        else if (currentMood <= 23 && currentMood >=0)
        {
            statu = StatuDePersonality.Anger;
        }

        switch (statu)
        {
            case StatuDePersonality.Content:
                persoSprite.sprite = happyPers;
                persoSprite.color = new Color(persoSprite.color.r, persoSprite.color.g, persoSprite.color.b, 1f);
                break;

            case StatuDePersonality.Mitigate:
                persoSprite.sprite = mitigatePers;
                persoSprite.color = new Color(255, 165, 0, 1f);
                break;

            case StatuDePersonality.Anger:
                persoSprite.sprite = angerPers;
                persoSprite.color = new Color(255, 0, 0, 1f);
                break;
            default:
                break;

        }

        if (currentEgo <= 100 && currentEgo > 77)
        {
            statu = StatuDePersonality.Exited;
        }
        else if (currentEgo <= 77 && currentEgo > 55)
        {
            statu = StatuDePersonality.Happy;
        }
        else if (currentEgo <= 55 && currentEgo >= 50)
        {
            statu = StatuDePersonality.Content;
        }

    }
}
