﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Events;
using VaughnVernon.Mockroservices;

namespace PricingContext
{
    public class Job : EventSourcedRootEntity
    {
        public Guid JobId { get; private set; }

        public int PriceScore { get; private set; }

        public Job(List<IDomainEvent> stream, int streamVersion) : base(stream, streamVersion)
        {
        }

        public void ScorePrice(MonetaryValue price)
        {
            Apply( new JobProposalPriceScored(
                this.JobId, 
                new Random(DateTime.Now.GetHashCode()).Next(-1, 1)));
        }

        public void When(JobProposalPriceScored jobProposalPriceScored)
        {
            this.PriceScore = jobProposalPriceScored.Score;
        }

        public override bool Equals(object obj)
        {
            var otherJob = obj as Job;

            if (otherJob == null)
            {
                return false;
            }

            return Equals(this.JobId, otherJob.JobId)
                && Equals(this.PriceScore, otherJob.PriceScore);
        }

    }
}
