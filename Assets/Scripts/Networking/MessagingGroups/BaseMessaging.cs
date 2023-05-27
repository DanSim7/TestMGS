namespace Networking.MessagingGroups
{
    public class BaseMessaging
    {
        protected ServerCommunication client;

        public BaseMessaging(ServerCommunication client)
        {
            this.client = client;
        }
    }
}