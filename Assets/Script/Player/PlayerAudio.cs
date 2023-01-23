using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerAudio : MonoBehaviour
{
    [Header("Steps")][SerializeField] private AudioClip[] _stepSounds;
    [SerializeField][Range(0, 1)] private float _stepsSoundLevel;

    [Header("Die")][SerializeField] private AudioClip[] _dieSounds;
    [SerializeField][Range(0, 1)] private float _dieSoundsSoundLevel;

    [Header("Hit")][SerializeField] private AudioClip[] _hitSounds;
    [SerializeField][Range(0, 1)] private float _hitSoundsSoundLevel;

    [Header("Player Interact Witn Item")][SerializeField] private AudioClip[] _interactWitnItemSounds;
    [SerializeField][Range(0, 1)] private float _interactWitnItemSoundsSoundLevel;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private static AudioClip GetRandomSound(AudioClip[] soundsArray)
    {
        return soundsArray[Random.Range(0, soundsArray.Length)];
    }

    public void PlayHitSound()
    {
        _audioSource.PlayOneShot(GetRandomSound(_hitSounds), _hitSoundsSoundLevel);
    }

    public void PlayDieSound()
    {
        _audioSource.PlayOneShot(GetRandomSound(_dieSounds), _dieSoundsSoundLevel);
    }

    public void PlayStepSound()
    {
        _audioSource.PlayOneShot(GetRandomSound(_stepSounds), _stepsSoundLevel);
    }

    public void PlayInteractWitnItemSound()
    {
        _audioSource.PlayOneShot(GetRandomSound(_interactWitnItemSounds), _interactWitnItemSoundsSoundLevel);
    }
}
