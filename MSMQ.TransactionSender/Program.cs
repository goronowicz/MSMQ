using System;
using System.Messaging;

namespace MSMQ.TransactionSender
{
    class Program
    {
        static void Main(string[] args)
        {
            var addres = args[0];

            using (var queue = new MessageQueue(addres))
            {
                var consoleKeyInfo = new ConsoleKeyInfo();
                int i = 0;
                while (consoleKeyInfo.Key != ConsoleKey.Escape)
                {
                    object message = $"Test {i++}";
                    using (var transaction = new MessageQueueTransaction())
                    {
                        transaction.Begin();
                        queue.Send(message);

                        /*queue.Send(message);
                        transaction.Abort();*/

                        //throw new StackOverflowException();


                        transaction.Commit();
                        Console.WriteLine($"Message {message} send to the addres : {addres} with status {transaction.Status}");
                        consoleKeyInfo = Console.ReadKey();
                    }
                }
            }
        }
    }
}
