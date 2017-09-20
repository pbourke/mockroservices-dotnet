using System;
using System.Reflection.Emit;
using VaughnVernon.Mockroservices;

namespace Common.Events
{
    public class JobProposed : IDomainEvent
    {
        public Guid JobId { get; }

        public DateTime TargetCompletionDate { get; }

        public MonetaryValue TargetPrice { get; }

        public string JobType { get; }

        public int EventVersion { get; }

        public DateTime OccurredOn { get; }

        public JobProposed(Guid jobId, DateTime targetCompletionDate, MonetaryValue targetPrice, string jobType)
        {
            EventVersion = 0;
            OccurredOn = DateTime.UtcNow;
            JobId = jobId;
            TargetCompletionDate = targetCompletionDate;
            TargetPrice = targetPrice;
            JobType = jobType;
        }
    }
}