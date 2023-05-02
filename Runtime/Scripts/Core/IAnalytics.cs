using System.Collections.Generic;

namespace HyperVR.Analytics
{
    public interface IAnalytics
    {
        void TrackEvent(string eventName, List<Param> parameters);
        int MaxParameterLength { get; }
    }
}