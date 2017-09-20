using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Common.Events;
using VaughnVernon.Mockroservices;

namespace JobMatchingContext
{
    public class JobWorkflowCoordinator : ISubscriber
    {
        private Topic _pricingContextTopic;
        private Topic _jobMatchingContextTopic;

        public JobWorkflowCoordinator()
        {
        }

        public void Subscribe(MessageBus messageBus)
        {
            _pricingContextTopic = messageBus.OpenTopic("PricingContext");
            _pricingContextTopic.Subscribe(this);

            _jobMatchingContextTopic = messageBus.OpenTopic("JobMatchingContext");
            _jobMatchingContextTopic.Subscribe(this);
        }

        public void Unsubscribe()
        {
            _pricingContextTopic.Close();
            _jobMatchingContextTopic.Close();
        }

        public void Handle(Message message)
        {
            switch (message.Type)
            {
                case "JobProposalPriceScored":
                    HandleJobProposalPriceScored(message.Payload);
                    break;

                case "JobProposalFinalized":
                    HandleJobProposalFinalized(message.Payload);
                    break;

                case "ProvidersMatchedToJob":
                    HandleProvidersMatchedToJob(message.Payload);
                    break;
            }
         
        }

        private void HandleJobProposalPriceScored(string messagePayload)
        {
            var jobProposalPriceScored = Serialization.Deserialize<JobProposalPriceScored>(messagePayload);

            var job = new Job(repository.ReadEvents(jobProposalPriceScored.JobId));

            //If this is a fair priced system
            if (job.IsProposalPriceFair())
            {
                var events = job.FinalizeProposal();
                Emit(events);   
            }
        }

        private void HandleJobProposalFinalized(string messagePayload)
        {
            var jobProposalFinalized = Serialization.Deserialize<JobProposalFinalized>(messagePayload);

            var job = new Job(repository.ReadEvents(jobProposalPriceScored.JobId));

            var matchingSvc = new JobProviderMatchingDomainService();
            var matchingProviders = matchingSvc.MatchProvidersToJob(job);

            Emit(matchingProviders);
        }

        private void HandleProvidersMatchedToJob(string messagePayload)
        {
            var providersMatchedToJob = Serialization.Deserialize<ProvidersMatchedToJob>(messagePayload);

            var job = new Job(repository.ReadEvents(providersMatchedToJob.JobId));

            foreach (var providerId in providersMatchedToJob.ProviderAggregateIds)
            {
                Emit(job.RequestBidFromProvider(providerId));
                
            }
        }

        protected void Emit(IDomainEvent @event)
        {
            var payload = Serialization.Serialize(@event);
            _jobMatchingContextTopic.Publish(
                new Message(Guid.NewGuid().ToString(), @event.GetType().Name, payload));
        }
    }
}
