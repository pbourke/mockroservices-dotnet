using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Events;
using VaughnVernon.Mockroservices;

namespace PricingContext
{
    public class JobPriceScoringWorkflowCoordinator : ISubscriber
    {
        private Topic _jobMatchingContext;
        private Topic _pricingContext;

        public void Subscribe(MessageBus messageBus)
        {
            _jobMatchingContext = messageBus.OpenTopic("JobMatchingContext");
            _jobMatchingContext.Subscribe(this);

            _pricingContext = messageBus.OpenTopic("PricingContext");
        }

        public void Handle(Message message)
        {
            switch (message.Type)
            {
                case "JobProposed":
                    HandleJobProposed(message.Payload);
                    break;
            }

        }

        private void HandleJobProposed(string messagePayload)
        {
            var jobProposed = Serialization.Deserialize<JobProposed>(messagePayload);

            var repository = new Repository<PricingJob>();

            var job = repository.Read(jobProposed.JobId.ToString()) ?? new PricingJob(jobProposed.JobId);

            job.ScorePrice(jobProposed.TargetPrice);
            
            Emit(job.MutatingEvents);

            repository.Save(job, job.JobId.ToString());
        }

        protected void Emit(IEnumerable<IDomainEvent> events)
        {
            foreach (var @event in events)
            {
                var payload = Serialization.Serialize(@event);
                _pricingContext.Publish(new Message(Guid.NewGuid().ToString(), @event.GetType().Name, payload));
            }
        }

    }
}
