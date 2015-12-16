using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour
{
    public float fadeTime = 5f;
    Renderer[] rends;
    public bool fading;

	void Awake () 
	{
        rends = GetComponentsInChildren<Renderer>();
	}

    public void In()
    {
        if(!fading)
            StartCoroutine("FadeIn");
    }

    public void Out()
    {
        if(!fading)
            StartCoroutine("FadeOut");
    }

    IEnumerator FadeIn()
    {
        if (fading)
            yield break;
        if(rends[0].enabled)
        {
            foreach (Renderer rend in rends)
            {
                Color c = rend.material.color;
                c.a = 0;
                rend.material.color = c;
                rend.enabled = false;
            }
        }
        fading = true;
        foreach (Renderer rend in rends)
            rend.enabled = true;
        for (float i = 0f; i <= fadeTime; i += .1f)
        {
            foreach (Renderer rend in rends)
            {
                Color c = rend.material.color;
                c.a = i / fadeTime;
                rend.material.color = c;
            }
            yield return new WaitForSeconds(.1f);
        }
        fading = false;
    }

    IEnumerator FadeOut()
    {
        if (!rends[0].enabled || fading)
            yield break;
        fading = true;
        for (float i = fadeTime; i >= 0f; i -= .1f)
        {
            foreach (Renderer rend in rends)
            {
                Color c = rend.material.color;
                c.a = i / fadeTime;
                rend.material.color = c;
            }
            yield return new WaitForSeconds(.1f);
        }
        foreach (Renderer rend in rends)
            rend.enabled = false;
        fading = false;
    }
}
