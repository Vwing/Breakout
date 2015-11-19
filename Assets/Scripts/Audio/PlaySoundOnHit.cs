using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaySoundOnHit : UnityEngine.MonoBehaviour
{
    public List<AudioClip> sounds;
    private AudioSource aud;

    void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Ball")
            aud.PlayOneShot(sounds[Random.Range(0, sounds.Count - 1)]);
    }
}
