using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalityLifeScript : MonoBehaviour
{
    public StatuDePersonality statu = StatuDePersonality.Content;

    private SpriteRenderer persoSprite;

    public Sprite HappyPers;
    public Sprite MitigatePers;
    public Sprite AngerPers;

    public int currentMood;
    public int maxMood = 30;

    void Start()
    {
        currentMood = maxMood;
        persoSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        CheckMood();

        switch (statu)
        {
            case StatuDePersonality.Content:
                this.gameObject.GetComponent<SpriteRenderer>().sprite = HappyPers;
                persoSprite.color = new Color(persoSprite.color.r, persoSprite.color.g, persoSprite.color.b, 1f);
                break;

            case StatuDePersonality.Mitigate:
                this.gameObject.GetComponent<SpriteRenderer>().sprite = MitigatePers;
                persoSprite.color = new Color(255, 165, 0, 1f);
                break;

            case StatuDePersonality.Anger:
                this.gameObject.GetComponent<SpriteRenderer>().sprite = AngerPers;
                persoSprite.color = new Color(255, 0, 0, 1f);
                break;

        }
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
    }
}
