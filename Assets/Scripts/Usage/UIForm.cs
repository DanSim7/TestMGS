using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Usage
{
    public class UIForm : MonoBehaviour
    {
        [SerializeField] private TMP_Text _odometerText;
        
        [Space(10)]
        [SerializeField] private Toggle _randomStatusCheckbox;
        
        [Space(10)]
        [SerializeField] private Image _connectionLight;
        [SerializeField] private TMP_Text _connectionStatusText;
        
        public void UpdateOdometerText(float odometer)
        {
            _odometerText.transform.PlayPulseAnim();
            StopAllCoroutines();
            StartCoroutine(CountOdometer(odometer));
        }

        public void UpdateRandomStatus(bool status)
        {
            _randomStatusCheckbox.transform.PlayPulseAnim();
            _randomStatusCheckbox.isOn = status;
        }

        public void SwitchGreenLight()
        {
            _connectionLight.transform.PlayPulseAnim();
            _connectionLight.color = Color.green;
            _connectionStatusText.text = "Connected";
        }
        
        public void SwitchRedLight()
        {
            _connectionLight.transform.PlayPulseAnim();
            _connectionLight.color = Color.red;
            _connectionStatusText.text = "Connecting";
        }
        
        private IEnumerator CountOdometer(float targetValue)
        {
            float currentValue = float.Parse(_odometerText.text);
            var rate = Mathf.Abs(targetValue - currentValue);
            while (currentValue != targetValue)
            {
                currentValue = Mathf.MoveTowards(currentValue, targetValue, rate * Time.deltaTime);
                _odometerText.text = currentValue.ToString();
                yield return null;
            }
        }
    }
}