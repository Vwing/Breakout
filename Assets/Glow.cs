using UnityEngine;
using System.Collections;

public class Glow : UnityEngine.MonoBehaviour
{
    public float GlowRate = 1f;
    float MaxIntensity;
    float elapsedTime;
    Light light;

	void Start () 
	{
        light = GetComponent<Light>();
        MaxIntensity = light.intensity;
        elapsedTime = 0f;
	}

	void Update ()
	{
        elapsedTime += Time.deltaTime;
        float fraction = elapsedTime % GlowRate / GlowRate;
        fraction *= 2;
        if(fraction < 1f)
            light.intensity = Mathf.Lerp(0f, MaxIntensity, fraction / 2);
        else
            light.intensity = Mathf.Lerp(0f, MaxIntensity, 1 - fraction / 2);
	}
}
