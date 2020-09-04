using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    float currentTime = 0f;
    public int startingTime = 60;

    public Animator transition;
    public Animator tuto;
    public Animator introPerso;

    public float transitionTime = 1f;
    public float tutoDuration = 10f;
    public float introPersoDuration = 10f;

    public bool gameStart = false;
    public bool isTuto = false;

    public int sceneToLoad;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;

        if (introPerso.gameObject != null)
        {
            introPerso.gameObject.SetActive(true);
        }
        if (isTuto)
        {
            if (tuto.gameObject != null)
            {
                tuto.gameObject.SetActive(true);
                StartCoroutine(EndTuto());
            }
        }
        else
        {
            StartCoroutine(EndIntroPerso());
        }

        transition.gameObject.SetActive(true);
        transition.Play("fade_Out");
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStart) return;
        currentTime -= 1 * Time.deltaTime;
        
        if (currentTime <= 0)
        {
            currentTime = 0;

            LoadNextLevel();
        }
    }

    [ContextMenu("LoadNextLevel")]
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(sceneToLoad));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
        Debug.Log("Game end");

        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator EndTuto()
    {
        Debug.Log("Start tuto");
        yield return new WaitForSeconds(tutoDuration);
        tuto.Play("fade_Out_Tuto");
        yield return new WaitForSeconds(transitionTime);
        Debug.Log("End tuto");
        StartCoroutine(EndIntroPerso());
        tuto.gameObject.SetActive(false);
    }

    IEnumerator EndIntroPerso()
    {
        
        Debug.Log("Start intro");
        yield return new WaitForSeconds(introPersoDuration);
        introPerso.Play("fade_Out_introPerso");
        yield return new WaitForSeconds(transitionTime);
        Debug.Log("End intro");
        gameStart = true;

        GetGoogleShit.Instance.LoadSheet();
        introPerso.gameObject.SetActive(false);
    }
}
