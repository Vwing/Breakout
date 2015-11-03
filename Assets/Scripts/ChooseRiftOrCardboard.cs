//changes the active camera depending on whether level
//is played in editor, or on Android.
//
//Feel free to make a third camera in scene for testing
//using mouse-look, and edit the script accordingly. Perhaps
//put a check in there to check if PlayerSettings.VRIsEnabled is true.

using UnityEngine;
using System.Collections;

public class ChooseRiftOrCardboard : UnityEngine.MonoBehaviour {
    public GameObject RiftCamera;
    public GameObject CardboardCamera;
    //public GameObject TheChosenCamera;

	void Awake () 
    {
#if UNITY_EDITOR
        RiftCamera.SetActive(true);
        CardboardCamera.SetActive(false);
        if(UnityEngine.VR.VRSettings.enabled)
            RiftCamera.GetComponent<SmoothedMouseLook>().enabled = false;
        else
            RiftCamera.GetComponent<SmoothedMouseLook>().enabled = true;
        //TheChosenCamera = RiftCamera;
#elif UNITY_STANDALONE
        RiftCamera.SetActive(true);
        CardboardCamera.SetActive(false);
        if(UnityEngine.VR.VRSettings.enabled)
            RiftCamera.GetComponent<SmoothedMouseLook>().enabled = false;
        else
            RiftCamera.GetComponent<SmoothedMouseLook>().enabled = true;
#elif UNITY_ANDROID
        RiftCamera.SetActive(false);
        CardboardCamera.SetActive(true);
#endif
    }
}
