using System.Collections.Generic;
using VaughnVernon.Mockroservices;

namespace ApplicationTests.Utils
{
    public class CollectMessagesSubscriber : ISubscriber
    {
        public List<Message> Messages { get; }

        public CollectMessagesSubscriber()
        {
            Messages = new List<Message>();
        }

        public void Handle(Message message)
        {
            Messages.Add(message);
        }
    }
}