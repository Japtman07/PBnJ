using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    public GameObject myCam;

    public void OTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            myCam.SetActive(true);
        }
        else
        {
            myCam.SetActive(false);
        }
    }
}
