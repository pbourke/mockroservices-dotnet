using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaughnVernon.Mockroservices;

namespace JobMatchingContext
{
    public class Job : EventSourcedRootEntity
    {
        protected Job(List<IDomainEvent> stream, int streamVersion) : base(stream, streamVersion)
        {
        }
    }
}
