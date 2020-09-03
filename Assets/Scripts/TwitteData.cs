using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TwitteData
{
    public TwitteType twitteType;

    public string pseudo;
    public string corp;
    public int impactEgo;
    public int impactHappy;

    public TwitteData(string _pseudo, string _corp, TwitteType _type, int _impactEgo, int _impactHappy)
    {
        pseudo = _pseudo;
        corp = _corp;
        twitteType = _type;
        impactEgo = _impactEgo;
        impactHappy = _impactHappy;
    }
}
