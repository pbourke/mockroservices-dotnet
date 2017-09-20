using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaughnVernon.Mockroservices;

namespace PricingContext
{
    public class JobScored : IDomainEvent
    {
        public JobScored()
        {
            EventVersion = 1;
            OccurredOn = DateTime.UtcNow;
        }

        public int EventVersion { get; }
        public DateTime OccurredOn { get; }
    }
}
