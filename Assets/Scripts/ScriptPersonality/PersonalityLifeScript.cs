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
    public int maxMood = 30;
    public int damage = 1;

    void Awake()
    {
        currentMood = maxMood;

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
        if (currentMood <= 30 && currentMood > 20)
        {
            statu = StatuDePersonality.Content;
        }
        else if (currentMood <= 20 && currentMood > 10)
        {
            statu = StatuDePersonality.Mitigate;
        }
        else if (currentMood <= 10 && currentMood >=0)
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

    }
}
