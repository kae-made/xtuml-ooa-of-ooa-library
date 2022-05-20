// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kae.XTUML.Tools.MetaModelGenerator.template
{
    partial class CIMOOAofOOAClass
    {
        private string version;
        private string domainName;

        public CIMOOAofOOAClass(string version, string domainName)
        {
            this.version = version;
            this.domainName = domainName;
        }
    }
}
