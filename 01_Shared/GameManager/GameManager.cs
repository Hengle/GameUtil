using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace GameUtil 
{

    public class GameManager:Singleton<GameManager>
    {
        //Interface Event Handler.

        public Dictionary<System.Type, List<object>> all_event_listeners = new Dictionary<System.Type, List<object>>();

        public void RegisterEventListener(object event_listener)
        {
            System.Type type = event_listener.GetType();

            System.Type[] interfaces = type.GetInterfaces();

            foreach (var tp in interfaces)
            {
                List<object> listeners = null;
                all_event_listeners.TryGetValue(tp, out listeners);

                if (listeners == null)
                {
                    all_event_listeners[tp] = new List<object>();
                    listeners = all_event_listeners[tp];
                }

                if (listeners.Contains(event_listener) == false)
                {
                    listeners.Add(event_listener);
                }
            }
        }

        public void UnRegisterEventListener(object event_listener) 
        {
            List<object> type_list = null;
            all_event_listeners.TryGetValue(event_listener.GetType(), out type_list);
            if (type_list != null) 
            {
                type_list.Remove(event_listener);
            }
        }

        public void UnregisterAllGameEventListener()
        {
            foreach (var val in all_event_listeners.Values)
            {
                val.Clear();
            }
            all_event_listeners.Clear();
        }

        public List<object> GetEventListeners<T>()
        {
            List<object> results = null;
            all_event_listeners.TryGetValue(typeof(T), out results);
            return results;
        }        
    }

}
