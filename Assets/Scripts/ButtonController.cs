using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class ButtonController : MonoBehaviour
{
    #region Transition
    public LevelLoader TransitionAnimation;
    public GameObject LevelLoaderGameObject;
    #endregion
    #region Dropdown
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private GameObject DropdownGameObject;
    #endregion

    #region Dropdown Values
    [HideInInspector] public int dropdownIndex;
    [HideInInspector] public string selectedAlgorithm;
    #endregion
    private void Start()
    {
        LevelLoaderGameObject = GameObject.Find("LevelLoader");
        TransitionAnimation = LevelLoaderGameObject.GetComponent<LevelLoader>();
        DropdownGameObject = GameObject.Find("AlgorithmDropdown");
        dropdown = DropdownGameObject.GetComponent<TMP_Dropdown>();
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
    #region FindButton
    public void FindButton()
    {
        dropdownIndex = dropdown.value;
        selectedAlgorithm = dropdown.options[dropdownIndex].text;
        Debug.Log(selectedAlgorithm);
        FindObjectOfType<AudioManager>().PlaySound("ButtonClick");
    }
    #endregion
    #region MainMenuButton
    public void MainMenuButton() {
        TransitionAnimation.LoadCertainScene(0);
        FindObjectOfType<AudioManager>().PlaySound("ButtonClick");
    }
    #endregion
}
