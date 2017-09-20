using System;
using System.Linq;
using System.Reflection;
using VaughnVernon.Mockroservices;

namespace Common
{
    public class Repository<T> where T : EventSourcedRootEntity
    {
        private EventJournal eventJournal;

        public Repository()
        {
            eventJournal = EventJournal.Open("ej");    
        }

        public void Save(T instance, string id)
        {
            foreach (var domainEvent in instance.MutatingEvents)
            {
                eventJournal.Write($"{nameof(T)}#{id}", instance.MutatedVersion, domainEvent.GetType().FullName, Serialization.Serialize(domainEvent));
            }
        }

        public T Read(string id)
        {
            MethodInfo deserializeMethod = typeof(Serialization).GetMethod("Deserialize");

            var eventStream = eventJournal.StreamReader().StreamFor($"{nameof(T)}#{id}");
            var domainEvents = eventStream.Stream.Select(
                ev =>
                {
                    var type = Type.GetType(ev.Type);
                    var method = deserializeMethod.MakeGenericMethod(type);
                    return method.Invoke(null, new[] {ev.Body}) as IDomainEvent;
                }).ToList();


            return (T) Activator.CreateInstance(typeof(T), domainEvents, (int)eventStream.StreamVersion);
        }
    }
}