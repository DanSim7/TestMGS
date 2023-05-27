using Networking.MessageModels.Lobby;
using UnityEngine.Events;

namespace Networking.MessagingGroups
{
    public class LobbyMessaging : BaseMessaging
    {
        public LobbyMessaging(ServerCommunication client) : base(client) { }

        public const string OdometerValName = "odometer_val";
        public UnityAction<OdometerValModel> OnOdometerValMessage;

        public const string CurrentOdometerName = "currentOdometer";
        public UnityAction<CurrentOdometerModel> OnCurrentOdometerMessage;

        public const string RandomStatusName = "randomStatus";
        public UnityAction<RandomStatusModel> OnRandomStatusMessage;

        public void GetCurrentOdometerOperation()
        {
            var message = "{\"operation\": \"getCurrentOdometer\"}";
            client.SendRequest(message);
        }

        public void GetRandomStatusOperation()
        {
            var message = "{\"operation\": \"getRandomStatus\"}";
            client.SendRequest(message);
        }
    }
}