using System;
using System.Messaging;
using MSMQ.Helpers;
using Rhino.ServiceBus.Msmq;

namespace MSMQ.Subqueue.Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            var queuePaths = args[0].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            QueueHelper.CreateRoomQueues(queuePaths);

            var subQueueName = args.Length > 1 ? args[1] : "vip";

            using (var queue = new MessageQueue(args[0]))
            {
                var received = queue.Peek();
                while (received != null)
                {
                    if (received.Priority == MessagePriority.VeryHigh)
                    {
                        queue.MoveToSubQueue(subQueueName, received);

                        Console.WriteLine($"Message {received.LookupId} moved to the subqueue: {subQueueName}");
                    }

                    received = queue.Peek();
                }
            }
        }
    }
}
