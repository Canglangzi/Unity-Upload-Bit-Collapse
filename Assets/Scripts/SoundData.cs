using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sound Data", menuName = "ScriptableObjects/Sound Data", order = 1)]
public class SoundData : ScriptableObject
{
    public AudioClip clip;
    public bool loop;
    [Range(0, 1)] public float volume = 1f;
}

