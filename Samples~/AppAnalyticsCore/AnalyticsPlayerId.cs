using System;
using UnityEngine;

namespace HyperVR.Analytics
{
    public static class AnalyticsPlayerId
    {
        private const string PlayerIdKey = "player_id";

        public static string Value
        {
            get
            {
                if (!PlayerPrefs.HasKey(PlayerIdKey))
                {
                    PlayerPrefs.SetString(PlayerIdKey, Guid.NewGuid().ToString());
                }

                return PlayerPrefs.GetString(PlayerIdKey);
            }
        }

        public static void Clear()
        {
            PlayerPrefs.DeleteKey(PlayerIdKey);
        }
    }
}