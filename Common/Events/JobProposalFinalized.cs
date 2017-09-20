using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaughnVernon.Mockroservices;

namespace Common.Events
{
    public class JobProposalFinalized : IDomainEvent
    {
        public int EventVersion { get; }
        public DateTime OccurredOn { get; }

        public Guid JobId { get; }

        public JobProposalFinalized(Guid jobId)
            : this(1, DateTime.UtcNow, jobId)
        {
            
        }

        public JobProposalFinalized(int eventVersion, DateTime occurredOn, Guid jobId)
        {
            EventVersion = eventVersion;
            OccurredOn = occurredOn;
            JobId = jobId;
        }
    }
}
