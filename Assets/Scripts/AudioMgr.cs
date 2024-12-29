using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMgr : MonoBehaviour
{
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
