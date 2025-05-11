using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] _audioClips;

    public void PlayClip(string soundName, float volume, float pitch)
    {
        var obj = new GameObject
        {
            name = soundName
        };

        var audioSource = obj.AddComponent<AudioSource>();
        AudioClip clip = _audioClips.First(c => c.name == soundName).clip;
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.spatialBlend = 0;
        audioSource.loop = false;
        Destroy(obj, clip.length * 2);
    }
}