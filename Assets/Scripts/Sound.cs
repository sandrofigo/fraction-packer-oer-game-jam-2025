using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioClip[] audioClips;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void playClip(int clipNumber)
    {
        audioSource.PlayOneShot(audioClips[clipNumber]);
    }
}
