using System;

namespace HyperVR.Analytics
{
    public class DummyCommonParamsProvider : ICommonParamsProvider
    {
        public Param[] Get()
        {
            return Array.Empty<Param>();
        }
    }
}