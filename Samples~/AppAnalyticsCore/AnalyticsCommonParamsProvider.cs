using UnityEngine;

namespace HyperVR.Analytics
{
    public class AnalyticsCommonParamsProvider : ICommonParamsProvider
    {
        private readonly Param[] commonParams = new Param[1];

        public const string EventIdParamName = "event_id";

        private const string EventIdPrefsKey = EventIdParamName;

        private int IncAndGetEventId
        {
            get
            {
                int eventId = PlayerPrefs.GetInt(EventIdPrefsKey, 0);
                eventId++;
                PlayerPrefs.SetInt(EventIdPrefsKey, eventId);
                return eventId;
            }
        }

        public Param[] Get()
        {
            commonParams[0] = AppAnalytics.GetParam().Set(EventIdParamName, IncAndGetEventId);

            return commonParams;
        }

        public static void Clear()
        {
            PlayerPrefs.DeleteKey(EventIdPrefsKey);
        }
    }
}