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

        public string JobId { get; }

        public JobProposalFinalized(string jobId)
            : this(1, DateTime.UtcNow, jobId)
        {
            
        }

        public JobProposalFinalized(int eventVersion, DateTime occurredOn, string jobId)
        {
            EventVersion = eventVersion;
            OccurredOn = occurredOn;
            JobId = jobId;
        }
    }
}
