﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public List<Level> levels;

    int currentIndex = 0;
    public static Level currentLevel = null;
	// Use this for initialization
	void Start () {
        LoadLevel(levels[0]);
	}
	
    void PreviousLevel()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = 0;
        }

        LoadLevel(levels[0]);
    }

    void NextLevel()
    {
        currentIndex++;
        if (currentIndex >= levels.Count) currentIndex = levels.Count - 1;

        LoadLevel(levels[currentIndex]);
    }

    void LoadLevel(Level level)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        currentLevel = GameObject.Instantiate(level);
        currentLevel.gameObject.SetActive(true);
        currentLevel.disableImmediately = false;
        currentLevel.CreateLevel();
        currentLevel.transform.position = new Vector3(0, 0, -.5f);
    }


	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.N))
        {
            NextLevel();
        }	

        if (Input.GetKeyDown(KeyCode.P))
        {
            PreviousLevel();
        }
	}
}