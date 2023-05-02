using System.Collections.Generic;
using GameAnalyticsSDK;
using HVR.Analytics;

namespace HyperVR.Analytics
{
    public class GameAnalyticsImpl : IAnalytics
    {
        private readonly Dictionary<string, object> _parametersCache = new Dictionary<string, object>();

        /// "...can max consist of 256 characters" 
        /// https://docs.gameanalytics.com/advanced-tracking/custom-event-fields
        private const int MaxParamValueLength = 256;

        public int MaxParameterLength => MaxParamValueLength;

        public GameAnalyticsImpl(string userId)
        {
            GameAnalytics.SetCustomId(userId);
            GameAnalytics.Initialize();
        }

        public void TrackEvent(string eventName, List<Param> parameters)
        {
            _parametersCache.Clear();
            for (int i = 0, iSize = parameters.Count; i < iSize; i++)
            {
                Param param = parameters[i];
                _parametersCache[param.Name] = param.ObjectValue;
            }

            GameAnalytics.NewDesignEvent(eventName, _parametersCache);
        }
    }
}