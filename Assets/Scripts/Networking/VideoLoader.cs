using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using Usage;

namespace Networking
{
    public class VideoLoader : MonoBehaviour
    {
        [SerializeField] private VideoPlayer _videoPlayer;
        [SerializeField] private SoundMusicSystem _soundMusicSystem;
        [SerializeField] private AudioSource _videoSource;

        [SerializeField] private MenuForm _menuForm;

        public void Start()
        {
            LoadVideo();
        }

        private void LoadVideo()
        {
            _videoPlayer.url = _menuForm.VideoUrlText;
            _videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
            _videoPlayer.EnableAudioTrack(0, true);
            _videoPlayer.SetTargetAudioSource(0, _videoSource);
            _videoPlayer.Prepare();
        }

        public void PlayVideo()
        {
            _soundMusicSystem.StopMusic();
            _videoPlayer.Play();
        }

        public void UpdateVideo()
        {
            if (_videoPlayer.isPlaying)
                _videoPlayer.Stop();
            LoadVideo();
        }

        private IEnumerator CheckingVideoEnding()
        {
            while (_videoPlayer.isPlaying)
                yield return new WaitForSeconds(1f);
            
            _soundMusicSystem.PlayMusic();
        }
    }
}