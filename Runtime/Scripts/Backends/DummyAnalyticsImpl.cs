using System.Collections.Generic;

namespace HyperVR.Analytics
{
    public class DummyAnalyticsImpl : IAnalytics
    {
        public int MaxParameterLength => int.MaxValue;

        public void TrackEvent(string eventName, List<Param> parameters)
        {
        }
    }
}