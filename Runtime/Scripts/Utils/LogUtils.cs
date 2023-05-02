namespace HyperVR.Analytics
{
    public static class LogUtils
    {
        /// http://answers.unity.com/answers/1898810/view.html
        public static string Colorize(this string text, string color)
        {
            return "<color=" + color + ">" + text + "</color>";
        }

        /// http://answers.unity.com/answers/1898810/view.html
        public static string Colorize(this string text, string color, bool bold)
        {
            return
                "<color=" + color + ">" +
                (bold ? "<b>" : "") +
                text +
                (bold ? "</b>" : "") +
                "</color>";
        }
    }
}