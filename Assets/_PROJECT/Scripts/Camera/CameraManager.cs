using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
public static CameraManager Instance;
[SerializeField] public GameObject[] allCams;

void Awake()
{
if (Instance != null && Instance != this) 
{ 
Destroy(this); 
} 
else 
{ 
Instance = this; 
} 
}

public void SwitchCam(GameObject activeCam)
{
foreach(GameObject cam in allCams)
{
cam.SetActive(false);
}
activeCam.SetActive(true);
}
}






