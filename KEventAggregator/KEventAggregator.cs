using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace KEventAggregator
{
    public class KEventAggregator
    {
        private static readonly Lazy<KEventAggregator> _lazyInstance = new Lazy<KEventAggregator>(() => new KEventAggregator());
        public static KEventAggregator Instance => _lazyInstance.Value;

        private Dictionary<Type, List<Delegate>> _subscribers = new Dictionary<Type, List<Delegate>>();

        public void Subscribe<TEvent>(Action<TEvent> action, Predicate<TEvent> filter=null)where TEvent:IKEvent
        {
            
            Type eventType = typeof(TEvent);
            filter = filter == null ? new Predicate<TEvent> ( delegate { return true; } ) : filter;
            var filterAction = new Action<TEvent>(e =>
            {
                if(filter(e))
                    action(e);
            });
            if (!_subscribers.ContainsKey(eventType))
            {
                _subscribers[eventType] = new List<Delegate>();
            }
            _subscribers[eventType].Add(filterAction);
        }

        public void Publish<TEvent>(TEvent eventToPublish)where TEvent:IKEvent
        {
            Type eventType = typeof(TEvent);
            if (_subscribers.ContainsKey(eventType))
            {
                //foreach (Delegate handler in _subscribers[eventType])
                //{
                //    handler.DynamicInvoke(eventToPublish);
                //}
                foreach (Action<TEvent> handler in _subscribers[eventType])
                {
                    handler(eventToPublish);
                }
            }
        }
    }

    public class KEventAggregator1
    {
        private static readonly Lazy<KEventAggregator1> _lazyInstance = new Lazy<KEventAggregator1>(() => new KEventAggregator1());
        public static KEventAggregator1 Instance => _lazyInstance.Value;

        private Dictionary<Type, List<KSubscriptionToken>> _subscribers = new Dictionary<Type, List<KSubscriptionToken>>();

        public KSubscriptionToken Subscribe<TEvent>(Action<TEvent> action, Predicate<TEvent> filter = null) where TEvent : IKEvent
        {

            Type eventType = typeof(TEvent);
            filter = filter == null ? new Predicate<TEvent>(delegate { return true; }) : filter;
            var filterAction = new Action<TEvent>(e =>
            {
                if (filter(e))
                    action(e);
            });
            var token = new KSubscriptionToken(filterAction);
            if (!_subscribers.ContainsKey(eventType))
            {
                _subscribers[eventType] = new List<KSubscriptionToken>();
            }
            _subscribers[eventType].Add(token);
            return token;
        }

        public void Publish<TEvent>(TEvent eventToPublish) where TEvent : IKEvent
        {
            Type eventType = typeof(TEvent);
            if (_subscribers.ContainsKey(eventType))
            {
                //foreach (Delegate handler in _subscribers[eventType])
                //{
                //    handler.DynamicInvoke(eventToPublish);
                //}
                foreach (var token in _subscribers[eventType])
                {
                    token.Invoke(eventToPublish);
                }
            }
        }

        public bool UnSubscribe<TEvent>(KSubscriptionToken token) 
        {
            Type eventType = typeof(TEvent);
            if (!_subscribers.ContainsKey(eventType))
            {
                _subscribers[eventType] = new List<KSubscriptionToken>();
            }
            var list = _subscribers[eventType];
            var item = list.Where(c => c==token).FirstOrDefault();
            if (item == null) return false;
            list.Remove(item);
            return true;
        }
    }
}
