using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] _audioClips;

    public void PlayClip(string soundName, float volume, float pitch)
    {
        Sound sound = _audioClips.FirstOrDefault(c => c.name == soundName);
        if (sound == null)
        {
            Debug.LogError($"Could not find sound '{soundName}'");
            return;
        }

        var obj = new GameObject
        {
            name = $"AudioSource ({soundName})"
        };

        var audioSource = obj.AddComponent<AudioSource>();
        AudioClip clip = sound.clip;
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.spatialBlend = 0;
        audioSource.loop = false;
        audioSource.Play();
        Destroy(obj, Mathf.Max(clip.length * 2, 3f));
    }
}