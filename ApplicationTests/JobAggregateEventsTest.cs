using System;
using System.Threading;
using ApplicationTests.Utils;
using Common;
using JobMatchingContext;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VaughnVernon.Mockroservices;

namespace ApplicationTests
{
    [TestClass]
    public class JobAggregateEventsTest
    {
        [TestMethod]
        public void NewJobShouldSendJobProposedEvent()
        {
//            var eventJournal = EventJournal.Open(nameof(NewJobShouldSendJobProposedEvent));
//            var messageBus = MessageBus.Start(nameof(NewJobShouldSendJobProposedEvent));
//            var topic = messageBus.OpenTopic("topic");
//            var publisher = EventJournalPublisher.From(eventJournal.Name, messageBus.Name, topic.Name);
//            
//            var subscriber = new CollectMessagesSubscriber();
//            topic.Subscribe(subscriber);

            var savedJob = new Job(new DateTime(2017, 12, 1), new MonetaryValue(500), "flooring");
            var repository = new Repository<Job>();
            repository.Save(savedJob, savedJob.JobId.ToString());
            var readJob = repository.Read(savedJob.JobId.ToString());

            Assert.AreEqual(readJob, savedJob);
        }
    }
}
