using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using PanicButton.Interfaces;

namespace PanicButton.Services
{
    public sealed class EventAggregator
    {
        private readonly List<IListen> _subscribers;
        private EventAggregator()
        {
            _subscribers = new();
        }

        private static readonly Lazy<EventAggregator> lazy = new(() => new EventAggregator());
        
        public static EventAggregator Instance
        {
            get { return lazy.Value; }
        }

        internal void Subscribe(IListen model)
        {
            this._subscribers.Add(model);
        }

        internal void Unsubscribe(IListen model)
        {
            this._subscribers.Remove(model);
        }

        internal void Publish<T>(T message)
        {
            foreach (IListen<T> item in this._subscribers.OfType<IListen<T>>())
            {
                item.Handle(message);
            }
        }

    }
}
