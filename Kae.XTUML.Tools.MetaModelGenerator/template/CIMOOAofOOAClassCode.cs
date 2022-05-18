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
