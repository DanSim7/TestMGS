using System.Collections;
using Networking.MessageModels;
using Networking.MessageModels.Lobby;
using Networking.MessagingGroups;
using Networking.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace Networking
{
    public class ServerCommunication : MonoBehaviour
    {
        private WsClient _client;

        public event UnityAction OnConnected;
        public event UnityAction OnDisconnected;

        public LobbyMessaging Lobby { private set; get; }

        public bool IsConnected { get; private set; } = false;

        private void Awake()
        {
            UpdateServerInfo();

            Lobby = new LobbyMessaging(this);
        }

        private void UpdateServerInfo()
        {
            FileIO.ReadConfig(out var hostIp, out var port);
            var server = "ws://" + hostIp + ":" + port + "/ws";
            _client = new WsClient(server);
        }

        private void Start()
        {
            StartCoroutine(CheckingConnection());
            OnDisconnected += ConnectToServer;
        }

        private void Update()
        {
            var concurrentQueue = _client.receiveQueue;
            string message;
            while (concurrentQueue.TryPeek(out message))
            {
                concurrentQueue.TryDequeue(out message);
                HandleMessage(message);
            }
        }

        private void HandleMessage(string message)
        {
            Debug.Log("Server: " + message);

            var operation = JsonUtility.FromJson<MessageModel>(message).operation;

            switch (operation)
            {
                case LobbyMessaging.OdometerValName:
                    Lobby.OnOdometerValMessage?.Invoke(JsonUtility.FromJson<OdometerValModel>(message));
                    break;
                case LobbyMessaging.CurrentOdometerName:
                    Lobby.OnCurrentOdometerMessage?.Invoke(JsonUtility.FromJson<CurrentOdometerModel>(message));
                    break;
                case LobbyMessaging.RandomStatusName:
                    Lobby.OnRandomStatusMessage?.Invoke(JsonUtility.FromJson<RandomStatusModel>(message));
                    break;
                default:
                    Debug.LogError("Unknown type of operation: " + operation);
                    break;
            }
        }

        public void ReloadServer()
        {
            _client.Close();
            UpdateServerInfo();
            ConnectToServer();
        }

        public async void ConnectToServer()
        {
            await _client.Connect();
        }

        public void SendRequest(string message)
        {
            _client.Send(message);
        }

        private IEnumerator CheckingConnection()
        {
            while (true)
            {
                if (_client.IsConnectionOpen() != IsConnected)
                {
                    IsConnected = _client.IsConnectionOpen();
                    if (IsConnected)
                        OnConnected?.Invoke();
                    else
                        OnDisconnected?.Invoke();
                }
                
                yield return new WaitForSeconds(1f);
            }
        }
    }
}