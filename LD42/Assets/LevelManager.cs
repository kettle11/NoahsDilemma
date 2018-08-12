using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public List<Level> levels;

    int currentIndex = 0;

    public static LevelManager instance;

    public GameObject victoryScreen;

    public static Level currentLevel = null;

    private void Awake()
    {
        instance = this;
    }
    // Use this for initialization
    void Start () {
       // currentIndex = levels.Count - 1;
        LoadLevel(levels[currentIndex]);
	}

    public Camera mainCamera;
    public void StartGame()
    {
        mainCamera.gameObject.SetActive(true);
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

    public GameObject endScreen;

    public void NextLevel()
    {
        currentIndex++;
        if (currentIndex >= levels.Count)
        {
            currentIndex = levels.Count - 1;
            mainCamera.enabled = false;
            endScreen.SetActive(true);
            SoundSource.PlayCompleteGame();
        }

        LoadLevel(levels[currentIndex]);

        victoryScreen.SetActive(false);
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

    public void Victory()
    {
        victoryScreen.SetActive(true);
        SoundSource.PlayCompleteLevel();
       // Debug.Log("Victory!");
      //  NextLevel();
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
