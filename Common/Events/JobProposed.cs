using System;
using VaughnVernon.Mockroservices;

namespace Common.Events
{
    public class JobProposed : IDomainEvent
    {
        public JobProposed()
        {
            EventVersion = 0;
            OccurredOn = DateTime.UtcNow;
        }

        public int EventVersion { get; }
        public DateTime OccurredOn { get; }
    }
}