using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoodBar : MonoBehaviour
{
    private PersonalityLife moodPers;
    //public Slider moodBar;
    public Text text;
    public Text egoColere;

    // Start is called before the first frame update
    void Start()
    {
        moodPers = FindObjectOfType<PersonalityLife>();
        //text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //moodBar.maxValue = moodPers.maxMood;
        //moodBar.value = moodPers.currentMood;
        text.text = "Exitation/dépression : " + moodPers.happinesse;

        egoColere.text = "Ego/Colère : " + moodPers.ego;

    }
}
