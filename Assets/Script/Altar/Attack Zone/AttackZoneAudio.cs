using UnityEngine;

public class AttackZoneAudio : MonoBehaviour
{
    [Header("Attack")][SerializeField] private AudioClip[] _attackSounds;
    [SerializeField][Range(0, 1)] private float _attackSoundsSoundLevel;

    [SerializeField] private AudioSource _audioSource;

    private static AudioClip GetRandomSound(AudioClip[] soundsArray)
    {
        return soundsArray[Random.Range(0, soundsArray.Length)];
    }

    public void PlayAttackSound()
    {
        _audioSource.PlayOneShot(GetRandomSound(_attackSounds), _attackSoundsSoundLevel);
    }
}
