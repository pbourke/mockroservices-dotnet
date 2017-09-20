using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaughnVernon.Mockroservices;

namespace Common.Events
{
    public class PricingJobCreated : IDomainEvent
    {
        public int EventVersion { get; }
        public DateTime OccurredOn { get; }
        public Guid JobId { get; }

        public PricingJobCreated(Guid jobId)
            : this(1, DateTime.UtcNow, jobId)
        {
            
        }

        public PricingJobCreated(int eventVersion, DateTime occurredOn, Guid jobId)
        {
            EventVersion = eventVersion;
            OccurredOn = occurredOn;
            JobId = jobId;
        }
    }
}
