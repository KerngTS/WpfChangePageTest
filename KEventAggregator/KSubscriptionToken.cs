using System;

namespace KEventAggregator
{
    public class KSubscriptionToken : IEquatable<KSubscriptionToken>, IDisposable 
    {
        private readonly Guid _guid;
        private Delegate _action;

        public KSubscriptionToken(Delegate action )
        {
            if(action==null )throw new ArgumentNullException("action");
            _guid = Guid.NewGuid();
            _action = action;
        }

        public void Invoke<TEvent>(TEvent eventToPublish) where TEvent : IKEvent
        {
            //((Action<TEvent>)_action)(eventToPublish);
            _action.DynamicInvoke(eventToPublish);
        }

        public static bool operator ==(KSubscriptionToken left, KSubscriptionToken right)
        {
            //if (left == null || right == null) return false;
            return left.Equals(right);
        }

        public static bool operator !=(KSubscriptionToken left, KSubscriptionToken right)
            => !(left == right);

        public bool Equals(KSubscriptionToken other)
        {
            if ((object)other == null) return false;
            return Equals(_guid, other._guid);
        }

        public void Dispose()
        {
            if (this._action != null)
            {
                //this._action();
                this._action = null;
            }

            GC.SuppressFinalize(this);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as KSubscriptionToken);
        }

        public override int GetHashCode()
        {
            return _guid.GetHashCode();
        }

        
    }

    
}
