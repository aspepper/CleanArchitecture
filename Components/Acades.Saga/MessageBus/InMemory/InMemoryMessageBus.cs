using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acades.Saga.MessageBus.Interfaces;
using Acades.Saga.Utils;

namespace Acades.Saga.MessageBus.InMemory
{
    public class InMemoryMessageBus : IMessageBus
    {
        private readonly Dictionary<Type, Dictionary<object, Subscriber>> typesAndSubscribers;

        public InMemoryMessageBus()
        {
            typesAndSubscribers = new Dictionary<Type, Dictionary<object, Subscriber>>();
        }

        public async Task Publish(IInternalMessage message)
        {
            Type incomingMessageType = message.GetType();

            KeyValuePair<Type, Dictionary<object, Subscriber>>[] typesAndSubscribersArr = null;

            lock (typesAndSubscribers)
            {
                typesAndSubscribersArr = typesAndSubscribers.ToArray();
            }

            foreach (KeyValuePair<Type, Dictionary<object, Subscriber>> typesAndSubs in typesAndSubscribersArr)
            {
                Type type = typesAndSubs.Key;
                if (incomingMessageType.Is(type))
                {
                    KeyValuePair<object, Subscriber>[] subscribersArr = null;

                    lock (typesAndSubscribers)
                    {
                        subscribersArr = typesAndSubs.Value.ToArray();
                    }

                    foreach (KeyValuePair<object, Subscriber> subscriber in subscribersArr)
                    {
                        Func<IInternalMessage, Task>[] actionsArr = null;

                        lock (typesAndSubscribers)
                        {
                            actionsArr = subscriber.Value.Actions.ToArray();
                        }

                        foreach (Func<IInternalMessage, Task> action in actionsArr)
                            await action(message);
                    }
                }
            }
        }

        public void Subscribe<T>(object listener, Func<T, Task> handler)
        {
            lock (typesAndSubscribers)
            {
                typesAndSubscribers.TryGetValue(typeof(T), out Dictionary<object, Subscriber> dict);
                dict ??= typesAndSubscribers[typeof(T)] = new Dictionary<object, Subscriber>();

                dict.TryGetValue(listener, out Subscriber subscriber);
                subscriber ??= dict[listener] = new Subscriber
                    {
                        Sub = subscriber
                    };

                Task func(IInternalMessage msg) => handler((T)msg);
                subscriber.Actions.Add(func);
            }
        }

        public void Unsubscribe<T>(object listener)
        {
            lock (typesAndSubscribers)
            {
                typesAndSubscribers.TryGetValue(typeof(T), out Dictionary<object, Subscriber> dict);
                if (dict == null) { return; }

                dict.TryGetValue(listener, out Subscriber subscriber);
                if (subscriber == null) { return; }

                subscriber.Actions.Clear();
                subscriber.Sub = null;
                dict.Remove(listener);
            }
        }

        internal class Subscriber
        {
            internal List<Func<IInternalMessage, Task>> Actions;
            internal object Sub;

            public Subscriber()
            {
                Actions = new List<Func<IInternalMessage, Task>>();
            }
        }
    }
}
