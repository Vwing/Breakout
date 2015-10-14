//changes the active camera depending on whether level
//is played in editor, or on Android.
//
//Feel free to make a third camera in scene for testing
//using mouse-look, and edit the script accordingly. Perhaps
//put a check in there to check if PlayerSettings.VRIsEnabled is true.

using UnityEngine;
using System.Collections;

public class ChooseRiftOrCardboard : MonoBehaviour {
    public GameObject RiftCamera;
    public GameObject CardboardCamera;
    public GameObject TheChosenCamera;
	// Use this for initialization
	void Awake () 
    {
#if UNITY_EDITOR
        RiftCamera.SetActive(true);
        TheChosenCamera = RiftCamera;
#elif UNITY_STANDALONE
        RiftCamera.SetActive(true);
        TheChosenCamera = RiftCamera;
#elif UNITY_ANDROID
        CardboardCamera.SetActive(true);
        TheChosenCamera = CardboardCamera;
#endif
    }
}
