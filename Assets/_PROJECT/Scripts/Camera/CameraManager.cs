using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    
[SerializeField] public GameObject[] allCams;

// method is called by  trigger boxes when the player enters
public void SwitchCam(GameObject activeCam)
{
foreach(GameObject cam in allCams)
{
// Turns all cameras off
cam.SetActive(false);
}
// Turn on specifc cam
activeCam.SetActive(true);
}
}