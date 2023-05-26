using UnityEngine;
using UnityEngine.Assertions;

namespace HyperVR.Analytics
{
    public class ClickButtonAnalytics : MonoBehaviour
    {
        [SerializeField] private string buttonId;

        public string ButtonId
        {
            get => buttonId;
            set => buttonId = value;
        }

        private const string EventName = "button_click";
        private const string ButtonIdParamName = "button_id";

        public void OnClick()
        {
            Assert.IsFalse(string.IsNullOrEmpty(buttonId), "'buttonId' can not be empty");
            AppAnalytics.TrackEvent(
                EventName,
                AppAnalytics.GetParam().Set(ButtonIdParamName, buttonId)
            );
        }
    }
}