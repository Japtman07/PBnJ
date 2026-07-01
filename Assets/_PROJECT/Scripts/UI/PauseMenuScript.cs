using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
   [Header("Script References")]
   private PlayerControllerRB _playerController;
   
   [Header("Object References")]
   [SerializeField] private GameObject pauseMenu;
   [SerializeField] private GameObject optionsMenu;

   public void OnPaused()
   {
      pauseMenu.SetActive(true);
   }

   public void OnUnpaused()
   {
      pauseMenu.SetActive(false);
   }

   public void OptionsMenu()
   {
      optionsMenu.SetActive(true);
   }

   public void QuitGame()
   {
      SceneManager.LoadScene(0);
   }
}
