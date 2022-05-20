// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System.Collections.Generic;

namespace Kae.CIM
{
    public abstract class CIModelRepository
    {
        public abstract IEnumerable<CIClassDef> GetCIInstances(string domainName, string className);
        public abstract T CreateCIInstance<T>(string domainName, string className, IDictionary<string, object> attributes, bool allowUndef = true) where T : CIClassDef;

        public abstract CIClassDef CreateCIInstance(string domainName, string className, IDictionary<string, object> attributes, bool allowUndef = true);

        public abstract void DeleteCIInstane(CIClassDef instane);
    }

}

