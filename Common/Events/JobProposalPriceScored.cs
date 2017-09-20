﻿using System;
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
        public string JobId { get; }
        public int Score { get; }

        public JobProposalPriceScored(string jobId, int score)
            : this(1, DateTime.UtcNow, jobId, score)
        {
            
        }

        public JobProposalPriceScored(int eventVersion, DateTime occurredOn, string jobId, int score)
        {
            EventVersion = eventVersion;
            OccurredOn = occurredOn;
            JobId = jobId;
            Score = score;
        }
    }
}