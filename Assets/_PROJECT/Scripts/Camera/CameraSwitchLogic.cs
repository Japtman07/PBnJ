using Unity.VisualScripting;
using UnityEngine;

public class CameraSwitchLogic : MonoBehaviour
{
[SerializeField] private GameObject myCam;
[SerializeField] private CameraManager cm;
private void OnTriggerEnter(Collider other)
{
if(other.CompareTag("Player"))
{
if (cm != null)
{
cm.SwitchCam(myCam);
}
}
}

}
