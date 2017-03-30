using System;
using System.Messaging;

namespace MSMQ.CustomMessageReceiver
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = args[0]; //queue path

            using (var queue = new MessageQueue(path))
            {
                Console.WriteLine($"Connected to the queue: {path}");
                queue.ReceiveCompleted += ShowMessage;
                queue.BeginReceive();

                Console.ReadKey();
            }
        }

        private static void ShowMessage(object sender, ReceiveCompletedEventArgs e)
        {

            var queue = sender as MessageQueue;
            if (queue == null) return;
            var message = queue.EndReceive(e.AsyncResult);

            message.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            Console.WriteLine($"Message :{message.Body} received in queue : {queue.QueueName}");
            queue.BeginReceive();
        }
    }
}
