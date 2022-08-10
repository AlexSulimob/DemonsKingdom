using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource source;
    public void PlaySfx(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }

}
