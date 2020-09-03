using UnityEngine;

public interface IPooledObject
{
    void OnObjectSpawn(PersonalityLife _target,string _pseudo, string _content, int _ego, int happynesse);
}
