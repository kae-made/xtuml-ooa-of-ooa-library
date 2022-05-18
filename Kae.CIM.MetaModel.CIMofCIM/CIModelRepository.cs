using System;
using System.Collections.Generic;

namespace Kae.CIM
{
    public abstract class CIModelRepository
    {
        public abstract IEnumerable<CIClassDef> GetCIInstances(string domainName, string className);
        public abstract T CreateCIInstance<T>(string domainName, string className, IDictionary<string, object> attributes) where T: CIClassDef;

        public abstract void DeleteCIInstane(CIClassDef instane);
    }

    public class CIModeRepsoitoryImpl : CIModelRepository
    {
        protected Dictionary<string, Dictionary<string, List<CIClassDef>>> ciInstances = new Dictionary<string, Dictionary<string, List<CIClassDef>>>();
        public override T CreateCIInstance<T>(string domainName, string className, IDictionary<string, object> attributes)
        {
            throw new NotImplementedException();
        }

        public override void DeleteCIInstane(CIClassDef instance)
        {
            lock (ciInstances)
            {
                if (ciInstances.ContainsKey(instance.DomainName))
                {
                    if (ciInstances[instance.DomainName].ContainsKey(instance.ClassName))
                    {
                        ciInstances[instance.DomainName][instance.ClassName].Remove(instance);
                    }
                }
            }
        }

        public override IEnumerable<CIClassDef> GetCIInstances(string domainName, string className)
        {
            IEnumerable<CIClassDef> instances = null;
            lock (ciInstances) {
                if (ciInstances.ContainsKey(domainName))
                {
                    var domainInstances = ciInstances[domainName];
                    if (domainInstances.ContainsKey(className)){
                        instances = domainInstances[className];
                    }
                }
            }
            return instances;
        }
    }

}

