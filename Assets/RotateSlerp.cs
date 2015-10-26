using UnityEngine;
using System.Collections;

public class RotateSlerp : MonoBehaviour
{
    Quaternion begin;
    Quaternion end;
    [Tooltip("Sets which axis to turn on (x, y, z, or w)")]
    public char axis = 'x';
    [Tooltip("How many degrees to turn")]
    public float degrees = 30;
    [Tooltip("How fast to turn, in degrees per second")]
    public float speed = 10;
    public float timeTilStop = 1f;
    float origTimeTilStop;

    private float elapsedTime = 0f;
    private float maxTime;
    private bool stopMoving = false;
    private MeshRenderer mesh;

	void Start () 
	{
        begin = transform.localRotation;
        end = begin;
        maxTime = degrees / speed;
        origTimeTilStop = timeTilStop;

        if (axis == 'x')
            end.Set(begin.x + degrees, begin.y, begin.z, begin.w);
        else if (axis == 'y')
            end.Set(begin.x, begin.y + degrees, begin.z, begin.w);
        else if (axis == 'z')
            end.Set(begin.x, begin.y, begin.z + degrees, begin.w);
        else if (axis == 'w')
            end.Set(begin.x, begin.y, begin.z, begin.w + degrees);

        mesh = transform.GetComponentInChildren<MeshRenderer>();

        Debug.Log("Begin: " + begin + " End: " + end);
	}

	void Update ()
	{
        if (mesh.isVisible && stopMoving)
            return;
        StopMovingMaybe();
        DoTheLerp();
	}

    void StopMovingMaybe()
    {
        if (mesh.isVisible)
        {
            timeTilStop -= Time.deltaTime;
            if (timeTilStop <= 0f)
                stopMoving = true;
        }
        else
        {
            stopMoving = false;
            timeTilStop = origTimeTilStop;
        }
    }

    bool reachedEnd = false;
    void DoTheLerp()
    {
        elapsedTime += Time.deltaTime;
        float fracJourney = elapsedTime / maxTime;

        if (!reachedEnd)
            transform.localRotation = Quaternion.Lerp(begin, end, fracJourney);
        else
            transform.localRotation = Quaternion.Lerp(end, begin, fracJourney);

        if (elapsedTime > maxTime)
        {
            elapsedTime = 0f;
            reachedEnd = !reachedEnd;
        }
    }
}
