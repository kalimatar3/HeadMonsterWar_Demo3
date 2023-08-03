using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundbyAnim : MyBehaviour
{
    public void PlaySound(string Name)
    {
        SoundSpawner.Instance.Play_audio(Name);
    }
}
