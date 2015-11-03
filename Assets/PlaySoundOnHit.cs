using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaySoundOnHit : UnityEngine.MonoBehaviour
{
    public List<AudioClip> sounds;
    private AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Ball")
            audio.PlayOneShot(sounds[Random.Range(0, sounds.Count - 1)]);
    }
}
