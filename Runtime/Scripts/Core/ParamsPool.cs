using System.Collections.Generic;

namespace HyperVR.Analytics
{
    public class ParamsPool
    {
        private readonly List<Param> parametersPool = new List<Param>();

        public Param Get()
        {
            Param p;
            if (parametersPool.Count == 0)
            {
                p = new Param();
            }
            else
            {
                p = parametersPool[parametersPool.Count - 1];
                parametersPool.RemoveAt(parametersPool.Count - 1);
            }

            return p;
        }

        public void Release(Param p)
        {
            parametersPool.Add(p);
        }
    }
}