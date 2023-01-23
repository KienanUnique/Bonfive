using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip[] _backgroundMusic;
    [SerializeField][Range(0, 1)] private float _backgroundMusicSoundLevel;

    private AudioSource _audioSource;
    private int _playingTrackIndex;
    private int RandomBackgroundTrackIndex => Random.Range(0, _backgroundMusic.Length);

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _playingTrackIndex = RandomBackgroundTrackIndex;
    }

    private void Update()
    {
        if (!_audioSource.isPlaying)
        {
            PlayNextTrack();
        }
    }

    private void PlayNextTrack()
    {
        int nextTrackIndex;
        if (_backgroundMusic.Length == 1)
        {
            nextTrackIndex = 0;
        }
        else
        {
            do
            {
                nextTrackIndex = RandomBackgroundTrackIndex;
            } while (nextTrackIndex == _playingTrackIndex);
        }
        _playingTrackIndex = nextTrackIndex;
        _audioSource.PlayOneShot(_backgroundMusic[nextTrackIndex], _backgroundMusicSoundLevel);
    }
}
