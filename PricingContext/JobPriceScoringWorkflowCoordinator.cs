using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Events;
using VaughnVernon.Mockroservices;

namespace PricingContext
{
    public class JobPriceScoringWorkflowCoordinator : ISubscriber
    {
        private MessageBus MessageBus { get; }

        public JobPriceScoringWorkflowCoordinator(MessageBus messageBus)
        {
            MessageBus = messageBus;
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

            var scoringSvc = new PriceScoringDomainService();
            var outputEvents = scoringSvc.ScoreJobProposalPrice(jobProposed.JobId, jobProposed.TargetPrice);

            Emit(outputEvents);
        }

        protected void Emit(IEnumerable<IDomainEvent> events)
        {
            var topic = MessageBus.OpenTopic("PricingContext");

            foreach (var @event in events)
            {
                var payload = Serialization.Serialize(@event);
                topic.Publish(new Message(Guid.NewGuid().ToString(), @event.GetType().Name, payload));
            }
        }

    }
}
