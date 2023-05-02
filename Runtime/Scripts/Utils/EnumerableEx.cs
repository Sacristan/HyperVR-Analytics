using System;
using System.Collections.Generic;

namespace HyperVR.Analytics
{
    public static class EnumerableEx
    {
        /// https://stackoverflow.com/a/8944374
        public static IEnumerable<string> SplitBy(this string str, int chunkLength)
        {
            if (String.IsNullOrEmpty(str)) throw new ArgumentException("Argument can't be null");
            if (chunkLength < 1) throw new ArgumentException("$chunkLength must be > 1");

            for (int i = 0; i < str.Length; i += chunkLength)
            {
                if (chunkLength + i > str.Length)
                    chunkLength = str.Length - i;

                yield return str.Substring(i, chunkLength);
            }
        }
    }
}