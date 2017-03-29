using System;
using System.Messaging;

namespace MSMQ.CustomMessageSender
{
    public class CustomMessageSender
    {
        static void Main(string[] args)
        {
            var addres = args[0];

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
                                          Priority = MessagePriority.VeryHigh
                                      };
                    queue.Send(message);

                    Console.WriteLine($"Message {messageBody} send to the multicast addres : {addres}");
                    consoleKeyInfo = Console.ReadKey();
                }
            }
        }
    }
}
