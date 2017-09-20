﻿using System;
using System.Collections.Generic;
using VaughnVernon.Mockroservices;

namespace Common.Events
{
    public class ProvidersMatchedToJob : IDomainEvent
    {
        public int EventVersion { get; }
        public DateTime OccurredOn { get; }

        /// <summary>
        /// Ideally this would be an immutable collection rather than IEnumerable, but...
        /// </summary>
        public IEnumerable<int> ProviderAggregateIds { get; }

        public ProvidersMatchedToJob(IEnumerable<int> providerAggregateIds)
            : this(1, DateTime.UtcNow, providerAggregateIds)
        {
                
        } 

        public ProvidersMatchedToJob(int eventVersion, DateTime occurredOn, IEnumerable<int> providerAggregateIds)
        {
            if (providerAggregateIds == null) throw new ArgumentNullException(nameof(providerAggregateIds));

            EventVersion = eventVersion;
            OccurredOn = occurredOn;
            ProviderAggregateIds = new List<int>(providerAggregateIds);
        }
    }
}