using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Common;
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

            var repository = new Repository<Job>();
            var job = repository.Read(jobProposalPriceScored.JobId.ToString());

            //If this is a fair priced system
            if (job.IsProposalPriceFair())
            {
                job.FinalizeProposal();

                Emit(job.MutatingEvents);

                repository.Save(job, job.JobId.ToString());

            }
        }

        private void HandleJobProposalFinalized(string messagePayload)
        {
            var jobProposalFinalized = Serialization.Deserialize<JobProposalFinalized>(messagePayload);

            var repository = new Repository<Job>();
            var job = repository.Read(jobProposalFinalized.JobId.ToString());

            var matchingSvc = new JobProviderMatchingDomainService();
            var matchingProviders = matchingSvc.MatchProvidersToJob(job);

            Emit(matchingProviders);
        }

        private void HandleProvidersMatchedToJob(string messagePayload)
        {
            var providersMatchedToJob = Serialization.Deserialize<ProvidersMatchedToJob>(messagePayload);

            var repository = new Repository<Job>();
            var job = repository.Read(providersMatchedToJob.JobId.ToString());

            foreach (var providerId in providersMatchedToJob.ProviderIds)
            {
                job.RequestBidFromProvider(providerId);                
            }

            Emit(job.MutatingEvents);
            repository.Save(job, job.JobId.ToString());
        }

        protected void Emit(IEnumerable<IDomainEvent> events)
        {
            foreach (var @event in events)
            {
                var payload = Serialization.Serialize(@event);
                _jobMatchingContextTopic.Publish(
                    new Message(Guid.NewGuid().ToString(), @event.GetType().Name, payload));
            }
        }
    }
}
