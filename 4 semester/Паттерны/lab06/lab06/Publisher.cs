using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab06
{
    public class Publisher
    {
        private delegate void SubscriberUpdate(string eventname);
        private event SubscriberUpdate? _update;
        private string _eventname;

        public Publisher(string eventname)
        {
            _eventname = eventname;
        }

        public void subscribe(ISubscriber subscriber)
        {
            _update += subscriber.update;
        }

        public bool unsubscribe(ISubscriber subscriber)
        {
            _update -= subscriber.update;
            return true;
        }

        public int nonify()
        {
            _update?.Invoke(_eventname);
            return _update?.GetInvocationList()?.Length ?? 0;
        }
    }
}