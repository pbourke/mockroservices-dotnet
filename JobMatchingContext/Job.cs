using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Events;
using VaughnVernon.Mockroservices;

namespace JobMatchingContext
{
    public class Job : EventSourcedRootEntity
    {
        public DateTime TargetCompletionDate { get; private set; }

        public MonetaryValue TargetPrice { get; private set; }

        public string JobType { get; private set; }

        public Guid JobId { get; private set; }

        public int PriceScore { get; private set; }

        public List<Guid> BidsRequestedFromProviders { get; private set; }

        public Job(List<IDomainEvent> stream, int streamVersion) : base(stream, streamVersion)
        {
        }

        public Job(DateTime targetCompletionDate, MonetaryValue targetPrice, string jobType)
        {
            Apply(new JobProposed(Guid.NewGuid(), targetCompletionDate, targetPrice, jobType));
        }

        public void When(JobProposed jobProposedEvent)
        {
            JobId = jobProposedEvent.JobId;
            TargetPrice = jobProposedEvent.TargetPrice;
            JobType = jobProposedEvent.JobType;
            TargetCompletionDate = jobProposedEvent.TargetCompletionDate;
        }

        public void When(JobProposalPriceScored jobProposalPriceScored)
        {
            PriceScore = jobProposalPriceScored.Score;
        }

        public void When(BidRequestedFromProvider bidRequestedFromProvider)
        {
            if (BidsRequestedFromProviders == null) BidsRequestedFromProviders = new List<Guid>();
            if (!BidsRequestedFromProviders.Contains(bidRequestedFromProvider.ProviderId))
            {
                BidsRequestedFromProviders.Add(bidRequestedFromProvider.ProviderId);
            }    
        }

        public override bool Equals(object obj)
        {
            var otherJob = obj as Job;

            if (otherJob == null)
            {
                return false;
            }

            return Equals(this.JobId, otherJob.JobId)
                && Equals(this.JobType, otherJob.JobType)
                && Equals(this.TargetCompletionDate, otherJob.TargetCompletionDate)
                && Equals(this.TargetPrice, otherJob.TargetPrice);
        }

        public bool IsProposalPriceFair()
        {
            return PriceScore > 0;
        }

        public void RequestBidFromProvider(Guid providerId)
        {
            Apply(new BidRequestedFromProvider(this.JobId, providerId));
        }

        public void FinalizeProposal()
        {
            Apply(new JobProposalFinalized(this.JobId));
        }
    }
}
