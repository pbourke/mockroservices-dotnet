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

        public string JobId { get; }

        public string ProviderId { get; }

        public BidRequestedFromProvider(string jobId, string providerId)
            : this (1, DateTime.UtcNow, jobId, providerId)
        {
            
        }

        public BidRequestedFromProvider(int eventVersion, DateTime occurredOn, string jobId, string providerId)
        {
            EventVersion = eventVersion;
            OccurredOn = occurredOn;
            JobId = jobId;
            ProviderId = providerId;
        }

    }
}
