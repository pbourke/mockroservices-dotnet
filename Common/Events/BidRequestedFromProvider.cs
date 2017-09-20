using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaughnVernon.Mockroservices;

namespace JobMatchingContext
{
    public class BidRequestedFromProvider : IDomainEvent
    {
        public int EventVersion { get; }

        public DateTime OccurredOn { get; }

        public Guid JobId { get; }

        public Guid ProviderId { get; }

        public BidRequestedFromProvider(Guid jobId, Guid providerId)
            : this (1, DateTime.UtcNow, jobId, providerId)
        {
            
        }

        public BidRequestedFromProvider(int eventVersion, DateTime occurredOn, Guid jobId, Guid providerId)
        {
            EventVersion = eventVersion;
            OccurredOn = occurredOn;
            JobId = jobId;
            ProviderId = providerId;
        }

    }
}
