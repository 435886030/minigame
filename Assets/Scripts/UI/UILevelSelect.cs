using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UILevelSelect : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LevelSelectButtonDown(string lvName)
    {
        SceneManager.LoadScene(lvName);
    }
}
