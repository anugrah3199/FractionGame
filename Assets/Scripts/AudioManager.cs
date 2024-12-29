using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

/// <summary>
/// Audio Manager class for playing audio
/// </summary>
public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]AudioSource audioSource;
    [SerializeField]AudioClip AddSound;

    [SerializeField]AudioClip celebration;
    [SerializeField]AudioClip SubtractSound;

    private void OnEnable()
    {
        PieMaker.OnCorrectAnswer += PlayCelebration;
    }

    private void OnDisable()
    {
        PieMaker.OnCorrectAnswer -= PlayCelebration;
    }

    void PlayAudio(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
    public void AddClip()
    {
        PlayAudio(AddSound);
    }

    public void RemoveClip()
    {
        PlayAudio(SubtractSound);
    }

    public void PlayCelebration()
    {
        PlayAudio(celebration);
    }
}
