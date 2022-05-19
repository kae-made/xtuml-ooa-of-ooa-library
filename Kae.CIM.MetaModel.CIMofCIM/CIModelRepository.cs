using System.Collections.Generic;

namespace Kae.CIM
{
    public abstract class CIModelRepository
    {
        public abstract IEnumerable<CIClassDef> GetCIInstances(string domainName, string className);
        public abstract T CreateCIInstance<T>(string domainName, string className, IDictionary<string, object> attributes) where T: CIClassDef;

        public abstract void DeleteCIInstane(CIClassDef instane);
    }

}

