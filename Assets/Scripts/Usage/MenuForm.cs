using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Usage
{
    public class MenuForm : MonoBehaviour
    {
        [Header("Systems")]
        [SerializeField] private SoundMusicSystem _soundMusicSystem;

        [Space(10)]
        [Header("Toggles")]
        [SerializeField] private GameObject _soundToggleCross;
        [SerializeField] private Toggle _soundToggle;
        [SerializeField] private GameObject _musicToggleCross;
        [SerializeField] private Toggle _musicToggle;
        
        [Space(10)]
        [Header("InputFields")]
        [SerializeField] private TMP_InputField _ipText;
        [SerializeField] private TMP_InputField _portText;
        [SerializeField] private TMP_InputField _videoUrlText;

        [Space(10)]
        [Header("Slider")]
        [SerializeField] private Slider _volumeSlider;

        public string VideoUrlText => _videoUrlText.text;
        public string IpText => _ipText.text;
        public string PortText => _portText.text;

        private void Awake()
        {
            _soundToggle.isOn = SaveSystem.IsSoundOn;
            _musicToggle.isOn = SaveSystem.IsMusicOn;
            _volumeSlider.value = SaveSystem.SoundMusicVolume;
        }

        public void UpdateIpPort(string hostIp, string port)
        {
            _ipText.text = hostIp;
            _portText.text = port;
        }

        public void ToggleSound(bool isOn)
        {
            _soundMusicSystem.ToggleSound(isOn);
            _soundToggleCross.SetActive(!isOn);
        }

        public void ToggleMusic(bool isOn)
        {
            _soundMusicSystem.ToggleMusic(isOn);
            _musicToggleCross.SetActive(!isOn);
        }
    }
}