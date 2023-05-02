using System;
using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Analytics;
using HyperVR.Analytics;
using UnityEngine;

namespace HyperVR.Analytics
{
    public class FirebaseAnalyticsImpl : IAnalytics
    {
        private class EventData
        {
            public readonly string eventName;
            public readonly Parameter[] parameters;

            public EventData(string eventName, Parameter[] parameters)
            {
                this.eventName = eventName;
                this.parameters = parameters;
            }
        }

        private bool Initialized { get; set; }
        private readonly List<EventData> _scheduledEvents = new List<EventData>();

        /// "Param values can be up to 100 characters long." 
        /// https://firebase.google.com/docs/reference/android/com/google/firebase/analytics/FirebaseAnalytics.Param
        private const int MaxParamValueLength = 100;

        public int MaxParameterLength => MaxParamValueLength;

        public FirebaseAnalyticsImpl(string playerId)
        {
            FirebaseManager.Instance.StartCoroutine(Init(playerId));
        }

        private IEnumerator Init(string playerId)
        {
            yield return new WaitUntil(() => FirebaseManager.Instance.IsReady);

            FirebaseAnalytics.SetUserId(playerId);
            Initialized = true;
            InvokeScheduledEvents();
        }

        public void TrackEvent(string eventName, List<Param> parameters)
        {
            if (Initialized)
            {
                FirebaseAnalytics.LogEvent(eventName, ConvertToFirebaseParameters(parameters));
            }
            else
            {
                _scheduledEvents.Add(new EventData(eventName, ConvertToFirebaseParameters(parameters)));
            }
        }

        private void InvokeScheduledEvents()
        {
            for (int i = 0, iSize = _scheduledEvents.Count; i < iSize; i++)
            {
                EventData scheduledEvent = _scheduledEvents[i];
                FirebaseAnalytics.LogEvent(scheduledEvent.eventName, scheduledEvent.parameters);
            }
        }

        private Parameter[] ConvertToFirebaseParameters(List<Param> parameters)
        {
            Parameter[] firebaseParams = new Parameter[parameters.Count];
            for (int i = 0, iSize = firebaseParams.Length; i < iSize; i++)
            {
                Param parameter = parameters[i];
                firebaseParams[i] = ConvertToFirebaseParameter(parameter);
            }

            return firebaseParams;
        }

        private static Parameter ConvertToFirebaseParameter(Param param)
        {
            if (param.HasStringValue)
            {
                return new Parameter(param.Name, param.StringValue);
            }

            if (param.HasLongValue)
            {
                return new Parameter(param.Name, param.LongValue);
            }

            if (param.HasDoubleValue)
            {
                return new Parameter(param.Name, param.DoubleValue);
            }

            if (param.HasBoolValue)
            {
                return new Parameter(param.Name, param.BoolValue.ToString());
            }

            throw new Exception($"Param '{param.Name}' has no known value type");
        }
    }
}