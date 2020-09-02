using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoodBarScript : MonoBehaviour
{
    private PersonalityLifeScript moodPers;
    //public Slider moodBar;
    public Text text;
    public Text egoColere;

    // Start is called before the first frame update
    void Start()
    {
        moodPers = FindObjectOfType<PersonalityLifeScript>();
        //text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //moodBar.maxValue = moodPers.maxMood;
        //moodBar.value = moodPers.currentMood;
        text.text = "Exitation/dépression : " + moodPers.currentMood;

        egoColere.text = "Ego/Colère : " + moodPers.currentEgo;

    }
}
