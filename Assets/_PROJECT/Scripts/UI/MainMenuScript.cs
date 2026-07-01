using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] private GameObject optionsMenu;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OptionsMenu()
    {
        optionsMenu.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
}
