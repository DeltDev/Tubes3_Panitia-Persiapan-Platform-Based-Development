using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    #region Transition
    public LevelLoader TransitionAnimation;
    public GameObject LevelLoaderGameObject;
    #endregion

    private void Start()
    {
        LevelLoaderGameObject = GameObject.Find("LevelLoader");
        TransitionAnimation = LevelLoaderGameObject.GetComponent<LevelLoader>();
    }
    #region Play Button
    public void PlayButton()
    {
        TransitionAnimation.LoadCertainScene(1);
        FindObjectOfType<AudioManager>().PlaySound("ButtonClick");
    }
    #endregion

    #region Exit Button
    public void ExitButton()
    {
        FindObjectOfType<AudioManager>().PlaySound("ButtonClick");
        Application.Quit();
        Debug.Log("Aplikasi sudah keluar");
    }
    #endregion
    #region Hover Button
    public void HoverPlaySound()
    {
        FindObjectOfType<AudioManager>().PlaySound("ButtonHover");
    }
    #endregion

    #region MainMenuButton
    public void MainMenuButton() {
        TransitionAnimation.LoadCertainScene(0);
        FindObjectOfType<AudioManager>().PlaySound("ButtonClick");
    }
    #endregion
}
