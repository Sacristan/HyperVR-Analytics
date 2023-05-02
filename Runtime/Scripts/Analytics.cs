using System.Collections.Generic;
using UnityEngine.Assertions;

namespace HyperVR.Analytics
{
    public class Analytics
    {
        private readonly IAnalytics[] backends;
        private readonly ParamsPool parametersPool = new ParamsPool();
        private readonly List<Param> parametersListCache = new List<Param>(8);
        private readonly List<Param> repackedParametersListCache = new List<Param>(8);
        private readonly List<Param> repackedParameterListCache = new List<Param>(8);

        public ICommonParamsProvider commonParametersProvider = new DummyCommonParamsProvider();

        public Analytics(IAnalytics[] backends)
        {
            this.backends = backends;
        }

        public void SetCommonParametersProvider(ICommonParamsProvider provider)
        {
            Assert.IsNotNull(provider);
            commonParametersProvider = provider;
        }

        public Param GetParam()
        {
            return parametersPool.Get();
        }

        private void ReleaseParameter(Param p)
        {
            parametersPool.Release(p);
        }

        public void TrackEvent(string eventName)
        {
            parametersListCache.Clear();
            TrackEventWithCachedParameters(eventName);
        }

        public void TrackEvent(string eventName, Param p)
        {
            parametersListCache.Clear();
            parametersListCache.Add(p);
            TrackEventWithCachedParameters(eventName);
        }

        public void TrackEvent(string eventName, Param p1, Param p2)
        {
            parametersListCache.Clear();
            parametersListCache.Add(p1);
            parametersListCache.Add(p2);
            TrackEventWithCachedParameters(eventName);
        }

        public void TrackEvent(string eventName, Param p1, Param p2, Param p3)
        {
            parametersListCache.Clear();
            parametersListCache.Add(p1);
            parametersListCache.Add(p2);
            parametersListCache.Add(p3);
            TrackEventWithCachedParameters(eventName);
        }

        public void TrackEvent(string eventName, Param p1, Param p2, Param p3, Param p4)
        {
            parametersListCache.Clear();
            parametersListCache.Add(p1);
            parametersListCache.Add(p2);
            parametersListCache.Add(p3);
            parametersListCache.Add(p4);
            TrackEventWithCachedParameters(eventName);
        }

        public void TrackEvent(string eventName, Param p1, Param p2, Param p3, Param p4, Param p5)
        {
            parametersListCache.Clear();
            parametersListCache.Add(p1);
            parametersListCache.Add(p2);
            parametersListCache.Add(p3);
            parametersListCache.Add(p4);
            parametersListCache.Add(p5);
            TrackEventWithCachedParameters(eventName);
        }

        public void TrackEvent(string eventName, Param p1, Param p2, Param p3, Param p4, Param p5, Param p6)
        {
            parametersListCache.Clear();
            parametersListCache.Add(p1);
            parametersListCache.Add(p2);
            parametersListCache.Add(p3);
            parametersListCache.Add(p4);
            parametersListCache.Add(p5);
            parametersListCache.Add(p6);
            TrackEventWithCachedParameters(eventName);
        }

        public void TrackEvent(string eventName, List<Param> parameters)
        {
            parametersListCache.Clear();
            for (int i = 0, iSize = parameters.Count; i < iSize; i++)
            {
                parametersListCache.Add(parameters[i]);
            }
        }

        private void TrackEventWithCachedParameters(string eventName)
        {
            AddCommonParameters();
            ValidateCachedParameters();

            for (int i = 0, iSize = backends.Length; i < iSize; i++)
            {
                backends[i].TrackEvent(
                    eventName,
                    RepackLongParameters(parametersListCache, backends[i].MaxParameterLength)
                );
            }

            ReleaseParametersListCache();
        }

        private List<Param> RepackLongParameters(List<Param> parameters, int maxParameterLength)
        {
            List<Param> repackedParameters = repackedParametersListCache;
            repackedParameters.Clear();

            for (int i = 0, iSize = parameters.Count; i < iSize; i++)
            {
                Param parameter = parameters[i];
                if (parameter.HasStringValue && parameter.StringValue.Length > maxParameterLength)
                {
                    repackedParameters.AddRange(RepackLongParameter(parameter, maxParameterLength));
                }
                else
                {
                    repackedParameters.Add(parameter);
                }
            }

            return repackedParameters;
        }

        private IEnumerable<Param> RepackLongParameter(Param parameter, int maxParameterLength)
        {
            Assert.IsTrue(parameter.HasStringValue);
            Assert.IsTrue(parameter.StringValue.Length > maxParameterLength);
            
            List<Param> repackedParameter = repackedParameterListCache;

            IEnumerable<string> subValues = parameter.StringValue.SplitBy(maxParameterLength);
            repackedParameter.Clear();
            
            int counter = 0;
            foreach (string subParamValue in subValues)
            {
                repackedParameter.Add(
                    GetParam().Set(counter == 0 ? parameter.Name : parameter.Name + counter, subParamValue)
                );
                counter++;
            }

            return repackedParameter;
        }

        private void ValidateCachedParameters()
        {
            for (int i = 0, iSize = parametersListCache.Count; i < iSize; i++)
            {
                Assert.IsTrue(parametersListCache[i].HasAnyValue);
            }
        }

        private void AddCommonParameters()
        {
            Param[] commonParameters = commonParametersProvider.Get();

            for (int i = 0, iSize = commonParameters.Length; i < iSize; i++)
            {
                parametersListCache.Add(commonParameters[i]);
            }
        }

        private void ReleaseParametersListCache()
        {
            for (int i = 0, iSize = parametersListCache.Count; i < iSize; i++)
            {
                ReleaseParameter(parametersListCache[i]);
            }

            parametersListCache.Clear();
        }
    }
}