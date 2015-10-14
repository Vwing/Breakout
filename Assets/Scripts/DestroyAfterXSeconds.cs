using UnityEngine;
using System.Collections;

public class DestroyAfterXSeconds : MonoBehaviour
{
    public float HowLongInSeconds = 3;
    float elapsedTime = 0f;

	void Update ()
	{
        if (elapsedTime > HowLongInSeconds)
            Destroy(this.gameObject);
        elapsedTime += Time.deltaTime;
	}
}
