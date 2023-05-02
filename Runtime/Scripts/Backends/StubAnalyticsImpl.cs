using System;
using System.Collections.Generic;
using UnityEngine;

namespace HyperVR.Analytics
{
    public class StubAnalyticsImpl : IAnalytics
    {
        public bool EnableLogs { get; set; } = true;

        public int MaxParameterLength => Int32.MaxValue;

        public StubAnalyticsImpl(bool enableLogs)
        {
            EnableLogs = enableLogs;
        }

        public void TrackEvent(string eventName, List<Param> parameters)
        {
            if (EnableLogs)
            {
                Debug.Log(
                    $"Analytics event: '{eventName.Colorize("green")}'. " +
                    $"Params: {string.Join(", ", parameters)}"
                );
            }
        }
    }
}