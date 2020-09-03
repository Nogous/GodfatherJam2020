using UnityEngine;

public interface IPooledObject
{
    void OnObjectSpawn(string _pseudo, string _content);
}
