// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Kae.CIM
{
    internal class CIModeRepsoitoryImpl : CIModelRepository
    {
        protected Dictionary<string, Dictionary<string, List<CIClassDef>>> ciInstances = new Dictionary<string, Dictionary<string, List<CIClassDef>>>();
        public override T CreateCIInstance<T>(string domainName, string className, IDictionary<string, object> attributes, bool allowundef)
        {
            string typeName = $"CIMClass{className}Base";
            var currentMethod = MethodBase.GetCurrentMethod();
            var assembly = currentMethod.DeclaringType.Assembly;
            var candidates = assembly.GetTypes().Where( t => t.IsClass && t.Name == typeName);
            if (candidates.Count() > 0)
            {
                var cClass = candidates.First();
                T cInstance = (T)cClass.GetConstructor(new Type[] { typeof(CIModelRepository), typeof(IDictionary<string, object>) }).Invoke(new object[] { this, attributes });
                if (!ciInstances.ContainsKey(domainName))
                {
                    ciInstances.Add(domainName, new Dictionary<string, List<CIClassDef>>());
                }
                if (!ciInstances[domainName].ContainsKey(className))
                {
                    ciInstances[domainName].Add(className, new List<CIClassDef>());
                }
                ciInstances[domainName][className].Add(cInstance);
                return cInstance;
            }
            else
            {
                if (allowundef)
                {
                    return default(T);
                }
                else
                {
                    throw new ArgumentOutOfRangeException("There is no class implementation for className");
                }
            }
        }

        public override CIClassDef CreateCIInstance(string domainName, string className, IDictionary<string, object> attributes, bool allowUndef = true)
        {
            CIClassDef cInstance = null;
            string typeName = $"CIMClass{className}Base";
            var currentMethod = MethodBase.GetCurrentMethod();
            var assembly = currentMethod.DeclaringType.Assembly;
            var candidates = assembly.GetTypes().Where(t => t.IsClass && t.Name == typeName);
            if (candidates.Count() > 0)
            {
                var cClass = candidates.First();
                cInstance = (CIClassDef)cClass.GetConstructor(new Type[] { typeof(CIModelRepository), typeof(IDictionary<string, object>) }).Invoke(new object[] { this, attributes });
                if (!ciInstances.ContainsKey(domainName))
                {
                    ciInstances.Add(domainName, new Dictionary<string, List<CIClassDef>>());
                }
                if (!ciInstances[domainName].ContainsKey(className))
                {
                    ciInstances[domainName].Add(className, new List<CIClassDef>());
                }
                ciInstances[domainName][className].Add(cInstance);
                return cInstance;
            }
            else
            {
                if (allowUndef)
                {
                    return cInstance;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("There is no class implementation for className");
                }
            }

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

