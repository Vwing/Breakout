using UnityEngine;
using System.Collections;

public class Glow : UnityEngine.MonoBehaviour
{
    public float GlowRate = 1f;
    float MaxIntensity;
    float elapsedTime;
    Light myLight;

	void Start () 
	{
        myLight = GetComponent<Light>();
        MaxIntensity = myLight.intensity;
        elapsedTime = 0f;
	}

	void Update ()
	{
        elapsedTime += Time.deltaTime;
        float fraction = elapsedTime % GlowRate / GlowRate;
        fraction *= 2;
        if(fraction < 1f)
            myLight.intensity = Mathf.Lerp(0f, MaxIntensity, fraction / 2);
        else
            myLight.intensity = Mathf.Lerp(0f, MaxIntensity, 1 - fraction / 2);
	}
}
