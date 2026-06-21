using UnityEngine;

public class CameraSwitchLogic : MonoBehaviour
{
[SerializeField] private GameObject myCam;

private void OnTriggerEnter(Collider other)
{
if(other.CompareTag("Player"))
{
if (CameraManager.Instance != null)
{
CameraManager.Instance.SwitchCam(myCam);
}
}
}

}
