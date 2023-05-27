using System.IO;

namespace Networking.Utils
{
    public static class FileIO
    {
        private const string PATH = "Assets/Resources/config.txt";

        public static void WriteConfig(string ip, string port)
        {
            StreamWriter writer = new StreamWriter(PATH);
            writer.WriteLine($"{ip}\n{port}");
            writer.Close();
        }

        public static void ReadConfig(out string ip, out string port)
        {
            StreamReader reader = new StreamReader(PATH);
            ip = reader.ReadLine();
            port = reader.ReadLine();
            reader.Close();
        }
    }
}