using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaughnVernon.Mockroservices;

namespace Common.Events
{
    public class JobProposalPriceScored : IDomainEvent
    {
        public int EventVersion { get; }
        public DateTime OccurredOn { get; }
        public int Score { get; }

        public JobProposalPriceScored(int score)
            : this(1, DateTime.UtcNow, score)
        {
            
        }

        public JobProposalPriceScored(int eventVersion, DateTime occurredOn, int score)
        {
            EventVersion = eventVersion;
            OccurredOn = occurredOn;
            Score = score;
        }
    }
}
