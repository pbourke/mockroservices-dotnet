﻿//   Copyright © 2017 Vaughn Vernon. All rights reserved.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

using System.Collections.Generic;

namespace VaughnVernon.Mockroservices
{
    public abstract class EventSourcedRootEntity
    {
        private readonly List<IDomainEvent> mutatingEvents;
        private readonly int unmutatedVersion;

        public List<IDomainEvent> MutatingEvents
        {
            get { return this.mutatingEvents; }
        }

        public int MutatedVersion
        {
            get { return this.unmutatedVersion + 1; }
        }

        public int UnmutatedVersion
        {
            get { return this.unmutatedVersion; }
        }

        protected EventSourcedRootEntity()
        {
            this.mutatingEvents = new List<IDomainEvent>();
            this.unmutatedVersion = 0;
        }

        protected EventSourcedRootEntity(List<IDomainEvent> stream, int streamVersion)
        {
            foreach (IDomainEvent domainEvent in stream)
            {
                DispatchWhen(domainEvent);
            }

            this.unmutatedVersion = streamVersion;
        }

        protected void Apply(IDomainEvent domainEvent)
        {
            mutatingEvents.Add(domainEvent);
            DispatchWhen(domainEvent);
        }

        private void DispatchWhen(IDomainEvent domainEvent)
        {
            ((dynamic) this).When((dynamic) domainEvent);
        }
    }
}
