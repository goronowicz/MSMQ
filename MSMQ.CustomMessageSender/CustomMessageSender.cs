using System;
using System.Messaging;

namespace MSMQ.CustomMessageSender
{
    public class CustomMessageSender
    {
        static void Main(string[] args)
        {
            var addres = args[0];
            var priority = MessagePriority.Normal;

            if (args.Length > 1)
            {
                Enum.TryParse(args[1], out priority);
            }

            using (var queue = new MessageQueue(addres))
            {
                var consoleKeyInfo = new ConsoleKeyInfo();

                while (consoleKeyInfo.Key != ConsoleKey.Escape)
                {
                    Console.Write("Provide message : ");
                    var messageBody = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(messageBody))
                    {
                        messageBody = "<EmptyMessage>";
                    }
                    var message = new Message(messageBody)
                    {
                        Priority = priority
                    };
                    queue.Send(message);

                    Console.WriteLine($"Message {messageBody} send to the multicast addres : {addres}");
                    consoleKeyInfo = Console.ReadKey();
                }
            }
        }
    }
}
