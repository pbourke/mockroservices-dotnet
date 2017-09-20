using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Events;
using VaughnVernon.Mockroservices;

namespace JobMatchingContext
{
    public class JobProviderMatchingDomainService
    {
        public IEnumerable<IDomainEvent> MatchProvidersToJob(Job job)
        {
            //Always the same ones
            return new [] {
                new ProvidersMatchedToJob(job.JobId, new []
                {
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    Guid.NewGuid()
                })
           };
        }
    }
}
