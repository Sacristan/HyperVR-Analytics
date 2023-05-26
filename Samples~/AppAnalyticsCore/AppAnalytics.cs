using System.Linq;
using UnityEngine;

namespace HyperVR.Analytics
{
    public static class AppAnalytics
    {
        private static Analytics analyticsImpl;

        private static bool initialized;

        public static void Initialize(bool enableLogs = false)
        {
            if (initialized)
            {
                return;
            }

            initialized = true;

            IAnalytics[] analyticsBackends = DefineBackends(enableLogs);
            Debug.Log($"Analytics backends: {string.Join(", ", analyticsBackends.Select(a => a.GetType().Name))}");

            analyticsImpl = new Analytics(analyticsBackends);
            analyticsImpl.SetCommonParametersProvider(new AnalyticsCommonParamsProvider());
        }

        private static IAnalytics[] DefineBackends(bool enableLogs)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            return new IAnalytics[] { new StubAnalyticsImpl(enableLogs) };
#elif UNITY_ANDROID
        return new IAnalytics[]
        {
            new StubAnalyticsImpl(enableLogs),
            new GameAnalyticsImpl(AnalyticsPlayerId.Value),
            new FirebaseAnalyticsImpl(AnalyticsPlayerId.Value)
        };
#else
        return new IAnalytics[] { new StubAnalyticsImpl(enableLogs) };
#endif
        }

        public static Param GetParam()
        {
            Initialize();
            return analyticsImpl.GetParam();
        }

        public static void TrackEvent(string eventName)
        {
            Initialize();
            analyticsImpl.TrackEvent(eventName);
        }

        public static void TrackEvent(string eventName, Param p)
        {
            Initialize();
            analyticsImpl.TrackEvent(eventName, p);
        }

        public static void TrackEvent(string eventName, Param p1, Param p2)
        {
            Initialize();
            analyticsImpl.TrackEvent(eventName, p1, p2);
        }

        public static void TrackEvent(string eventName, Param p1, Param p2, Param p3)
        {
            Initialize();
            analyticsImpl.TrackEvent(eventName, p1, p2, p3);
        }

        public static void TrackEvent(string eventName, Param p1, Param p2, Param p3, Param p4)
        {
            Initialize();
            analyticsImpl.TrackEvent(eventName, p1, p2, p3, p4);
        }

        public static void TrackEvent(string eventName, Param p1, Param p2, Param p3, Param p4, Param p5)
        {
            Initialize();
            analyticsImpl.TrackEvent(eventName, p1, p2, p3, p4, p5);
        }

        public static void TrackEvent(string eventName, Param p1, Param p2, Param p3, Param p4, Param p5, Param p6)
        {
            Initialize();
            analyticsImpl.TrackEvent(eventName, p1, p2, p3, p4, p5, p6);
        }
    }
}