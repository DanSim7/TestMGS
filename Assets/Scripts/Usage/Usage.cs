using System.Collections;
using Networking.MessageModels.Lobby;
using Networking.Utils;
using UnityEngine;
using Networking;

namespace Usage
{
    public class Usage : MonoBehaviour
    {
        [SerializeField] private ServerCommunication _communication;
        
        [SerializeField] private UIForm _uiForm;
        [SerializeField] private MenuForm _menuForm;

        private void Start()
        {
            _communication.ConnectToServer();
            _communication.OnConnected += _uiForm.SwitchGreenLight;
            _communication.OnConnected += OnConnectedToServer;
            _communication.OnDisconnected += _uiForm.SwitchRedLight;
            _communication.OnDisconnected += OnDisconnectedToServer;
            
            FileIO.ReadConfig(out var hostIp, out var port);
            _menuForm.UpdateIpPort(hostIp, port);
        }

        private void OnConnectedToServer()
        {
            _communication.Lobby.GetCurrentOdometerOperation();

            _communication.Lobby.OnCurrentOdometerMessage += OnCurrentOdometerMessage;
            _communication.Lobby.OnOdometerValMessage += OnOdometerValMessage;
            _communication.Lobby.OnRandomStatusMessage += OnRandomStatusMessage;

            StartCoroutine(RequestRandomStatus());
        }
        
        private void OnDisconnectedToServer()
        {
            _communication.Lobby.OnCurrentOdometerMessage -= OnCurrentOdometerMessage;
            _communication.Lobby.OnOdometerValMessage -= OnOdometerValMessage;
            _communication.Lobby.OnRandomStatusMessage -= OnRandomStatusMessage;

            StopAllCoroutines();
        }

        private void OnCurrentOdometerMessage(CurrentOdometerModel message)
        {
            Debug.Log("Message received: " + message.odometer);
            _uiForm.UpdateOdometerText(message.odometer);
        }
        
        private void OnOdometerValMessage(OdometerValModel message)
        {
            Debug.Log("Message received: " + message.value);
            _uiForm.UpdateOdometerText(message.value);
        }
        
        private void OnRandomStatusMessage(RandomStatusModel message)
        {
            Debug.Log("Message received: " + message.status);
            _uiForm.UpdateRandomStatus(message.status);
        }
        
        public void ReloadServer()
        {
            FileIO.WriteConfig(_menuForm.IpText, _menuForm.PortText);
            _communication.ReloadServer();
        }

        private IEnumerator RequestRandomStatus()
        {
            while (true)
            {
                if (_communication.IsConnected)
                    _communication.Lobby.GetRandomStatusOperation();
                yield return new WaitForSeconds(10f);
            }
        }
    }
}