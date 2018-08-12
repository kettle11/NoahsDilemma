using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
       // StartGame();
	}
	
    public void StartGame()
    {
        LevelManager.instance.StartGame();
        this.gameObject.SetActive(false);
        SoundSource.PlayStart();
    }
}
