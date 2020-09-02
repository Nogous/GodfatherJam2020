using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoodBarScript : MonoBehaviour
{
    private PersonalityLifeScript moodPers;
    public Slider moodBar;

    // Start is called before the first frame update
    void Start()
    {
        moodPers = FindObjectOfType<PersonalityLifeScript>();
    }

    // Update is called once per frame
    void Update()
    {
        moodBar.maxValue = moodPers.maxMood;
        moodBar.value = moodPers.currentMood;
    }
}
