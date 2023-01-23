using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AltarAudio : MonoBehaviour
{
    [Header("Firewood Burn")][SerializeField] private AudioClip[] _firewoodBurnSounds;
    [SerializeField][Range(0, 1)] private float _firewoodBurnSoundsSoundLevel;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private static AudioClip GetRandomSound(AudioClip[] soundsArray)
    {
        return soundsArray[Random.Range(0, soundsArray.Length)];
    }

    public void PlayFirewoodBurnSound()
    {
        _audioSource.PlayOneShot(GetRandomSound(_firewoodBurnSounds), _firewoodBurnSoundsSoundLevel);
    }
}
