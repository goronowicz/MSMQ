using System;
using System.Messaging;

namespace MSMQ.MulticastMessageSender
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateRoomQueues(args[0].Split(new []{';'}, StringSplitOptions.RemoveEmptyEntries));

            var ipAddres = args[1];

            var multicastAddres = $"formatname:multicast={ipAddres}";

            using (var queue = new MessageQueue(multicastAddres))
            {
                var consoleKeyInfo = new ConsoleKeyInfo();
                int i = 0;
                while (consoleKeyInfo.Key != ConsoleKey.Escape)
                {
                    object message = $"Test {i++}";
                    queue.Send(message);

                    Console.WriteLine($"Message {message} send to the multicast addres : {ipAddres}");
                    consoleKeyInfo = Console.ReadKey();
                }
            }
        }

        private static void CreateRoomQueues(string[] paths)
        {
            foreach (var path in paths)
            {
                if (!MessageQueue.Exists(path))
                {
                    MessageQueue.Create(path);
                }
            }            
        }
    }

    public class MulticastMessage
    {
        public string Name { get; set; }
        public string Content { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, Content: {Content}";
        }
    }
}
