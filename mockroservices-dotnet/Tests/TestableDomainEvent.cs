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

using System;

namespace VaughnVernon.Mockroservices
{
    public class TestableDomainEvent : IDomainEvent
    {
        public int EventVersion { get; private set; }
        public long Id { get; private set; }
        public string Name { get; private set; }
        public DateTime OccurredOn { get; private set; }

        public TestableDomainEvent(long id, string name)
        {
            this.Id = id;
            this.Name = name;
            this.EventVersion = 1;
            this.OccurredOn = DateTime.Now;
        }
    }
}
