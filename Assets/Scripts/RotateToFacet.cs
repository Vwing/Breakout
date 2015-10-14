//Currently works in the Oculus Rift, but not in Cardboard. Rotates the
//player so that they are facing the facet of the cube they're looking toward.
//Sort of disorientating / nauseating. Probably won't want it as-is in the
//final product; may want to fade to black and back again, rather than lerping.

using UnityEngine;
using System.Collections;

public class RotateToFacet : MonoBehaviour
{
    public float timeToSlerp = 0.5f;
    public Transform North;
    public Transform East;
    public Transform West;
    public Transform South;
    public Transform Up;
    public Transform Down;
    private int paddleBoxLayer;
    private Transform cameraTransform;
    private CardboardHead cardboardHead;
    private bool android = false;

    void Start ()
    {
        paddleBoxLayer = 1 << LayerMask.NameToLayer("PaddleBox");
        cameraTransform = GetComponent<ChooseRiftOrCardboard>().TheChosenCamera.transform;
#if UNITY_EDITOR
        cardboardHead = null;
#elif UNITY_STANDALONE
        cardboardHead = null;
#elif UNITY_ANDROID
        cardboardHead = cameraTransform.parent.gameObject.GetComponent<CardboardHead>();
        android = true;
#endif
    }

	void Update ()
	{
        if (Input.GetButtonDown("Fire1") || Cardboard.SDK.Triggered)
        {
            RaycastHit hit;
            Vector3 fwd = cameraTransform.TransformDirection(Vector3.forward);
            Physics.Raycast(transform.position, fwd * 10, out hit, paddleBoxLayer);
            if (!android)
                RotateToDirection(hit.transform.gameObject.name);
            else
            {
                cardboardHead.target = ReturnDirectionTransform(hit.transform.gameObject.name);
                Cardboard.SDK.Recenter();
            }
        }
	}

    Transform ReturnDirectionTransform(string direction)
    {
        if (direction == "North")
            return North;
        if (direction == "East")
            return East;
        if (direction == "South")
            return South;
        if (direction == "West")
            return West;
        if (direction == "Up")
            return Up;
        if (direction == "Down")
            return Down;
        return null;
    }

    void RotateToDirection(string direction)
    {
        if (direction == "North")
            StartCoroutine(SlerpToRotation(North.rotation));
        if (direction == "East")
            StartCoroutine(SlerpToRotation(East.rotation));
        if (direction == "South")
            StartCoroutine(SlerpToRotation(South.rotation));
        if (direction == "West")
            StartCoroutine(SlerpToRotation(West.rotation));
        if (direction == "Up")
            StartCoroutine(SlerpToRotation(Up.rotation));
        if (direction == "Down")
            StartCoroutine(SlerpToRotation(Down.rotation));
    }

    private bool rotating = false;

    IEnumerator SlerpToRotation(Quaternion rotation)
    {
        if (rotating)
            yield return null;
        rotating = true;
        float elapsedTime = 0f;
        Quaternion initialRotation = transform.rotation;
        while(elapsedTime < timeToSlerp)
        {
            transform.rotation = Quaternion.Slerp(initialRotation, rotation, elapsedTime / timeToSlerp);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        //if (android)
        //{
        //    cameraTransform.parent.rotation = transform.rotation;
        //    Cardboard.SDK.Recenter();
        //}
        rotating = false;
    }
}
