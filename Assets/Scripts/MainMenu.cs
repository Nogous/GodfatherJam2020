﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LevelTwo()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT !!");
        Application.Quit();
    }

    public void LevelOne()
    {
        SceneManager.LoadScene(1);
    }

    public void LevelTree()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadSceneNb(int i)
    {
        SceneManager.LoadScene(i);
    }
}
