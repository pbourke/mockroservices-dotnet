using System;
using System.Collections.Generic;
using VaughnVernon.Mockroservices;

namespace Common.Events
{
    public class ProvidersMatchedToJob : IDomainEvent
    {
        public int EventVersion { get; }
        public DateTime OccurredOn { get; }

        public Guid JobId { get; }
        /// <summary>
        /// Ideally this would be an immutable collection rather than IEnumerable, but...
        /// </summary>
        public IEnumerable<Guid> ProviderIds { get; }

        public ProvidersMatchedToJob(Guid jobId, IEnumerable<Guid> providerIds)
            : this(1, DateTime.UtcNow, jobId, providerIds)
        {
                
        } 

        public ProvidersMatchedToJob(int eventVersion, DateTime occurredOn, Guid jobId, IEnumerable<Guid> providerIds)
        {
            if (providerIds == null) throw new ArgumentNullException(nameof(providerIds));

            EventVersion = eventVersion;
            OccurredOn = occurredOn;
            JobId = jobId;
            ProviderIds = new List<Guid>(providerIds);
        }
    }
}