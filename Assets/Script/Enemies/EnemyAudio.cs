using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemyAudio : MonoBehaviour
{
    [Header("Steps")][SerializeField] private AudioClip[] _stepSounds;
    [SerializeField][Range(0, 1)] private float _stepsSoundLevel;

    [Header("Die")][SerializeField] private AudioClip[] _dieSounds;
    [SerializeField][Range(0, 1)] private float _dieSoundsSoundLevel;

    [Header("Attack")][SerializeField] private AudioClip[] _attackSounds;
    [SerializeField][Range(0, 1)] private float _attackSoundsSoundLevel;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private static AudioClip GetRandomSound(AudioClip[] soundsArray)
    {
        return soundsArray[Random.Range(0, soundsArray.Length)];
    }

    public void PlayAttackSound()
    {
        _audioSource.PlayOneShot(GetRandomSound(_attackSounds), _attackSoundsSoundLevel);
    }

    public void PlayDieSound()
    {
        _audioSource.PlayOneShot(GetRandomSound(_dieSounds), _dieSoundsSoundLevel);
    }

    public void PlayStepSound()
    {
        _audioSource.PlayOneShot(GetRandomSound(_stepSounds), _stepsSoundLevel);
    }
}
