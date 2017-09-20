using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Events;
using VaughnVernon.Mockroservices;

namespace JobMatchingContext
{
    public class Job : EventSourcedRootEntity
    {
        public string JobId { get; }

        protected Job(List<IDomainEvent> stream, int streamVersion) : base(stream, streamVersion)
        {
        }

        public bool IsProposalPriceFair()
        {
            throw new NotImplementedException(); 
        }

        public IDomainEvent FinalizeProposal()
        {
            var finalizeEvent = new JobProposalFinalized(this.JobId);
            Apply(finalizeEvent);
            return finalizeEvent;
        }

        public IDomainEvent RequestBidFromProvider(string providerId)
        {
            var bidRequestedEvent = new BidRequestedFromProvider(this.JobId, providerId);
            Apply(bidRequestedEvent);
            return bidRequestedEvent;
        }
    }
}
