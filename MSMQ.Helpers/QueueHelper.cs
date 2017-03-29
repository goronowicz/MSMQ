using System.Messaging;

namespace MSMQ.Helpers
{
    public static class QueueHelper
    {
        public static void CreateRoomQueues(string[] paths)
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
}
