using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour { 
    
	public void LoadSceneOnClick(string sceneName)
    {
        PlayerPrefs.SetInt("PreviousScene", SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadSceneAsync(sceneName);    
    }    
    public void ReloadPreviousScene()
    {
        int sceneNumber = PlayerPrefs.GetInt("PreviousScene");
        SceneManager.LoadSceneAsync(sceneNumber);
    }
    public void ReloadScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
