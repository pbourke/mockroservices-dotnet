using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Events;
using VaughnVernon.Mockroservices;

namespace PricingContext
{
    public class PriceScoringDomainService
    {
        public IEnumerable<IDomainEvent> ScoreJobProposalPrice(string jobId, double price)
        {
            var random = new Random(DateTime.Now.GetHashCode());
            return new[]
            {
                new JobProposalPriceScored(jobId, random.Next(-1, 1))
            };
        }
    }
}
