using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Fx_manager : MonoBehaviour
{
    public static Fx_manager instance = null;
    public List<GameObject> fxlist;

public void playfx(int index)
    {
        var obj = new GameObject(fxlist[index].name);
        obj = fxlist[index];
        var fx_clip = obj.GetComponent<ParticleSystem>();
        fx_clip.Play();
        StartCoroutine(Lifeduration(obj));

    }
   public IEnumerator Lifeduration(GameObject obj)
    {
        var fx_clip = obj.GetComponent<ParticleSystem>();
        yield return new WaitForSeconds(fx_clip.duration);
        fx_clip.loop = false;
    }
    public void playfx(string _name)
    {
        for (int i = 0; i < fxlist.Count; i++)
        {
            if (fxlist[i].name == _name)
            {
                playfx(i);
            }
        }
    }
}

