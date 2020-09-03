using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PersonalityLife))]
public class PersonalityLifeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();


        PersonalityLife persona = (PersonalityLife)target;

        if (persona.twitteTypeFactor == null)
        {
            persona.twitteTypeFactor = new int[TwitteType.Inutile.GetHashCode()+1];
            for (int i = persona.twitteTypeFactor.Length; i-->0;)
            {
                persona.twitteTypeFactor[i] = 1;
            }
        }
        else if (persona.twitteTypeFactor.Length != TwitteType.Inutile.GetHashCode()+1)
        {
            persona.twitteTypeFactor = new int[TwitteType.Inutile.GetHashCode()+1];
            for (int i = persona.twitteTypeFactor.Length; i-- > 0;)
            {
                persona.twitteTypeFactor[i] = 1;
            }
        }
    }
}
