using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public void LoadNextLevel()
    {
        Debug.Log("Animation Loaded");
        StartCoroutine(LoadWithTransition(SceneManager.GetActiveScene().buildIndex + 1));
    }
    public void RestartLevel()
    {
        Debug.Log("Animation Loaded");
        StartCoroutine(LoadWithTransition(SceneManager.GetActiveScene().buildIndex));
    }
    public void LoadCertainScene(int SceneIdx)
    {
        Debug.Log("Moved To " + SceneIdx);
        StartCoroutine(LoadWithTransition(SceneIdx));
    }
    IEnumerator LoadWithTransition(int LevelIdx)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(LevelIdx);
    }
}
